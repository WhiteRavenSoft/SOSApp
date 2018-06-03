using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;
using WhiteRaven.Core.Enum;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.AppModel.External.MercadoPago;
using WhiteRaven.Data.DBModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.ExternalService
{
    public static class MercadoPagoSvc
    {

        public static AccessToken GetToken(string clientId, string clientSecret)
        {
            string requestMessage = "grant_type=client_credentials&client_id=" + clientId + "&client_secret=" + clientSecret;

            var webRequest = System.Net.WebRequest.Create(new Uri("https://api.mercadolibre.com/oauth/token"));
            webRequest.Method = "POST";
            //webRequest.ContentLength = requestMessage.Length;
            webRequest.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new System.IO.StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(requestMessage);
                streamWriter.Close();
            }

            var response = webRequest.GetResponse();

            using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<AccessToken>(result);
            }
        }
        public static PaymentPreferenceResponse GetPreference(PaymentPreferenceRequest preferenceRequest, AccessToken accessToken)
        {
            //deben ignorarse los null mp no acepta arrays vacíos
            string requestMessage = JsonConvert.SerializeObject(preferenceRequest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, });

            var webRequest = System.Net.WebRequest.Create(new Uri("https://api.mercadolibre.com/checkout/preferences?access_token=%token%".Replace("%token%", accessToken.access_token)));
            webRequest.Method = "POST";
            //webRequest.ContentLength = requestMessage.Length;
            webRequest.ContentType = "application/json";

            using (var streamWriter = new System.IO.StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(requestMessage);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = webRequest.GetResponse();

            using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<PaymentPreferenceResponse>(streamReader.ReadToEnd());
            }
        }
        public static PaymentSearchResponse GetPaymentSearchResponse(string external_reference)
        {
            var accessToken = GetToken(ConfigurationManager.AppSettings["MPClientID"].ToString(), ConfigurationManager.AppSettings["MPClientSecret"].ToString());

            string url = "https://api.mercadolibre.com/collections/search?access_token=";

            var webRequest = System.Net.WebRequest.Create(new Uri(url + accessToken.access_token + "&external_reference=" + external_reference + ""));
            webRequest.Method = "GET";
            //webRequest.ContentLength = 0;
            webRequest.ContentType = "application/json";

            var response = webRequest.GetResponse();

            PaymentSearchResponse searchResponse = null;

            using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(result).SelectToken("results");

                if (!jsonObject.HasValues)
                {
                    searchResponse = new PaymentSearchResponse()
                    {
                        Description = "Response empty",
                        SearchStatus = SearchStatus.Empty,
                        Status = "Empty"
                    };
                }
                if (jsonObject.Any())
                {
                    jsonObject = jsonObject[jsonObject.Count() - 1].SelectToken("collection");

                    var status = jsonObject.SelectToken("status").ToString();
                    var statusDetail = jsonObject.SelectToken("status_detail").ToString();
                    var id = jsonObject.SelectToken("id").ToString();
                    var total = Convert.ToDouble(jsonObject.SelectToken("total_paid_amount").ToString());


                    switch (status.ToLower())
                    {
                        default:
                            searchResponse = new PaymentSearchResponse()
                            {
                                Description = statusDetail,
                                SearchStatus = SearchStatus.NotPaid,
                                Status = status,
                                Id = id,
                                Total = total
                            };
                            break;
                        case "pending":
                        case "in_process":
                            searchResponse = new PaymentSearchResponse()
                            {
                                Description = statusDetail,
                                SearchStatus = SearchStatus.Pending,
                                Status = status,
                                Id = id,
                                Total = total
                            };
                            break;
                        case "approved":
                            searchResponse = new PaymentSearchResponse()
                            {
                                Description = statusDetail,
                                SearchStatus = SearchStatus.Paid,
                                Status = status,
                                Id = id,
                                Total = total
                            };
                            break;
                    }
                }

                GenericSvc<Pago, WhiteAdsEntities> paymenttSvc = IoC.Resolve<GenericSvc<Pago, WhiteAdsEntities>>();

                var payment = paymenttSvc.Load(Convert.ToInt32(external_reference));
                payment.Estado = searchResponse.SearchStatus == SearchStatus.Paid ? (int)PaymentStatus.Success : (int)PaymentStatus.Errror;
                payment.TraceTrx = jsonObject.ToString();
                paymenttSvc.UpdateEntity(payment);
            }

            return searchResponse;
        }


        public static PaymentPreferenceResponse InitPayment(int userID, decimal amount, bool autoReturn)
        {
            try
            {

                Pago payment = new Pago()
                {
                    Active = true,
                    Monto = amount,
                    FechaCrecion = DateTime.UtcNow,
                    Proveedor = (int)PaymentProvider.MercadoPago,
                    Estado = (int)PaymentStatus.Errror,
                    Tipo = (int)PaymentType.Charge,
                    CodigoTrx = "",
                    ID_Usuario = userID,
                    TraceTrx = ""
                };

                GenericSvc<Pago, WhiteAdsEntities> paymenttSvc = IoC.Resolve<GenericSvc<Pago, WhiteAdsEntities>>();
                payment = paymenttSvc.CreateEntity(payment);

                var userSvc = IoC.Resolve<UsuarioSvc>();
                var user = userSvc.Load(userID);
                var mercadoPagoItems = new List<PaymentPreferenceRequest.Items>
                            {
                                new PaymentPreferenceRequest.Items
                                    {
                                        currency_id = "ARS",
                                        description = "Publicidad WhiteAds",
                                        id = payment.ID.ToString(),//id de la compra
                                        picture_url = string.Empty,
                                        quantity = 1,
                                        title = string.Format("WhiteAds {0} {1}", "PMNT" + payment.ID.ToString().PadLeft(10,'0'), DateTime.Now.ToString("ddMMyyyyHHmm")),
                                        unit_price = Convert.ToDouble(amount)
                                    }
                            };

                string ulr = Settings.ApiUrl + "api/Payment/GatewayResult?status={0}&id={1}";

                var req = new PaymentPreferenceRequest
                {
                    items = mercadoPagoItems,
                    external_reference = payment.ID.ToString(), //id de la compra
                    payer = new PaymentPreferenceRequest.Payer
                    {
                        email = user.Email
                        //name = user.Name,
                        //surname = user.LastName
                    },
                    payment_methods = new PaymentPreferenceRequest.PaymentMethods()
                    {
                        excluded_payment_methods = new List<PaymentPreferenceRequest.PaymentMethods.ExcludedPaymentMethods>()
                        {
                            new PaymentPreferenceRequest.PaymentMethods.ExcludedPaymentMethods() {id = "pagofacil" },
                        },
                        excluded_payment_types = new List<PaymentPreferenceRequest.PaymentMethods.ExcludedPaymentTypes>()
                        {
                            new PaymentPreferenceRequest.PaymentMethods.ExcludedPaymentTypes() { id = "ticket"},
                            new PaymentPreferenceRequest.PaymentMethods.ExcludedPaymentTypes() { id = "atm"}
                        }

                    }
                };

                if (autoReturn)
                {
                    req.auto_return = "approved";
                    req.back_urls = new PaymentPreferenceRequest.BackUrls
                    {
                        failure = string.Format(ulr, "failure", payment.ID.ToString()),
                        pending = string.Format(ulr, "pending", payment.ID.ToString()),
                        success = string.Format(ulr, "success", payment.ID.ToString()),
                    };
                }

                var token = GetToken(ConfigurationManager.AppSettings["MPClientID"].ToString(), ConfigurationManager.AppSettings["MPClientSecret"].ToString());

                var response = GetPreference(req, token);
                payment.CodigoTrx = response.id;
                paymenttSvc.UpdateEntity(payment);

                return response;

            }
            catch (Exception e)
            {
                //TODO: Log e
                return null;
            }
        }
    }
}

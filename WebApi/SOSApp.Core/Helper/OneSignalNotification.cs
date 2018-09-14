using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace SOSApp.Core.Helper
{
    public static class OneSignalNotification
    {
        /// <summary>
        /// Envía Push Notifications a los dispositivos pasados como parámetros.
        /// </summary>
        /// <param name="strMessage">Mensaje a mostrar en la push</param>
        /// <param name="listPlayerIds">Lista de PlayerIds de OneSignal (Cada dispositivo se corresponde con un PlayerId)</param>
        /// <param name="newsId">Identificador de la noticia, para ser referenciado en el click de la push</param>
        public static void SendNotification(string strMessage, string[] listPlayerIds, int newsId)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic " + Settings.OneSignalRestAPIKey);

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = Settings.OneSignalAppId,
                contents = new { en = strMessage, es = strMessage },
                small_icon = "punto",
                large_icon = "icon",
                data = new { noticia = newsId },
                include_player_ids = listPlayerIds
            };

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Net.Http;

namespace SOSApp.Helpers
{
    public static class ApiRest
    {
        public static async Task<T> GetAsyncFormData<T>(string url)
        {
            try
            {

                using (var client = new HttpClient(new NativeMessageHandler()))
                {

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url)
                    };

                    var response = client.SendAsync(requestMessage);

                    //if (response.IsSuccessStatusCode)
                    //{
                    try
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content.Result);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("La respuesta no se puede deserializar a " + typeof(T).Name);
                    }
                    //}
                    //else
                    //    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    Debug.WriteLine(ex.ToString());
                });
            }

            return default(T);
        }

        public static async Task<T> GetFormData<T>(string url)
        {
            try
            {
                var client = new HttpClient(new NativeMessageHandler());
                //client.BaseAddress = new Uri(url);
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Autorization","BEARER eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1NjQyMjIwNTIsInR5cGUiOiJleHRlcm5hbCIsInVzZXIiOiJyYW1pcm8uc292cmFub0B3aGl0ZXJhdmVuc29mdC5jb20ifQ.Rl_xG2T58q2lNhVAsDeZHhB92GmlkPUj7NQ19dusad16A52n20YijKS9cFRQTzEjuyQksg9MM7l5D-mYknckJQ");
                //var requestMessage = new HttpRequestMessage
                //{
                //    Method = HttpMethod.Get,
                //    RequestUri = new Uri(url)
                //};


                using (var response = await client.GetAsync(new Uri(url)))
                {
                    try
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("La respuesta no se puede deserializar a " + typeof(T).Name);
                    }
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    Debug.WriteLine(ex.ToString());
                });
            }

            return default(T);
        }

        public static async Task<T> PostFormData<T>(string url, IEnumerable<KeyValuePair<string, string>> xparams)
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new FormUrlEncodedContent(xparams);

                    var response = await client.PostAsync(new Uri(url), data);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() =>
                //{
                //    Debug.WriteLine(ex.ToString());
                //});
            }

            return default(T);
        }
    }
}

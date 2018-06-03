using WhiteRaven.Data.AppModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Svc.ExternalService
{
    public class W3WService
    {
        public async Task<W3wResponse> GetByWords(string words)
        {
            using (var client = new HttpClient())
            {

                string apiKey = ConfigurationManager.AppSettings["W3WApiKey"].ToString();

                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(string.Format("https://api.what3words.com/v2/forward?addr={0}&display=full&format=json&key={1}&lang=es", words, apiKey))
                };

                var response = await client.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<W3wResponse>(content);
            }
        }

        public async Task<W3wResponse> GetByLatLong(string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {
                string apiKey = ConfigurationManager.AppSettings["W3WApiKey"].ToString();

                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(string.Format("https://api.what3words.com/v2/reverse?coords={0},{1}&display=full&format=json&key={2}&lang=es", latitude, longitude, apiKey))
                };

                var response = await client.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<W3wResponse>(content);
            }
        }
    }
}

using SOSApp.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SOSApp.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyHandler : DelegatingHandler
    {
        /// <summary>
        /// 
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        private bool ValidateKey(HttpRequestMessage message)
        {
            if (!AppHelper.LoadAppSettingboolean("App.ApiKey.Validate", false))
                return true;

            IEnumerable<string> apikey;

            var hasApiKey = message.Headers.TryGetValues("apikey", out apikey);

            if (hasApiKey)
            {
                //var key = apikey.ElementAt(0);

                //PQ_ApiKey apiKeyEntity = IoC.Resolve<ApiKeySvc>().FindSingle(a => a.Active && a.Key == key);
                //if (apiKeyEntity != null)
                //{
                //    return true;
                //}
            }

            return false;
        }
    }
}
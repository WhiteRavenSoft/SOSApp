﻿using HtmlAgilityPack;
using SOSApp.API.Core;
using SOSApp.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOSApp.API.Controllers
{
    public class GarbageCollectionController : ApiBaseController
    {
        // GET: api/GarbageCollection
        public HttpResponseMessage Get(string PlayerId)
        {
            string response = string.Empty;
            string dateJsonFormated = string.Empty;

            var user = userSvc.LoadByPlayerId(PlayerId);
            string apiParameters = $"?lat={user.Lat}&long={user.Lon}&fa={dateJsonFormated}";

            //var url = Settings.GarbageRootApi + apiParameters;
            //var web = new HtmlWeb();
            //var doc = web.Load(url);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Settings.GarbageRootApi);
                var result = client.GetAsync(apiParameters).Result;
                if (result.IsSuccessStatusCode)
                    response = result.Content.ReadAsStringAsync().Result;
            }

            response = response.Replace("width:800px", "width:100%;");

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private string GetActualDateJsonFormated()
        {
            var date = DateTime.Now;
            return $"{date.Year}-{date.Month}-{date.Day}T{date.Hour}:{date.Minute}";
        }
    }
}
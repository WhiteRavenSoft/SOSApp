using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace WhiteRaven.API.Core
{
    /// <summary>
    /// Application Configuration
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Para aplicar camelcase... empieza con miniscula
            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new BsonMediaTypeFormatter());
        }
    }
}
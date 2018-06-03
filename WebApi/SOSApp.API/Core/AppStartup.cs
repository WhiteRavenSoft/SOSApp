using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Owin;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using WhiteRaven.API.AppProvider;
using WhiteRaven.Core;
using WhiteRaven.Svc.Infrastructure;

[assembly: OwinStartup(typeof(WhiteRaven.API.Core.AppStartup))]
namespace WhiteRaven.API.Core
{
    /// <summary>
    /// App Startup
    /// </summary>
    public class AppStartup
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 
        /// </summary>
        public static HttpConfiguration HttpConfiguration { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            try
            {
                HttpConfiguration = new HttpConfiguration();

                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;

                ConfigureOAuth(app);

                app.UseCors(CorsOptions.AllowAll);

                //HttpConfiguration.EnableCors();

                //AreaRegistration.RegisterAllAreas();
                AppConfiguration.RegisterApi(HttpConfiguration);
                app.UseWebApi(HttpConfiguration);

                HttpConfiguration.MessageHandlers.Add(new ApiKeyHandler());
                HttpConfiguration.Filters.Add(new CustomExceptionFilter());

                //app.MapSignalR("/websocket", new HubConfiguration());
                var idProvider = new CustomUserProvider();

                IoC.InitializeWith(new DependencyResolverFactory());

                GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

                app.Map("/websocket", map =>
                {
                    // Setup the CORS middleware to run before SignalR.
                    // By default this will allow all origins. You can 
                    // configure the set of origins and/or http verbs by
                    // providing a cors options with a different policy.
                    map.UseCors(CorsOptions.AllowAll);

                    var hubConfiguration = new HubConfiguration
                    {
                        // You can enable JSONP by uncommenting line below.
                        // JSONP requests are insecure but some older browsers (and some
                        // versions of IE) require JSONP to work cross domain
                        // EnableJSONP = true
                    };

                    map.RunSignalR(hubConfiguration);
                });

                
            }
            catch (Exception ex)
            {
                var exception = new AppException(ex.ToString(), ex.InnerException);
                logger.Error(exception.FullDetail);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AppAuthorizationProvider(),
                RefreshTokenProvider = new AppRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
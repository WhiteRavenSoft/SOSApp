using log4net;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SOSApp.Core;
using SOSApp.Core.Enum;
using SOSApp.Core.Helper;
using SOSApp.Data.DBModel;
using SOSApp.Svc.DataService;
using SOSApp.Svc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SOSApp.API.AppProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class AppAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {

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
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                User userDB = null;

                if (context.GrantType.ToLower().Equals("basic"))
                {
                    var email = context.Parameters.Get("email");
                    var password = context.Parameters.Get("password");

                    if (email.NotEmpty() && password.NotEmpty())
                    {
                        if (IoC.Resolve<UserSvc>().ExistsByEmail(email))
                            userDB = await IoC.Resolve<UserSvc>().Login(email, password);
                    }
                }

                if (userDB != null)
                {
                    IoC.Resolve<UserSvc>().Save(userDB);

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    identity.AddClaim(new Claim(ClaimTypes.Email, userDB.Email));
                    identity.AddClaim(new Claim(ClaimTypes.PrimarySid, userDB.ID.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, userDB.RoleId.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, userDB.LastName + ", " + userDB.Name));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                    "given_name", userDB.Name
                                },
                                {
                                    "email", userDB.Email
                                },
                                {
                                    "id", userDB.ID.ToString()
                                }
                            });

                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError(((int)ErrorCodeEnum.InvalidCredentials).ToString(), ErrorCodeEnum.InvalidCredentials.EnumDescription());
                    return;
                }
            }
            catch (Exception ex)
            {
                var exception = new AppException(ex.ToString(), ex.InnerException);
                logger.Error(exception.FullDetail);
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
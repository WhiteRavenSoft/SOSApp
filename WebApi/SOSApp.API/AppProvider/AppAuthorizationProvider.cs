using log4net;
using Microsoft.Owin.Security.OAuth;
using WhiteRaven.Core;
using WhiteRaven.Core.Enum;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WhiteRaven.API.AppProvider
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
                object db = null;

                if (context.GrantType.ToLower().Equals("basic"))
                {
                    var email = context.Parameters.Get("email");
                    var password = context.Parameters.Get("password");

                    if (email.NotEmpty() && password.NotEmpty())
                    {
                        if (IoC.Resolve<UsuarioSvc>().ExistsByEmail(email))
                            db = await IoC.Resolve<UsuarioSvc>().Login(email, password);
                        else
                        {
                            if (IoC.Resolve<EmpresaSvc>().ExistsByEmail(email))
                                db = await IoC.Resolve<EmpresaSvc>().Login(email, password);
                            else
                            {
                                if (IoC.Resolve<VendedorSvc>().ExistsByEmail(email))
                                    db = await IoC.Resolve<VendedorSvc>().Login(email, password);
                                else
                                {
                                    if (IoC.Resolve<InstitucionSocialSvc>().ExistsByEmail(email))
                                        db = await IoC.Resolve<InstitucionSocialSvc>().Login(email, password);
                                }
                            }
                        }
                    }
                }

                var accesstoken = context.Parameters.Get("accesstoken");

                #region Create ClaimsIdentity
                switch (db.GetType().Name)
                {
                    case "Usuario":
                        //Usuario puede generarse desde las redes sociales
                        var userDB = (Usuario)db;

                        if (accesstoken.NotEmpty() && userDB == null)
                        {
                            SocialModel model = await IoC.Resolve<UsuarioSvc>().SocialLogin(context.GrantType.ToLower(), accesstoken);

                            if (!IoC.Resolve<UsuarioSvc>().ExistsByEmail(model.Email, 0))
                            {
                                userDB = new Usuario()
                                {
                                    Nombre = model.Name,
                                    Email = model.Email,
                                    Telefono = string.Empty,
                                    AuthenticationMode = model.AuthMode,
                                    Pwd = string.Empty,
                                    SaltKey = string.Empty,
                                    UsuarioRol = new UsuarioRol() { ID = 1 }, //TODO: Verificar rol id
                                    Active = true,
                                    Deleted = false
                                };

                                userDB = IoC.Resolve<UsuarioSvc>().Save(userDB);

                                var webClient = new WebClient();

                                byte[] imageBytes = webClient.DownloadData(model.Picture);

                                IoC.Resolve<UsuarioSvc>().SavePic(userDB.ID, imageBytes, "");
                            }
                            else
                                userDB = await IoC.Resolve<UsuarioSvc>().FindFirstAsync(u => u.Email == model.Email && u.Active && !u.Deleted && u.AuthenticationMode == model.AuthMode);
                        }

                        if (userDB != null)
                        {
                            userDB.UltimoAcceso = DateTime.UtcNow;
                            IoC.Resolve<UsuarioSvc>().Save(userDB);

                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                            identity.AddClaim(new Claim(ClaimTypes.Email, userDB.Email));
                            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, userDB.ID.ToString()));
                            identity.AddClaim(new Claim("TypeOfUser", "Usuario"));
                            identity.AddClaim(new Claim(ClaimTypes.Role, userDB.ID_RolUsuario.ToString()));
                            identity.AddClaim(new Claim(ClaimTypes.GivenName, userDB.Nombre));

                            context.Validated(identity);
                        }
                        else
                        {
                            context.SetError(((int)ErrorCodeEnum.InvalidCredentials).ToString(), ErrorCodeEnum.InvalidCredentials.EnumDescription());
                            return;
                        }
                        break;
                    case "Empresa":
                        var empresaDB = (Empresa)db;

                        if (empresaDB != null)
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                            identity.AddClaim(new Claim(ClaimTypes.Email, empresaDB.Email));
                            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, empresaDB.ID.ToString()));
                            identity.AddClaim(new Claim("TypeOfUser", "Empresa"));
                            identity.AddClaim(new Claim(ClaimTypes.GivenName, empresaDB.RazonSocial));

                            context.Validated(identity);
                        }
                        else
                        {
                            context.SetError(((int)ErrorCodeEnum.InvalidCredentials).ToString(), ErrorCodeEnum.InvalidCredentials.EnumDescription());
                            return;
                        }
                        break;
                    case "InstitucionSocial":
                        var institucionDB = (InstitucionSocial)db;
                        if (institucionDB != null)
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                            identity.AddClaim(new Claim(ClaimTypes.Email, institucionDB.Email));
                            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, institucionDB.ID.ToString()));
                            identity.AddClaim(new Claim("TypeOfUser", "Institucion"));
                            identity.AddClaim(new Claim(ClaimTypes.GivenName, institucionDB.RazonSocial));

                            context.Validated(identity);
                        }
                        else
                        {
                            context.SetError(((int)ErrorCodeEnum.InvalidCredentials).ToString(), ErrorCodeEnum.InvalidCredentials.EnumDescription());
                            return;
                        }
                        break;
                    case "Vendedor":
                        var vendedorDB = (Vendedor)db;
                        if (vendedorDB != null)
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                            identity.AddClaim(new Claim(ClaimTypes.Email, vendedorDB.Email));
                            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, vendedorDB.ID.ToString()));
                            identity.AddClaim(new Claim("TypeOfUser", "Vendedor"));
                            identity.AddClaim(new Claim(ClaimTypes.GivenName, vendedorDB.Nombre));

                            context.Validated(identity);
                        }
                        else
                        {
                            context.SetError(((int)ErrorCodeEnum.InvalidCredentials).ToString(), ErrorCodeEnum.InvalidCredentials.EnumDescription());
                            return;
                        }

                        break;
                    default:
                        break;
                }
                #endregion
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
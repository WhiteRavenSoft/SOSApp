using Facebook;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WhiteRaven.Core;
using WhiteRaven.Core.Enum;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.Context;
using WhiteRaven.Svc.GenericDataService;
using WhiteRaven.Svc.Infrastructure;

namespace WhiteRaven.Svc.DataService
{
    public class UsuarioSvc : GenericSvc<Usuario, WhiteAdsEntities>
    {
        public Usuario Save(Usuario x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public bool ExistsByEmail(string email, int Usuario)
        {
            var query = from x in Context.Usuario
                        where x.Email.ToLower().Equals(email.ToLower())
                        && x.ID != Usuario
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public bool ExistsByEmail(string email)
        {
            var query = from x in Context.Usuario
                        where x.Email.ToLower().Equals(email.ToLower())
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public async Task<Usuario> Login(string email, string pwd)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            Usuario Usuario = FindSingle(x => x.Email.Equals(email.ToLower()) && !x.Deleted);

            if (Usuario == null)
                return null;

            var realpwd = AppHelper.DecryptText(Usuario.Pwd, Usuario.SaltKey);

            if (!realpwd.Equals(pwd))
                return null;

            if (Usuario.Active && !Usuario.Deleted)
            {
                //Last Access
                Usuario.UltimoAcceso = DateTime.Now;
                IoC.Resolve<UsuarioSvc>().Save(Usuario);

                if (BaseContext.Current.WebSession == null)
                    BaseContext.Current.WebSession = BaseContext.Current.LoadSession(true);

                BaseContext.Current.Usuario = Usuario;
                BaseContext.Current.WebSession.UltimoAccesso = DateTime.UtcNow;
                BaseContext.Current.WebSession.ID_Usuario = Usuario.ID;

                BaseContext.Current.WebSession = IoC.Resolve<UserSessionSvc>().UpdateEntity(BaseContext.Current.WebSession);
            }
            else
            {
                return null;
            }

            return Usuario;
        }

        public Usuario Create(UsuarioModel user)
        {

            if (ExistsByEmail(user.Email, user.ID))
            {
                throw new Exception("El usuario ya se encuentra registrado");
            }

            if (HasAny(u => u.Active && u.Email == user.Email))
            {
                throw new Exception("Ya existe otro usuario con el mismo correo electrónico");
            }

            Usuario newUser = new Usuario()
            {
                Active = true,
                Deleted = false,
                Nombre = user.Nombre,
                Email = user.Email,
                AuthenticationMode = user.AuthenticationMode,
                Telefono = user.Telefono,
                Direccion = user.Direccion,
                UltimoAcceso = DateTime.Now,
                ID_RolUsuario = user.ID_RolUsuario,
            };

            if (((int)AuthMode.Application) != user.AuthenticationMode)
            {
                newUser.Pwd = "";
                newUser.SaltKey = "";
            }
            else
            {
                newUser.SaltKey = AppHelper.CreateSaltKey(10);
                newUser.Pwd = AppHelper.EncryptText(user.Pwd.Trim(), newUser.SaltKey);
            }

            newUser = Save(newUser);

            Save(newUser);

            return newUser;
        }

        public async Task<SocialModel> SocialLogin(string authType, string token)
        {
            if (token.NotEmpty())
            {
                SocialModel model = new SocialModel();

                if (authType == "google")
                {
                    using (var client = new HttpClient())
                    {
                        string url = AppHelper.LoadAppSetting("App.Social.Google.Url") + token;

                        var requestMessage = new HttpRequestMessage
                        {
                            Method = System.Net.Http.HttpMethod.Get,
                            RequestUri = new Uri(url)
                        };

                        requestMessage.Headers.Add("Accept", "application/json");

                        var response = await client.SendAsync(requestMessage);
                        var content = await response.Content.ReadAsStringAsync();

                        model = JsonConvert.DeserializeObject<SocialModel>(content);
                        model.Name = model.Given_name;
                        model.Lastname = model.Family_name;
                        model.AuthMode = (int)AuthenticationModeEnum.Google;
                    }
                }

                if (authType == "facebook")
                {
                    var fb = new FacebookClient(token);

                    dynamic response = await fb.GetTaskAsync("me", new { fields = "email, first_name, last_name" });

                    model.Email = response.email;
                    model.Name = response.first_name;
                    model.Lastname = response.last_name;
                    model.Picture = LoadFacebookPicture(response.id);
                    model.AuthMode = (int)AuthenticationModeEnum.Facebook;
                }

                return model;
            }

            return null;
        }

        private string LoadFacebookPicture(string faceBookId)
        {
            WebResponse response = null;

            string pic = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create(string.Format(AppHelper.LoadAppSetting("AppHelper.Social.Facebook.Picture"), faceBookId));
                response = request.GetResponse();
                pic = response.ResponseUri.ToString();
            }
            catch (Exception ex)
            {
                var exception = new AppException(ex.ToString(), ex.InnerException);
                logger.Error(exception.FullDetail);
            }
            finally
            {
                if (response != null) response.Close();
            }

            return pic;
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Active = false;
                UpdateEntity(x);
            }
        }

        public IQueryable<Usuario> LoadActives()
        {
            var query = from x in Context.Usuario
                        where x.Active && !x.Deleted
                        orderby x.Nombre
                        select x;

            return query;
        }

        public Usuario SavePic(int id, byte[] avatar, string fileName)
        {
            try
            {
                var db = Load(id);

                string folder = string.Format(AppHelper.LoadAppSetting("App.Cdn.Path"), id);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                //db.Avatar = FileHelper.SaveFile(avatar, folder, ".jpg", fileName);

                db = Save(db);

                return db;
            }
            catch (Exception ex)
            {
                var exception = new AppException(ex.ToString(), ex.InnerException);
                logger.Error(exception.FullDetail);
            }

            return null;
        }
    }
}

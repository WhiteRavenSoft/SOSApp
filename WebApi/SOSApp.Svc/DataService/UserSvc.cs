using GoogleMaps.LocationServices;
using SOSApp.Core;
using SOSApp.Core.Enum;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.AvlServiceTest;
using SOSApp.Svc.GenericDataService;
using SOSApp.Svc.Infrastructure;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SOSApp.Svc.DataService
{
    public class UserSvc : GenericSvc<User, SOSAppDBEntities>
    {
        public User Save(User x)
        {
            try
            {
                if (x.ID == 0)
                {
                    x.CreatedDate = DateTime.UtcNow;
                    x.LastUpdate = DateTime.UtcNow;
                    x.Active = true;
                    x.Deleted = false;
                    return CreateEntity(x);
                }

                x.LastUpdate = DateTime.UtcNow;
                return UpdateEntity(x);
            }
            catch (Exception ex)
            {
                var a = ex;
                return null;
            }
        }

        public User Save(UserModel x)
        {
            try
            {
                DateTime? birthdate;
                if (x.Birthdate != null)
                    birthdate = DateTime.Parse(x.Birthdate);
                else
                    birthdate = null;

                if (x.ID == 0)
                {
                    var newUser = new User()
                    {
                        LastName = x.LastName,
                        Name = x.Name,
                        Address = x.Address,
                        Lat = x.Lat,
                        Lon = x.Lon,
                        Birthdate = birthdate,
                        MobileID = x.MobileID,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        Email = x.Email,
                        CreatedDate = DateTime.UtcNow,
                        LastUpdate = DateTime.UtcNow,
                        Active = true,
                        Deleted = false
                    };

                    return CreateEntity(newUser);
                }
                else
                {
                    var db = Load(x.ID);

                    db.LastUpdate = DateTime.UtcNow;
                    db.LastName = x.LastName;
                    db.Name = x.Name;
                    db.Address = x.Address;
                    db.Lat = x.Lat;
                    db.Lon = x.Lon;
                    db.Birthdate = birthdate;
                    db.MobileID = x.MobileID;
                    db.Phone = x.Phone;
                    db.RoleId = x.RoleId;
                    db.Active = x.Active;

                    return UpdateEntity(db);
                }
            }
            catch (Exception ex)
            {
                var a = ex;
                return null;
            }
        }

        public bool ExistsByEmail(string email, int Usuario)
        {
            var query = from x in Context.User
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
            var query = from x in Context.User
                        where x.Email.ToLower().Equals(email.ToLower())
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public async Task<User> Login(string email, string pwd)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            User Usuario = FindSingle(x => x.Email.Equals(email.ToLower()) && !x.Deleted);

            if (Usuario == null)
                return null;

            var realpwd = AppHelper.DecryptText(Usuario.Password, Usuario.Salt);

            if (!realpwd.Equals(pwd))
                return null;

            if (!Usuario.Active || Usuario.Deleted)
                return null;

            return Usuario;
        }

        /// <summary>
        /// Agrega el usuario mobile a una región, y retorna la región a la que pertenece.
        /// </summary>
        /// <param name="value">Objeto que contiene PlayerId de OneSignal y dirección del usuario</param>
        /// <returns>Id de la region a la que pertenece el usuario</returns>
        public int CreateMobile(UserMobileModel value)
        {
            double latitude = 0, longitude = 0;
            int RegionId = GetRegionId(value, ref latitude, ref longitude);

            if (RegionId != 0)
            {
                var user = LoadByPlayerId(value.PlayerID);
                if (user == null)
                {
                    user = Save(new User()
                    {
                        Address = value.Address,
                        Lat = latitude,
                        Lon = longitude,
                        RoleId = (int)UserRoleEnum.UserMobile,
                        MobileID = value.PlayerID,
                        Active = true
                    });
                }
                else
                {
                    user.Address = value.Address;
                    user.MobileID = value.PlayerID;
                    user.LastUpdate = DateTime.UtcNow;

                    UpdateEntity(user);
                }

                IoC.Resolve<UserByUserGroupSvc>().DeleteByUser(user.ID);
                IoC.Resolve<UserByUserGroupSvc>().Save(new UsersByUserGroup()
                {
                    UserID = user.ID,
                    UserGroupID = RegionId,
                    LastUpdate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Deleted = false,
                    Active = true
                });
            }

            return RegionId;
        }

        private User LoadByPlayerId(string playerID)
        {
            var query = from x in Context.User
                        where x.Active && !x.Deleted
                        && x.MobileID == playerID
                        select x;

            return query.FirstOrDefault();
        }

        private static int GetRegionId(UserMobileModel value, ref double latitude, ref double longitude)
        {
            try
            {
                string URL = "http://maps.googleapis.com/maps/api/geocode/xml"; 
                string urlParameters = $"?address={value.Address}, Suncheles, Santa Fe, Argentina&sensor=false";

                HttpClient client = new HttpClient
                { BaseAddress = new Uri(URL) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GeocodeResponse));
                    GeocodeResponse resultingMessage = (GeocodeResponse)serializer.Deserialize(new XmlTextReader(response.Content.ReadAsStreamAsync().Result));

                    latitude = double.Parse(resultingMessage.Result.Geometry.Location.Lat.Replace('.',','));
                    longitude = double.Parse(resultingMessage.Result.Geometry.Location.Lng.Replace('.', ','));

                    if (latitude == Settings.ExcludeLatitude && longitude == Settings.ExcludeLongitude)
                        return 0;

                    //Consulta a la API de AVL la región a la que pertenecen las coordenadas
                    AvlSoapClient avlClient = new AvlSoapClient();
                    var avlResponse = avlClient.ObtenerRegionesActualesPorCoordenada(6, latitude, longitude);

                    if (avlResponse.Rows.Count > 0)
                        if (avlResponse.Rows[0].ItemArray.Count() > 0)
                            return int.Parse(avlResponse.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    return 0;
                }

                client.Dispose();
            }
            catch (Exception ex)
            {
                var a = ex;
                return 0;
            }

            return 0;
        }

        public User Create(UserModel user)
        {
            if (ExistsByEmail(user.Email, user.ID))
                throw new Exception("El usuario ya se encuentra registrado");

            if (HasAny(u => u.Active && u.Email == user.Email))
                throw new Exception("Ya existe otro usuario con el mismo correo electrónico");

            DateTime? birthdate;
            if (user.Birthdate != null)
                birthdate = DateTime.Parse(user.Birthdate);
            else
                birthdate = null;

            User newUser = new User()
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Birthdate = birthdate,
                Address = user.Address,
                RoleId = user.RoleId,
                CreatedDate = DateTime.UtcNow,
                LastUpdate = DateTime.UtcNow,
                Active = true,
                Deleted = false
            };

            newUser.Salt = AppHelper.CreateSaltKey(10);
            newUser.Password = AppHelper.EncryptText(user.Password.Trim(), newUser.Salt);

            newUser = Save(newUser);

            Save(newUser);

            return newUser;
        }

        public void ChangePass(int id, UserChangePasswordModel value)
        {
            string newSalt = AppHelper.CreateSaltKey(10);
            string newPassword = AppHelper.EncryptText(value.Password.Trim(), newSalt);

            var x = Load(id);
            x.Salt = newSalt;
            x.Password = newPassword;
            x.LastUpdate = DateTime.UtcNow;
            UpdateEntity(x);
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

        public IQueryable<User> LoadActives()
        {
            var query = from x in Context.User
                        where x.Active && !x.Deleted
                        orderby x.LastName, x.Name
                        select x;

            return query;
        }

        public IQueryable<User> LoadNonClients()
        {
            var query = from x in Context.User
                        where x.Active && !x.Deleted && x.UserRole.ID != 3
                        orderby x.LastName, x.Name
                        select x;

            return query;
        }

        public User SavePic(int id, byte[] avatar, string fileName)
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

﻿using SOSApp.Core;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.GenericDataService;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOSApp.Svc.DataService
{
    public class UserSvc : GenericSvc<User, SOSAppDBEntities>
    {
        public User Save(User x)
        {
            if (x.ID == 0)
            {
                x.CreatedDate = DateTime.Now;
                x.LastUpdate = DateTime.Now;
                x.Active = true;
                x.Deleted = false;
                return CreateEntity(x);
            }

            x.LastUpdate = DateTime.Now;
            return UpdateEntity(x);
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

        public User Create(UserModel user)
        {

            if (ExistsByEmail(user.Email, user.ID))
                throw new Exception("El usuario ya se encuentra registrado");

            if (HasAny(u => u.Active && u.Email == user.Email))
                throw new Exception("Ya existe otro usuario con el mismo correo electrónico");

            User newUser = new User()
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                RoleId = user.RoleId,
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,
                Active = true,
                Deleted = false
            };

            newUser.Salt = AppHelper.CreateSaltKey(10);
            newUser.Password = AppHelper.EncryptText(user.Password.Trim(), newUser.Salt);

            newUser = Save(newUser);

            Save(newUser);

            return newUser;
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
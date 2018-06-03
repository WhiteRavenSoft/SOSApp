using System;
using System.Linq;
using System.Threading.Tasks;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;
using WhiteRaven.Svc.Infrastructure;

namespace WhiteRaven.Svc.DataService
{
    public class InstitucionSocialSvc : GenericSvc<InstitucionSocial, WhiteAdsEntities>
    {
        public InstitucionSocial Save(InstitucionSocial x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<InstitucionSocial> Load()
        {
            var query = from x in Context.InstitucionSocial
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<InstitucionSocial> LoadCity(int ciudad)
        {
            var query = from x in Context.InstitucionSocial
                        where !x.Deleted
                        && x.Active
                        && x.ID_Ciudad == ciudad
                        orderby x.ID
                        select x;

            return query;
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = base.Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Active = false;
                UpdateEntity(x);
            }
        }

        public async Task<InstitucionSocial> Login(string email, string pwd)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            InstitucionSocial institucion = FindSingle(x => x.Email.Equals(email.ToLower()) && !x.Deleted);

            if (institucion == null)
                return null;

            var realpwd = AppHelper.DecryptText(institucion.Password, institucion.Salt);

            if (!realpwd.Equals(pwd))
                return null;

            if (institucion.Active && !institucion.Deleted)
                return institucion;
            else
                return null;
        }

        public bool ExistsByEmail(string email, long Institucion)
        {
            var query = from x in Context.InstitucionSocial
                        where x.Email.ToLower().Equals(email.ToLower())
                        && x.ID != Institucion
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public bool ExistsByEmail(string email)
        {
            var query = from x in Context.InstitucionSocial
                        where x.Email.ToLower().Equals(email.ToLower())
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public InstitucionSocial Create(InstitucionSocialModel institucion)
        {
            if (ExistsByEmail(institucion.Email, institucion.ID))
            {
                throw new Exception("La institucion ya se encuentra registrada");
            }

            if (HasAny(u => u.Active && u.Email == institucion.Email))
            {
                throw new Exception("Ya existe otra institucion con el mismo correo electrónico");
            }

            InstitucionSocial newInstitucion = new InstitucionSocial()
            {
                RazonSocial = institucion.RazonSocial,
                CUIT = institucion.CUIT,
                Email = institucion.Email,
                Telefono = institucion.Telefono,
                Direccion = institucion.Direccion,
                FechaAlta = DateTime.Now,
                PerfilCabecera = institucion.PerfilCabecera,
                PerfilLogo = institucion.PerfilLogo,
                ID_Ciudad = institucion.ID_Ciudad,
                Web = institucion.Web,
                Mapa_Latitud = institucion.Mapa_Latitud,
                Mapa_Longitud = institucion.Mapa_Longitud,
                Active = true,
                Deleted = false,
                TS = DateTime.Now
            };

            newInstitucion.Salt = AppHelper.CreateSaltKey(10);
            newInstitucion.Password = AppHelper.EncryptText(institucion.Password.Trim(), newInstitucion.Salt);
            newInstitucion = Save(newInstitucion);

            return newInstitucion;
        }
    }
}

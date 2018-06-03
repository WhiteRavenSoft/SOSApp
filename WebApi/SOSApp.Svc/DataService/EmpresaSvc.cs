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
    public class EmpresaSvc : GenericSvc<Empresa, WhiteAdsEntities>
    {
        public Empresa Save(Empresa x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
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

        public IQueryable<Empresa> Load()
        {
            var query = from x in Context.Empresa
                        where !x.Deleted
                        orderby x.RazonSocial
                        select x;

            return query;
        }

        public IQueryable<Empresa> LoadActives()
        {
            var query = from x in Context.Empresa
                        where !x.Deleted
                        && x.Active
                        select x;

            return query;
        }

        public async Task<Empresa> Login(string email, string pwd)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            Empresa empresa = FindSingle(x => x.Email.Equals(email.ToLower()) && !x.Deleted);

            if (empresa == null)
                return null;

            var realpwd = AppHelper.DecryptText(empresa.Password, empresa.Salt);

            if (!realpwd.Equals(pwd))
                return null;

            if (empresa.Active && !empresa.Deleted)
                return empresa;
            else
                return null;
        }

        public bool ExistsByEmail(string email, long idEmpresa)
        {
            var query = from x in Context.Empresa
                        where x.Email.ToLower().Equals(email.ToLower())
                        && x.ID != idEmpresa
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public bool ExistsByEmail(string email)
        {
            var query = from x in Context.Empresa
                        where x.Email.ToLower().Equals(email.ToLower())
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public Empresa Create(EmpresaModel empresa)
        {
            if (ExistsByEmail(empresa.Email, empresa.ID))
            {
                throw new Exception("La empresa ya se encuentra registrada");
            }

            if (HasAny(u => u.Active && u.Email == empresa.Email))
            {
                throw new Exception("Ya existe otra empresa con el mismo correo electrónico");
            }

            Empresa newEmpresa = new Empresa()
            {
                RazonSocial = empresa.RazonSocial,
                CUIT = empresa.CUIT,
                Email = empresa.Email,
                Telefono = empresa.Telefono,
                Direccion = empresa.Direccion,
                FechaAlta = DateTime.Now,
                PerfilCabecera = empresa.PerfilCabecera,
                PerfilLogo = empresa.PerfilLogo,
                GoogleMaps = empresa.GoogleMaps,
                ID_Rubro = empresa.ID_Rubro,
                ID_Plan = empresa.ID_Plan,
                ID_Ciudad = empresa.ID_Ciudad,
                W3w = empresa.W3w,
                Web = empresa.Web,
                Mapa_Latitud = empresa.Mapa_Latitud,
                Mapa_Longitud = empresa.Mapa_Longitud,
                MostrarCalificaciones = true,
                Active = true,
                Deleted = false,
                TS = DateTime.Now
            };

            newEmpresa.Salt = AppHelper.CreateSaltKey(10);
            newEmpresa.Password = AppHelper.EncryptText(empresa.Password.Trim(), newEmpresa.Salt);
            newEmpresa = Save(newEmpresa);

            return newEmpresa;
        }
    }
}
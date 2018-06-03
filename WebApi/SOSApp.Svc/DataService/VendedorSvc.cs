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
    public class VendedorSvc : GenericSvc<Vendedor, WhiteAdsEntities>
    {
        public Vendedor Save(Vendedor x)
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

        public IQueryable<Vendedor> Load()
        {
            var query = from x in Context.Vendedor
                        where !x.Deleted
                        && x.Active
                        orderby x.Apellido
                        select x;

            return query;
        }

        public IQueryable<Vendedor> LoadActives()
        {
            var query = from x in Context.Vendedor
                        where !x.Deleted
                        && x.Active
                        select x;

            return query;
        }

        public async Task<Vendedor> Login(string email, string pwd)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            Vendedor vendedor = FindSingle(x => x.Email.Equals(email.ToLower()) && !x.Deleted);

            if (vendedor == null)
                return null;

            var realpwd = AppHelper.DecryptText(vendedor.Password, vendedor.Salt);

            if (!realpwd.Equals(pwd))
                return null;

            if (vendedor.Active && !vendedor.Deleted)
                return vendedor;
            else
                return null;
        }

        public bool ExistsByEmail(string email, long idVendedor)
        {
            var query = from x in Context.Vendedor
                        where x.Email.ToLower().Equals(email.ToLower())
                        && x.ID != idVendedor
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public bool ExistsByEmail(string email)
        {
            var query = from x in Context.Vendedor
                        where x.Email.ToLower().Equals(email.ToLower())
                        && !x.Deleted
                        select x;


            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public Vendedor Create(VendedorModel vendedor)
        {

            if (ExistsByEmail(vendedor.Email, vendedor.ID))
            {
                throw new Exception("El vendedor ya se encuentra registrado");
            }

            if (HasAny(u => u.Active && u.Email == vendedor.Email))
            {
                throw new Exception("Ya existe otro vendedor con el mismo correo electrónico");
            }

            Vendedor newVendedor = new Vendedor()
            {
                Apellido = vendedor.Apellido,
                Nombre = vendedor.Nombre,
                Avatar = vendedor.Avatar,
                CUIT = vendedor.CUIT,
                Email = vendedor.Email,
                Telefono = vendedor.Telefono,
                Direccion = vendedor.Direccion,
                ID_Ciudad = vendedor.ID_Ciudad,
                Active = true,
                Deleted = false,
                TS = DateTime.Now
            };

            newVendedor.Salt = AppHelper.CreateSaltKey(10);
            newVendedor.Password = AppHelper.EncryptText(vendedor.Password.Trim(), newVendedor.Salt);
            newVendedor = Save(newVendedor);

            return newVendedor;
        }
    }
}
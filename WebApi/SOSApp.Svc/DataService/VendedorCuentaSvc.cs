using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class VendedorCuentaSvc : GenericSvc<VendedorCuenta, WhiteAdsEntities>
    {
        public VendedorCuenta Save(VendedorCuenta x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<VendedorCuenta> Load()
        {
            var query = from x in Context.VendedorCuenta
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<VendedorCuenta> LoadSeller(int vendedor)
        {
            var query = from x in Context.VendedorCuenta
                        where !x.Deleted
                        && x.Active
                        && x.ID_Vendedor == vendedor
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
    }
}

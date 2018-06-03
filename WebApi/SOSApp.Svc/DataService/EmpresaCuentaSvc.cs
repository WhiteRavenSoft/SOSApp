using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaCuentaSvc : GenericSvc<EmpresaCuenta, WhiteAdsEntities>
    {
        public EmpresaCuenta Save(EmpresaCuenta x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaCuenta> Load()
        {
            var query = from x in Context.EmpresaCuenta
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<EmpresaCuenta> LoadCompany(int empresa)
        {
            var query = from x in Context.EmpresaCuenta
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa
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

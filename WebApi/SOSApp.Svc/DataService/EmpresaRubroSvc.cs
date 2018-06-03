using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaRubroSvc : GenericSvc<EmpresaRubro, WhiteAdsEntities>
    {
        public EmpresaRubro Save(EmpresaRubro x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaRubro> Load()
        {
            var query = from x in Context.EmpresaRubro
                        where !x.Deleted
                         && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }

        public IQueryable<EmpresaRubro> LoadById(int id)
        {
            var query = from x in Context.EmpresaRubro
                        where !x.Deleted
                        && x.Active
                        && x.ID == id
                        orderby x.Nombre
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

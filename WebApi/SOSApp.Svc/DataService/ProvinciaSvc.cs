using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class ProvinciaSvc : GenericSvc<Provincia, WhiteAdsEntities>
    {
        public Provincia Save(Provincia x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Provincia> Load()
        {
            var query = from x in Context.Provincia
                        where !x.Deleted
                         && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }

        public IQueryable<Provincia> LoadCountry(int pais)
        {
            var query = from x in Context.Provincia
                        where !x.Deleted
                        && x.Active
                        && x.ID_Pais == pais
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

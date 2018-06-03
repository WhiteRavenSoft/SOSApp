using WhiteRaven.Svc.GenericDataService;
using WhiteRaven.Data.DBModel;
using System.Linq;
using System;

namespace WhiteRaven.Svc.DataService
{
    public class MonedaSvc : GenericSvc<Moneda, WhiteAdsEntities>
    {
        public Moneda Save(Moneda x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Moneda> Load()
        {
            var query = from x in Context.Moneda
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<Moneda> LoadActives()
        {
            var query = from x in Context.Moneda
                        where x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<Moneda> LoadPublished()
        {
            var query = from x in Context.Moneda
                        where x.Active
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

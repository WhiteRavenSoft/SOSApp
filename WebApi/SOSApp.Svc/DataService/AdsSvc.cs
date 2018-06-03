using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class AdsSvc : GenericSvc<Ads, WhiteAdsEntities>
    {
        public Ads Save(Ads x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Ads> Load()
        {
            var query = from x in Context.Ads
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<Ads> LoadByCompany(int empresa)
        {
            var query = from x in Context.Ads
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa
                        orderby x.Empresa
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

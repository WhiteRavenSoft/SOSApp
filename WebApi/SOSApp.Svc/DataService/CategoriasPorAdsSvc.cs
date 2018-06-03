using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class CategoriasPorAdsSvc : GenericSvc<CategoriasPorAds, WhiteAdsEntities>
    {
        public CategoriasPorAds Save(CategoriasPorAds x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<CategoriasPorAds> Load()
        {
            var query = from x in Context.CategoriasPorAds
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<CategoriasPorAds> Load(int ads, int categoria)
        {
            var query = from x in Context.CategoriasPorAds
                        where !x.Deleted
                        && x.Active
                        && x.ID_Ads == ads && x.ID_Categoria == categoria
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

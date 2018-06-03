using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class AdsCategoriaSvc : GenericSvc<AdsCategoria, WhiteAdsEntities>
    {
        public AdsCategoria Save(AdsCategoria x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<AdsCategoria> Load()
        {
            var query = from x in Context.AdsCategoria
                        where !x.Deleted
                         && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }

        public IQueryable<AdsCategoria> LoadByParent(int categoriaPadre)
        {
            var query = from x in Context.AdsCategoria
                        where !x.Deleted
                        && x.Active
                        && x.ID_CategoriaPadre == categoriaPadre
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

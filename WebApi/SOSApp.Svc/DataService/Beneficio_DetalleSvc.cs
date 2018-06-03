using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class Beneficio_DetalleSvc : GenericSvc<Beneficio_Detalle, WhiteAdsEntities>
    {
        public Beneficio_Detalle Save(Beneficio_Detalle x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Beneficio_Detalle> Load()
        {
            var query = from x in Context.Beneficio_Detalle
                        where !x.Deleted
                         && x.Active
                        orderby x.Fecha
                        select x;

            return query;
        }

        public IQueryable<Beneficio_Detalle> LoadBySeller(int vendedor)
        {
            var query = from x in Context.Beneficio_Detalle
                        where !x.Deleted
                        && x.Active
                        && x.ID_Vendedor == vendedor
                        orderby x.Fecha
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

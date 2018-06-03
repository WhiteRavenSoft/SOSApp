using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class HorariosSvc : GenericSvc<Horarios, WhiteAdsEntities>
    {
        public Horarios Save(Horarios x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Horarios> Load()
        {
            var query = from x in Context.Horarios
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<Horarios> LoadCompany(int empresa)
        {
            var query = from x in Context.Horarios
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

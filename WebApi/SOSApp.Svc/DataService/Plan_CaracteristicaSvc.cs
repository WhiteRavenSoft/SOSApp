using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class Plan_CaracteristicaSvc : GenericSvc<Plan_Caracteristica, WhiteAdsEntities>
    {
        public Plan_Caracteristica Save(Plan_Caracteristica x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Plan_Caracteristica> Load()
        {
            var query = from x in Context.Plan_Caracteristica
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<Plan_Caracteristica> LoadPlan(int plan)
        {
            var query = from x in Context.Plan_Caracteristica
                        where !x.Deleted
                        && x.Active
                        && x.ID_Plan == plan
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

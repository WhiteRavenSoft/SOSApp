using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class CalificacionSvc : GenericSvc<Calificacion, WhiteAdsEntities>
    {
        public Calificacion Save(Calificacion x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Calificacion> Load()
        {
            var query = from x in Context.Calificacion
                        where !x.Deleted
                         && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }

        public IQueryable<Calificacion> LoadCompany(int empresa)
        {
            var query = from x in Context.Calificacion
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa
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

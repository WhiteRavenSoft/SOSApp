using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class UsuarioAccionSvc : GenericSvc<UsuarioAccion, WhiteAdsEntities>
    {
        public UsuarioAccion Save(UsuarioAccion x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Active = false;
                UpdateEntity(x);
            }
        }

        public IQueryable<UsuarioAccion> Load()
        {
            var query = from x in Context.UsuarioAccion
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }
    }
}
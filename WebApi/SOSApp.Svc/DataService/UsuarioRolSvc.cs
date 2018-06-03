using WhiteRaven.Svc.GenericDataService;
using WhiteRaven.Data.DBModel;
using System.Linq;

namespace WhiteRaven.Svc.DataService
{
    public class UsuarioRolSvc : GenericSvc<UsuarioRol, WhiteAdsEntities>
    {
        public UsuarioRol Save(UsuarioRol x)
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

        public bool Exists(string name, int id)
        {
            var query = from x in Context.UsuarioRol
                        where x.Nombre.ToLower().Equals(name.ToLower())
                        && x.ID != id
                        && !x.Deleted
                        select x;

            if (query.FirstOrDefault() == null)
                return false;

            return true;
        }

        public IQueryable<UsuarioRol> Load()
        {
            var query = from x in Context.UsuarioRol
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }
    }
}

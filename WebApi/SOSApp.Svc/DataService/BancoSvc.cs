using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class BancoSvc : GenericSvc<Banco, WhiteAdsEntities>
    {
        public Banco Save(Banco x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<Banco> LoadActives()
        {
            var query = from x in Context.Banco
                        where !x.Deleted
                        orderby x.ID
                        select x;

            return query;
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

        public IQueryable<Banco> LoadPublished()
        {
            var query = from x in Context.Banco
                        where !x.Deleted
                        && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }
    }
}

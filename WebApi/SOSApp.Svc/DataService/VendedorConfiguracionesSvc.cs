using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class VendedorConfiguracionesSvc : GenericSvc<VendedorConfiguraciones, WhiteAdsEntities>
    {
        public VendedorConfiguraciones Save(VendedorConfiguraciones x)
        {
            //Estandariza el formato de las keys
            x.StringKey = x.StringKey.Trim().ToUpper();

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<VendedorConfiguraciones> LoadActives(int vendedor)
        {
            var query = from x in Context.VendedorConfiguraciones
                        where x.ID_Vendedor == vendedor
                        && !x.Deleted
                        orderby x.ID
                        select x;
            return query;
        }

        public IQueryable<VendedorConfiguraciones> LoadPublished(int vendedor)
        {
            var query = from x in Context.VendedorConfiguraciones
                        where x.ID_Vendedor == vendedor
                        && !x.Deleted
                        && x.Published
                        select x;

            return query;
        }

        public VendedorConfiguraciones LoadByKey(int vendedor, string key)
        {
            var query = from x in Context.VendedorConfiguraciones
                        where x.ID_Vendedor == vendedor
                        && x.StringKey == key
                        && !x.Deleted
                        && x.Published
                        select x;

            return query.FirstOrDefault();
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Published = false;
                UpdateEntity(x);
            }
        }
    }
}

using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaConfiguracionesSvc : GenericSvc<EmpresaConfiguraciones, WhiteAdsEntities>
    {
        public EmpresaConfiguraciones Save(EmpresaConfiguraciones x)
        {
            //Estandariza el formato de las keys
            x.StringKey = x.StringKey.Trim().ToUpper();

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaConfiguraciones> LoadActives(int company)
        {
            var query = from x in Context.EmpresaConfiguraciones
                        where x.ID_Empresa == company
                        && !x.Deleted
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<EmpresaConfiguraciones> LoadPublished(int company)
        {
            var query = from x in Context.EmpresaConfiguraciones
                        where x.ID_Empresa == company 
                        && !x.Deleted
                        && x.Published
                        select x;

            return query;
        }

        public EmpresaConfiguraciones LoadByKey(int company, string key)
        {
            var query = from x in Context.EmpresaConfiguraciones
                        where x.ID_Empresa == company
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

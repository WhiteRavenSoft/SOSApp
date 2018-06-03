using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class InstitucionSocialConfiguracionesSvc : GenericSvc<InstitucionSocialConfiguraciones, WhiteAdsEntities>
    {
        public InstitucionSocialConfiguraciones Save(InstitucionSocialConfiguraciones x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<InstitucionSocialConfiguraciones> Load()
        {
            var query = from x in Context.InstitucionSocialConfiguraciones
                        //where !x.Deleted
                        // //&& x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<InstitucionSocialConfiguraciones> LoadSocialinstitution(int institucionSocial)
        {
            var query = from x in Context.InstitucionSocialConfiguraciones
                            
                            //TODO: revisar este método... @Ezequiel

                            //where !x.Deleted
                            //&& x.Active
                            //&& x.ID_InstitucionSocial == institucionSocial
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
                x.Published = false;
                UpdateEntity(x);
            }
        }

    }
}

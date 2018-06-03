using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class InstitucionSocialCuentaSvc : GenericSvc<InstitucionSocialCuenta, WhiteAdsEntities>
    {
        public InstitucionSocialCuenta Save(InstitucionSocialCuenta x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<InstitucionSocialCuenta> Load()
        {
            var query = from x in Context.InstitucionSocialCuenta
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<InstitucionSocialCuenta> LoadSocialInstitution(int institucionSocial)
        {
            var query = from x in Context.InstitucionSocialCuenta
                        where !x.Deleted
                        && x.Active
                        && x.ID_InstitucionSocial == institucionSocial
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

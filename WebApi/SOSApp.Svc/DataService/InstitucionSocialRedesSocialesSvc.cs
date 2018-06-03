using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class InstitucionSocialRedesSocialesSvc : GenericSvc<InstitucionSocialRedesSociales, WhiteAdsEntities>
    {
        public InstitucionSocialRedesSociales Save(InstitucionSocialRedesSociales x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<InstitucionSocialRedesSociales> Load()
        {
            var query = from x in Context.InstitucionSocialRedesSociales
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<InstitucionSocialRedesSociales> Load(int institucionSocial, int redSocial)
        {
            var query = from x in Context.InstitucionSocialRedesSociales
                        where !x.Deleted
                        && x.Active
                        && x.ID_InstitucionSocial == institucionSocial && x.ID_RedSocial == redSocial
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

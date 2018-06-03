using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaRedesSocialesSvc : GenericSvc<EmpresaRedesSociales, WhiteAdsEntities>
    {
        public EmpresaRedesSociales Save(EmpresaRedesSociales x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaRedesSociales> Load()
        {
            var query = from x in Context.EmpresaRedesSociales
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<EmpresaRedesSociales> Load(int empresa, int redSocial)
        {
            var query = from x in Context.EmpresaRedesSociales
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa && x.ID_RedSocial == redSocial
                        orderby x
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

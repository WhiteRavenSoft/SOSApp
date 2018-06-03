using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class VendedorRedesSocialesSvc : GenericSvc<VendedorRedesSociales, WhiteAdsEntities>
    {
        public VendedorRedesSociales Save(VendedorRedesSociales x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<VendedorRedesSociales> Load()
        {
            var query = from x in Context.VendedorRedesSociales
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<VendedorRedesSociales> Load(int redSocial, int vendedor)
        {
            var query = from x in Context.VendedorRedesSociales
                        where !x.Deleted
                        && x.Active
                        && x.ID_RedSocial == redSocial && x.ID_Vendedor == vendedor
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

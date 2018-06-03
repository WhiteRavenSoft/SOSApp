using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class FormaPagoVendedorSvc : GenericSvc<FormaPagoVendedor, WhiteAdsEntities>
    {
        public FormaPagoVendedor Save(FormaPagoVendedor x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<FormaPagoVendedor> Load()
        {
            var query = from x in Context.FormaPagoVendedor
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<FormaPagoVendedor> Load(int formaPago, int vendedor)
        {
            var query = from x in Context.FormaPagoVendedor
                        where !x.Deleted
                        && x.Active
                        && x.ID_FormaPago == formaPago && x.ID_Vendedor == vendedor
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

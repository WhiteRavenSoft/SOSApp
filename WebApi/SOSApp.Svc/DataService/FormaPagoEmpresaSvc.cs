using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class FormaPagoEmpresaSvc : GenericSvc<FormaPagoEmpresa, WhiteAdsEntities>
    {
        public FormaPagoEmpresa Save(FormaPagoEmpresa x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<FormaPagoEmpresa> Load()
        {
            var query = from x in Context.FormaPagoEmpresa
                        where !x.Deleted
                         && x.Active
                        orderby x.ID
                        select x;

            return query;
        }

        public IQueryable<FormaPagoEmpresa> Load(int empresa, int formaPago)
        {
            var query = from x in Context.FormaPagoEmpresa
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa && x.ID_FormaPago == formaPago
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

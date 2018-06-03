using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaHorariosEspecialesSvc : GenericSvc<EmpresaHorariosEspeciales, WhiteAdsEntities>
    {
        public EmpresaHorariosEspeciales Save(EmpresaHorariosEspeciales x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaHorariosEspeciales> Load()
        {
            var query = from x in Context.EmpresaHorariosEspeciales
                        where !x.Deleted
                         && x.Active
                        orderby x.Abierto
                        select x;

            return query;
        }

        public IQueryable<EmpresaHorariosEspeciales> LoadCompany(int empresa)
        {
            var query = from x in Context.EmpresaHorariosEspeciales
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa
                        orderby x.Abierto
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

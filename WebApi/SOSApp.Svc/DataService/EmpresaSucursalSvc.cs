﻿using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class EmpresaSucursalSvc : GenericSvc<EmpresaSucursal, WhiteAdsEntities>
    {
        public EmpresaSucursal Save(EmpresaSucursal x)
        {
            x.TS = DateTime.Now;

            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<EmpresaSucursal> Load()
        {
            var query = from x in Context.EmpresaSucursal
                        where !x.Deleted
                         && x.Active
                        orderby x.Nombre
                        select x;

            return query;
        }

        public IQueryable<EmpresaSucursal> LoadCompany(int empresa)
        {
            var query = from x in Context.EmpresaSucursal
                        where !x.Deleted
                        && x.Active
                        && x.ID_Empresa == empresa
                        orderby x.Nombre
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

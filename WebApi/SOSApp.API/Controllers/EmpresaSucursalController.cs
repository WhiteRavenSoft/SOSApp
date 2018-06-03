﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhiteRaven.API.Core;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;

namespace WhiteAds.API.Controllers
{
    /// <summary>
    /// Controlador de Empresa Sucursal
    /// </summary>
    public class EmpresaSucursalController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Vendedor
        /// </summary>
        /// <returns></returns>
        // GET: api/EmpresaSucursal
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<EmpresaSucursalModel>> response = new AppPagedResponse<List<EmpresaSucursalModel>>() { Data = null };

            var db = empresaSucursalSvc.LoadCompany(1);

            //if (filter != string.Empty)
            //{
            //    filters = JsonConvert.DeserializeObject<List<GridFilter>>(filter);
            //    db = ApplyFilters(db, filters);
            //}


            //if (sort != string.Empty)
            //{
            //    sorts = JsonConvert.DeserializeObject<List<GridSort>>(sort);
            //    db = ApplySorts(db, sorts);
            //}
            //else
            //    db = db.OrderBy(x => x.Apellido);

            response.Total = db.Count();
            db = db.Skip(start.Value).Take(limit.Value);
            var model = MapToModel(db.ToList());
            response.Data = model;
            response.Limit = limit.Value;
            response.Filter = filters;
            response.Sort = sorts;

            //Calculate actual page
            int intPage = limit.Value == 0 ? 0 : (int)Math.Ceiling((decimal)(start.Value / limit.Value));
            response.Page = intPage + 1;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Mostrar EmpresaSucursal por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/EmpresaSucursal/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<EmpresaSucursalModel> response = new AppResponse<EmpresaSucursalModel>() { Data = null };
            var db = empresaSucursalSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar EmpresaSucursal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/EmpresaSucursal
        public HttpResponseMessage Post([FromBody] EmpresaSucursalModel value)
        {
            AppResponse<EmpresaSucursal> response = new AppResponse<EmpresaSucursal>() { Data = null };
            var db = empresaSucursalSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        ///  Método para Modificar EmpresaSucursal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/EmpresaSucursal/5
        public HttpResponseMessage Put(int id, [FromBody] EmpresaSucursalModel value)
        {
            AppResponse<EmpresaSucursal> response = new AppResponse<EmpresaSucursal>() { Data = null };
            var db = empresaSucursalSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        ///  Método para Eliminar EmpresaSucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/EmpresaSucursal/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            empresaSucursalSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

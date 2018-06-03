using Newtonsoft.Json;
using System;
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
    /// Controlador de Vendedor
    /// </summary>
    public class VendedorController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Vendedor
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Vendedor
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<VendedorModel>> response = new AppPagedResponse<List<VendedorModel>>() { Data = null };

            var db = vendedorSvc.Load();

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

        private IQueryable<Vendedor> ApplyFilters(IQueryable<Vendedor> db, List<GridFilter> filters)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Vendedor> ApplySorts(IQueryable<Vendedor> db, List<GridSort> sorts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método para Mostrar Vendedor por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Vendedor/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<VendedorModel> response = new AppResponse<VendedorModel>() { Data = null };
            var db = vendedorSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Vendedor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //  POST: api/Vendedor 
        public HttpResponseMessage Post([FromBody] VendedorModel value)
        {
            AppResponse<Vendedor> response = new AppResponse<Vendedor>() { Data = null };
            var db = vendedorSvc.Create(value);
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Vendedor/5
        public HttpResponseMessage Put(int id, [FromBody] VendedorModel value)
        {
            AppResponse<Vendedor> response = new AppResponse<Vendedor>() { Data = null };
            var db = vendedorSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar Vendedor
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Vendedor/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            vendedorSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

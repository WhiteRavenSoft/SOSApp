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
    /// Controlador de Ventas
    /// </summary>
    public class VentasController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Ventas
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Ventas
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<VentasModel>> response = new AppPagedResponse<List<VentasModel>>() { Data = null };

            var db = ventasSvc.Load();

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
        /// Método para Mostrar Ventas por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Ventas/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<VentasModel> response = new AppResponse<VentasModel>() { Data = null };
            var db = ventasSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Ventas
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //  POST: api/Ventas 
        public HttpResponseMessage Post([FromBody] VentasModel value)
        {
            AppResponse<Ventas> response = new AppResponse<Ventas>() { Data = null };
            var db = ventasSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Ventas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Ventas/5
        public HttpResponseMessage Put(int id, [FromBody] VentasModel value)
        {
            AppResponse<Ventas> response = new AppResponse<Ventas>() { Data = null };
            var db = ventasSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar Ventas
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Ventas/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            ventasSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

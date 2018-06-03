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
    /// Controlador de Beneficio_Detalle
    /// </summary>
    public class Beneficio_DetalleController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Beneficio_Detalle
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Beneficio_Detalle
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<Beneficio_DetalleModel>> response = new AppPagedResponse<List<Beneficio_DetalleModel>>() { Data = null };

            var db = beneficio_DetalleSvc.Load();

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
        /// Método para Mostrar Beneficio_Detalle por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Beneficio_Detalle/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<Beneficio_DetalleModel> response = new AppResponse<Beneficio_DetalleModel>() { Data = null };
            var db = beneficio_DetalleSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Beneficio_Detalle
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Beneficio_Detalle
        public HttpResponseMessage Post([FromBody] Beneficio_DetalleModel value)
        {
            AppResponse<Beneficio_Detalle> response = new AppResponse<Beneficio_Detalle>() { Data = null };
            var db = beneficio_DetalleSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Beneficio_Detalle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Beneficio_Detalle/5
        public HttpResponseMessage Put(int id, [FromBody] Beneficio_DetalleModel value)
        {
            AppResponse<Beneficio_Detalle> response = new AppResponse<Beneficio_Detalle>() { Data = null };
            var db = beneficio_DetalleSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar Beneficio_Detalle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Beneficio_Detalle/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            beneficio_DetalleSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

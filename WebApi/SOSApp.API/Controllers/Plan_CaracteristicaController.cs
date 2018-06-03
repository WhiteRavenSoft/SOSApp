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
    /// Controlador de Plan_Caracteristica
    /// </summary>
    public class Plan_CaracteristicaController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Plan_Caracteristica
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Plan_Caracteristica
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<Plan_CaracteristicaModel>> response = new AppPagedResponse<List<Plan_CaracteristicaModel>>() { Data = null };

            var db = plan_CaracteristicaSvc.Load();

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
        /// Método para Mostrar Plan_Caracteristica por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Plan_Caracteristica/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<Plan_CaracteristicaModel> response = new AppResponse<Plan_CaracteristicaModel>() { Data = null };
            var db = plan_CaracteristicaSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Plan_Caracteristica
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Plan_Caracteristica
        public HttpResponseMessage Post([FromBody] Plan_CaracteristicaModel value)
        {
            AppResponse<Plan_Caracteristica> response = new AppResponse<Plan_Caracteristica>() { Data = null };
            var db = plan_CaracteristicaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Plan_Caracteristica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Plan_Caracteristica/5
        public HttpResponseMessage Put(int id, [FromBody] Plan_CaracteristicaModel value)
        {
            AppResponse<Plan_Caracteristica> response = new AppResponse<Plan_Caracteristica>() { Data = null };
            var db = plan_CaracteristicaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar Plan_Caracteristica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Plan_Caracteristica/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            plan_CaracteristicaSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

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
    /// Controlador de EmpresaHorariosEspeciales
    /// </summary>
    public class EmpresaHorariosEspecialesController : ApiBaseController
    {
        /// <summary>
        /// Métodos para Mostrar Lista de EmpresaHorariosEspeciales
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/EmpresaHorariosEspeciales
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<EmpresaHorariosEspecialesModel>> response = new AppPagedResponse<List<EmpresaHorariosEspecialesModel>>() { Data = null };

            var db = empresaHorariosEspecialesSvc.Load();

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
        /// Método para Mostrar EmpresaHorariosEspeciales por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/EmpresaHorariosEspeciales/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<EmpresaHorariosEspecialesModel> response = new AppResponse<EmpresaHorariosEspecialesModel>() { Data = null };
            var db = empresaHorariosEspecialesSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar EmpresaHorariosEspeciales
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/EmpresaHorariosEspeciales
        public HttpResponseMessage Post([FromBody] EmpresaHorariosEspecialesModel value)
        {
            AppResponse<EmpresaHorariosEspeciales> response = new AppResponse<EmpresaHorariosEspeciales>() { Data = null };
            var db = empresaHorariosEspecialesSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar EmpresaHorariosEspeciales
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/EmpresaHorariosEspeciales/5
        public HttpResponseMessage Put(int id, [FromBody] EmpresaHorariosEspecialesModel value)
        {
            AppResponse<EmpresaHorariosEspeciales> response = new AppResponse<EmpresaHorariosEspeciales>() { Data = null };
            var db = empresaHorariosEspecialesSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar EmpresaHorariosEspeciales 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/EmpresaHorariosEspeciales/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            empresaHorariosEspecialesSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

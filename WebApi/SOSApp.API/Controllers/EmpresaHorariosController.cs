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
    /// Controlador de EmpresaHorarios
    /// </summary>
    public class EmpresaHorariosController : ApiBaseController
    {
        /// <summary>
        ///  Método para Mostrar Lista de EmpresaHorarios
        /// </summary>
        /// <returns></returns>
        // GET: api/EmpresaHorarios
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<EmpresaHorariosModel>> response = new AppPagedResponse<List<EmpresaHorariosModel>>() { Data = null };

            var db = empresaHorariosSvc.Load();

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
        /// Método para Mostrar EmpresaHorarios por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/EmpresaHorarios/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<EmpresaHorariosModel> response = new AppResponse<EmpresaHorariosModel>() { Data = null };
            var db = empresaHorariosSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar EmpresaHorarios
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/EmpresaHorarios
        public HttpResponseMessage Post([FromBody] EmpresaHorariosModel value)
        {
            AppResponse<EmpresaHorarios> response = new AppResponse<EmpresaHorarios>() { Data = null };
            var db = empresaHorariosSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar EmpresaHorarios
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/EmpresaHorarios/5
        public HttpResponseMessage Put(int id, [FromBody] EmpresaHorariosModel value)
        {
            AppResponse<EmpresaHorarios> response = new AppResponse<EmpresaHorarios>() { Data = null };
            var db = empresaHorariosSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar EmpresaHorarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/EmpresaHorarios/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            empresaHorariosSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

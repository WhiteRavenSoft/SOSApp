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
    /// Controlador de InstitucionSocialConfiguraciones
    /// </summary>
    public class InstitucionSocialConfiguracionesController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de InstitucionSocialConfiguraciones
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/InstitucionSocialConfiguraciones
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<InstitucionSocialConfiguracionesModel>> response = new AppPagedResponse<List<InstitucionSocialConfiguracionesModel>>() { Data = null };

            var db = institucionSocialConfiguracionesSvc.Load();

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
        /// Método para Mostrar InstitucionSocialConfiguraciones por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/InstitucionSocialConfiguraciones/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<InstitucionSocialConfiguracionesModel> response = new AppResponse<InstitucionSocialConfiguracionesModel>() { Data = null };
            var db = institucionSocialConfiguracionesSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar InstitucionSocialConfiguraciones
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/InstitucionSocialConfiguraciones
        public HttpResponseMessage Post([FromBody] InstitucionSocialConfiguracionesModel value)
        {
            AppResponse<InstitucionSocialConfiguraciones> response = new AppResponse<InstitucionSocialConfiguraciones>() { Data = null };
            var db = institucionSocialConfiguracionesSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar InstitucionSocialConfiguraciones
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/InstitucionSocialConfiguraciones/5
        public HttpResponseMessage Put(int id, [FromBody] InstitucionSocialConfiguracionesModel value)
        {
            AppResponse<InstitucionSocialConfiguraciones> response = new AppResponse<InstitucionSocialConfiguraciones>() { Data = null };
            var db = institucionSocialConfiguracionesSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar InstitucionSocialConfiguraciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/InstitucionSocialConfiguraciones/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            institucionSocialConfiguracionesSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

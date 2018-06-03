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
    /// Controlador de Provincia
    /// </summary>
    public class ProvinciaController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Provincia
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Provincia
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<ProvinciaModel>> response = new AppPagedResponse<List<ProvinciaModel>>() { Data = null };

            var db = provinciaSvc.Load();

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
        /// Método para Mostrar Provincia por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Provincia/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<ProvinciaModel> response = new AppResponse<ProvinciaModel>() { Data = null };
            var db = provinciaSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Provincia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Provincia
        public HttpResponseMessage Post([FromBody] ProvinciaModel value)
        {
            AppResponse<Provincia> response = new AppResponse<Provincia>() { Data = null };
            var db = provinciaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Provincia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Provincia/5
        public HttpResponseMessage Put(int id, [FromBody] ProvinciaModel value)
        {
            AppResponse<Provincia> response = new AppResponse<Provincia>() { Data = null };
            var db = provinciaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar Provincia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Provincia/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            provinciaSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

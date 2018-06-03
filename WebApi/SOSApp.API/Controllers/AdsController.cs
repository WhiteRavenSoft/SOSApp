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
    /// Controlador de Ads
    /// </summary>
    [Authorize]
    public class AdsController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de Ads
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/Ads
        [AllowAnonymous]
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<AdsModel>> response = new AppPagedResponse<List<AdsModel>>() { Data = null };

            var db = adsSvc.Load();

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
        /// Método para Mostrar Ads por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Ads/5
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            AppResponse<AdsModel> response = new AppResponse<AdsModel>() { Data = null };
            var db = adsSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar Ads
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Ads
        public HttpResponseMessage Post([FromBody] AdsModel value)
        {
            AppResponse<Ads> response = new AppResponse<Ads>() { Data = null };
            var db = adsSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar Ads
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Ads/5
        public HttpResponseMessage Put(int id, [FromBody] AdsModel value)
        {
            AppResponse<Ads> response = new AppResponse<Ads>() { Data = null };
            var db = adsSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    
        /// <summary>
        /// Método para Eliminar Ads
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Ads/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            adsSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

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
    /// Controlador de FormaPagoEmpresa
    /// </summary>
    public class FormaPagoEmpresaController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de FormaPagoEmpresa
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/FormaPagoEmpresa
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<FormaPagoEmpresaModel>> response = new AppPagedResponse<List<FormaPagoEmpresaModel>>() { Data = null };

            var db = formaPagoEmpresaSvc.Load();

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
        /// Método para Mostrar FormaPagoEmpresa por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/FormaPagoEmpresa/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<FormaPagoEmpresaModel> response = new AppResponse<FormaPagoEmpresaModel>() { Data = null };
            var db = formaPagoEmpresaSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar FormaPagoEmpresa
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/FormaPagoEmpresa
        public HttpResponseMessage Post([FromBody] FormaPagoEmpresaModel value)
        {
            AppResponse<FormaPagoEmpresa> response = new AppResponse<FormaPagoEmpresa>() { Data = null };
            var db = formaPagoEmpresaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar FormaPagoEmpresa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/FormaPagoEmpresa/5
        public HttpResponseMessage Put(int id, [FromBody] FormaPagoEmpresaModel value)
        {
            AppResponse<FormaPagoEmpresa> response = new AppResponse<FormaPagoEmpresa>() { Data = null };
            var db = formaPagoEmpresaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        ///  Método para Eliminar FormaPagoEmpresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/FormaPagoEmpresa/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            formaPagoEmpresaSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhiteRaven.API.Core;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;

namespace WhiteRaven.API.Controllers
{
    /// <summary>
    /// Controlador de VendedorCuenta
    /// </summary>
    public class VendedorCuentaController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de VendedorCuenta
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/VendedorCuenta
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<VendedorCuentaModel>> response = new AppPagedResponse<List<VendedorCuentaModel>>() { Data = null };

            var db = vendedorCuentaSvc.Load();

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
        /// Método para Mostrar VendedorCuenta por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/VendedorCuenta/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<VendedorCuentaModel> response = new AppResponse<VendedorCuentaModel>() { Data = null };
            var db = vendedorCuentaSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar VendedorCuenta
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //  POST: api/VendedorCuenta 
        public HttpResponseMessage Post([FromBody] VendedorCuentaModel value)
        {
            AppResponse<VendedorCuenta> response = new AppResponse<VendedorCuenta>() { Data = null };
            var db = vendedorCuentaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar VendedorCuenta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/VendedorCuenta/5
        public HttpResponseMessage Put(int id, [FromBody] VendedorCuentaModel value)
        {
            AppResponse<VendedorCuenta> response = new AppResponse<VendedorCuenta>() { Data = null };
            var db = vendedorCuentaSvc.Save(MapToDB(value));
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar VendedorCuenta
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/VendedorCuenta/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            vendedorCuentaSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

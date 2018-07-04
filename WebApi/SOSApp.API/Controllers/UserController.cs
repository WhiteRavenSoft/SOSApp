using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SOSApp.API.Core;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;

namespace SOSApp.API.Controllers
{
    /// <summary>
    /// Controlador de User
    /// </summary>
    [Authorize]
    public class UserController : ApiBaseController
    {
        /// <summary>
        /// Método Para Mostrar por Lista de User
        /// </summary>
        /// <param name="page"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/User
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<UserModel>> response = new AppPagedResponse<List<UserModel>>() { data = null };

            var db = userSvc.LoadActives();

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
            response.data = model;
            response.Limit = limit.Value;
            response.Filter = filters;
            response.Sort = sorts;

            //Calculate actual page
            int intPage = limit.Value == 0 ? 0 : (int)Math.Ceiling((decimal)(start.Value / limit.Value));
            response.Page = intPage + 1;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Mostrar User por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/User/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<UserModel> response = new AppResponse<UserModel>() { Data = null };
            var db = new User();
            if (id == 0)
            {
                db = userSvc.Load(UserId);
                response.Data = MapToModel(db);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            db = userSvc.Load(id);
            response.Data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar User
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/User
        public HttpResponseMessage Post([FromBody] UserModel value)
        {
            AppResponse<User> response = new AppResponse<User>() { Data = null };
            var db = userSvc.Create(value);
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Modificar User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/User/5
        public HttpResponseMessage Put(int id, [FromBody] UserModel value)
        {
            AppResponse<User> response = new AppResponse<User>() { Data = null };
            var db = userSvc.Save(value);
            response.Data = db;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Eliminar User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            userSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("ResetPassword")]
        [HttpPut]
        public HttpResponseMessage ResetPassword(int id, [FromBody] UserChangePasswordModel value)
        {
            userSvc.ChangePass(id, value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

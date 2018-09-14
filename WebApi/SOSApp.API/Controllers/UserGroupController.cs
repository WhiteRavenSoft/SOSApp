using SOSApp.API.Core;
using SOSApp.Data.AppModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOSApp.API.Controllers
{
    /// <summary>
    /// Controlador de grupos de usuario
    /// </summary>
    [Authorize]
    public class UserGroupController : ApiBaseController
    {
        // GET: api/UserGroup
        public HttpResponseMessage Get()
        {
            AppResponse<List<UserGroupModel>> response = new AppResponse<List<UserGroupModel>>() { Data = null };

            var listUserGroups = userGroupSvc.LoadActives();
            response.Data = MapToModel(listUserGroups.ToList());

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: api/UserGroup/5
        public HttpResponseMessage Get(int id)
        {
            AppResponse<UserGroupModel> response = new AppResponse<UserGroupModel>() { Data = null };

            var userGroup = userGroupSvc.Load(id);
            response.Data = MapToModel(userGroup);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // DELETE: api/UserGroup/5
        public HttpResponseMessage Delete(int id)
        {
            userGroupSvc.DeleteLogic(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

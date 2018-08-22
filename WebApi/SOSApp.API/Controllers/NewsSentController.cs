using SOSApp.API.Core;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOSApp.API.Controllers
{
    /// <summary>
    /// Controlador de Noticias enviadas
    /// </summary>
    [Authorize]
    public class NewsSentController : ApiBaseController
    {
        // GET: api/NewsSent
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<NewsSentGridModel>> response = new AppPagedResponse<List<NewsSentGridModel>>() { data = null };

            var db = newsSentSvc.LoadAll();

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

            try
            {
                response.Total = db.Count();
                //db = db.Skip(start.Value).Take(limit.Value);
                var model = MapToGridModel(db.ToList());
                response.data = model;
                response.Limit = 0;//limit.Value;
                response.Filter = filters;
                response.Sort = sorts;

                //Calculate actual page
                int intPage = limit.Value == 0 ? 0 : (int)Math.Ceiling((decimal)(start.Value / limit.Value));
                response.Page = intPage + 1;
            }
            catch (Exception ex)
            {
                var a = ex;
                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar envío de noticias a grupos
        /// </summary>
        // POST: api/NewsSent
        [AllowAnonymous]
        public HttpResponseMessage Post(NewsSentPostModel model)
        {
            List<string> PlayerIds = GetPlayerIds(model.Regions);
            if (PlayerIds.Count > 0)
            {
                var news = newsSvc.Load(model.NewsId);
                if (news != null)
                {
                    OneSignalNotification.SendNotification(news.Title, PlayerIds.ToArray(), news.ID);
                    newsSentSvc.Create(model);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private List<string> GetPlayerIds(List<int> regions)
        {
            List<string> listPlayerIds = new List<string>();
            foreach (var item in regions)
                listPlayerIds.AddRange(userGroupSvc.GetPlayerIds(item));

            return listPlayerIds;
        }
    }
}

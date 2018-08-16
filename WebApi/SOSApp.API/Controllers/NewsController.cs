using SOSApp.API.Core;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SOSApp.API.Controllers
{
    /// <summary>
    /// Controlador de noticias
    /// </summary>
    [Authorize]
    public class NewsController : ApiBaseController
    {
        // GET: News
        [AllowAnonymous]
        public HttpResponseMessage Get(int? page = 1, int? start = 0, int? limit = 20, string filter = "", string sort = "")
        {
            List<GridFilter> filters = new List<GridFilter>();
            List<GridSort> sorts = new List<GridSort>();

            AppPagedResponse<List<NewsGridModel>> response = new AppPagedResponse<List<NewsGridModel>>() { data = null };

            var db = newsSvc.LoadActives();

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
            var model = MapToGridModel(db.ToList());
            response.data = model;
            response.Limit = limit.Value;
            response.Filter = filters;
            response.Sort = sorts;

            //Calculate actual page
            int intPage = limit.Value == 0 ? 0 : (int)Math.Ceiling((decimal)(start.Value / limit.Value));
            response.Page = intPage + 1;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: News/5
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            AppPagedResponse<NewsModel> response = new AppPagedResponse<NewsModel>() { data = null };
            var db = newsSvc.Load(id);
            response.data = MapToModel(db);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Método para Agregar una Noticia
        /// </summary>
        /// <returns></returns>
        // POST: api/News
        public async Task<HttpResponseMessage> Post()
        {
            AppResponse<News> response = new AppResponse<News>() { Data = null };

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/Images/News/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                string fileName = string.Empty;
                string oldFileName = string.Empty;
                string newFileName = string.Empty;

                // Show all the key-value pairs.
                var strTitle = provider.FormData.GetValues("Title").First();
                var strImportant = provider.FormData.GetValues("Important").First();
                var strBody = provider.FormData.GetValues("Body").First();
                var strDate = provider.FormData.GetValues("Date").First();
                var strActive = provider.FormData.GetValues("Active").First();

                var newNews = newsSvc.Save(new News()
                {
                    Title = strTitle,
                    Important = strImportant,
                    Date = DateHelper.GetDateFromString(strDate),
                    Body = strBody,
                    Deleted = false,
                    Active = true,
                    LastUpdate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                });


                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    fileName = file.Headers.ContentDisposition.FileName;
                    oldFileName = file.LocalFileName;
                    string[] fileNameArray = oldFileName.Split('\\');
                    fileNameArray = fileNameArray.Take(fileNameArray.Count() - 1).ToArray();
                    newFileName = String.Join("\\", fileNameArray) + $"\\{newNews.ID.ToString()}.jpg";
                }

                if (File.Exists(newFileName))
                    File.Delete(newFileName);

                File.Copy(oldFileName, newFileName);
                File.Delete(oldFileName);

                //Actualiza la url de la imagen
                newNews.Image = FileHelper.GetNewsImageUrl(newNews.ID);
                newsSvc.Save(newNews);

                return Request.CreateResponse(HttpStatusCode.OK, newNews.ID);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Método para Modificar una Noticia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/User/5
        public async Task<HttpResponseMessage> Put()
        {
            AppResponse<News> response = new AppResponse<News>() { Data = null };

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/Images/News/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                string fileName = string.Empty;
                string oldFileName = string.Empty;
                string newFileName = string.Empty;

                // Show all the key-value pairs.
                var strTitle = provider.FormData.GetValues("Title").First();
                var strImportant = provider.FormData.GetValues("Important").First();
                var strBody = provider.FormData.GetValues("Body").First();
                var strDate = provider.FormData.GetValues("Date").First();
                var strActive = provider.FormData.GetValues("Active").First();
                var strID = provider.FormData.GetValues("ID").First();

                var newNews = newsSvc.Save(new News()
                {
                    ID = int.Parse(strID),
                    Title = strTitle,
                    Important = strImportant,
                    Date = DateHelper.GetDateFromString(strDate),
                    Body = strBody,
                    Deleted = false,
                    Active = true,
                    LastUpdate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                });

                // This illustrates how to get the file names.
                if (provider.FileData.Count > 0)
                {
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        fileName = file.Headers.ContentDisposition.FileName;
                        oldFileName = file.LocalFileName;
                        string[] fileNameArray = oldFileName.Split('\\');
                        fileNameArray = fileNameArray.Take(fileNameArray.Count() - 1).ToArray();
                        newFileName = String.Join("\\", fileNameArray) + $"\\{newNews.ID.ToString()}.jpg";
                    }

                    if (File.Exists(newFileName))
                        File.Delete(newFileName);

                    File.Copy(oldFileName, newFileName);
                    File.Delete(oldFileName);

                    //Actualiza la url de la imagen
                    newNews.Image = FileHelper.GetNewsImageUrl(newNews.ID);
                    newsSvc.Save(newNews);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Método para Eliminar una Noticia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            AppResponse<bool> response = new AppResponse<bool>() { Data = false };
            newsSvc.DeleteLogic(id);
            response.Data = true;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("SaveImage")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveImage(HttpRequestMessage request, int Id, int avatarHeight = 0, int avatarWidth = 0)
        {
            var httpRequest = HttpContext.Current.Request;
            Stream requestStream = await request.Content.ReadAsStreamAsync();

            var objNews =  newsSvc.Load(Id);

            if (requestStream == null || objNews == null)
                return NotFound();

            byte[] data;

            MemoryStream memoryStream = requestStream as MemoryStream;
            if (memoryStream == null)
            {
                memoryStream = new MemoryStream();
                requestStream.CopyTo(memoryStream);
            }

            data = memoryStream.ToArray();
            string extention = ".jpg";
            newsSvc.SavePic(Id, data, string.Concat(Id.ToString(), extention));

            return Ok();
        }

    }
}

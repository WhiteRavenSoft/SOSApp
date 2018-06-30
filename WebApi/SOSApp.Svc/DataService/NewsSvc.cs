using SOSApp.Core;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.GenericDataService;
using System;
using System.IO;
using System.Linq;

namespace SOSApp.Svc.DataService
{
    public class NewsSvc : GenericSvc<News, SOSAppDBEntities>
    {
        public News Save(News x)
        {
            if (x.ID == 0)
            {
                x.CreatedDate = DateTime.Now;
                x.LastUpdate = DateTime.Now;
                x.Active = true;
                x.Deleted = false;
                return CreateEntity(x);
            }

            x.LastUpdate = DateTime.Now;
            return UpdateEntity(x);
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Active = false;
                UpdateEntity(x);
            }
        }

        public IQueryable<News> LoadActives()
        {
            var query = from x in Context.News
                        where x.Active && !x.Deleted
                        orderby x.Date descending
                        select x;

            return query;
        }

        public IQueryable<News> LoadAll()
        {
            var query = from x in Context.News
                        where !x.Deleted
                        orderby x.Date descending
                        select x;

            return query;
        }

        public News SavePic(int id, byte[] avatar, string fileName)
        {
            try
            {
                var db = Load(id);

                string folder = AppHelper.LoadAppSetting("App.CDN.Path.News");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                db.Image = FileHelper.SaveFile(avatar, folder, ".jpg", fileName);

                db = Save(db);

                return db;
            }
            catch (Exception ex)
            {
                var exception = new AppException(ex.ToString(), ex.InnerException);
                logger.Error(exception.FullDetail);
            }

            return null;
        }
    }
}

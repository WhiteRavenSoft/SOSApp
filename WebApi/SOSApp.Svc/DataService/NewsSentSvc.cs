using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.GenericDataService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOSApp.Svc.DataService
{
    public class NewsSentSvc : GenericSvc<NewsSent, SOSAppDBEntities>
    {
        public NewsSent Save(NewsSent x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public IQueryable<NewsSent> LoadAll()
        {
            var query = from x in Context.NewsSent.Include("News")
                        orderby x.SentDate descending
                        select x;

            return query;
        }

        public bool Create(List<NewsSentModel> list)
        {
            try
            {
                var sendDate = DateTime.Now;
                foreach (var item in list)
                {
                    Save(new NewsSent()
                    {
                        NewsID = item.NewsID,
                        UserGroupId = item.UserGroupId,
                        SentDate = sendDate
                    });
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<NewsSent> Create(NewsSentPostModel model)
        {
            try
            {
                List<NewsSent> finalList = new List<NewsSent>();

                var sendDate = DateTime.Now;
                foreach (var item in model.Regions)
                {
                    finalList.Add(Save(new NewsSent()
                    {
                        NewsID = model.NewsId,
                        UserGroupId = item,
                        SentDate = sendDate
                    }));
                }

                return finalList;
            }
            catch
            {
                return new List<NewsSent>();
            }
        }
    }
}

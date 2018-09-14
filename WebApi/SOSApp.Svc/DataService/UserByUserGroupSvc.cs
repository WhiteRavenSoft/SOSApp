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
    public class UserByUserGroupSvc : GenericSvc<UsersByUserGroup, SOSAppDBEntities>
    {
        public UsersByUserGroup Save(UsersByUserGroup x)
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

        public IQueryable<UsersByUserGroup> LoadActives()
        {
            var query = from x in Context.UsersByUserGroup.Include("User").Include("UserGroup")
                        where x.Active && !x.Deleted
                        select x;

            return query;
        }

        internal void DeleteByUser(int UserId)
        {
            var list = (from x in Context.UsersByUserGroup
                        where x.Active && !x.Deleted && x.UserID == UserId
                        select x).ToList();

            foreach (var item in list)
                Delete(item.ID);
        }
    }
}

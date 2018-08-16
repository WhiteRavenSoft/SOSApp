using SOSApp.Core;
using SOSApp.Core.Helper;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.GenericDataService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SOSApp.Svc.DataService
{
    public class UserGroupSvc : GenericSvc<UserGroup, SOSAppDBEntities>
    {
        public UserGroup Save(UserGroup x)
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

        public IQueryable<UserGroup> LoadActives()
        {
            var query = from x in Context.UserGroup
                        where x.Active && !x.Deleted
                        orderby x.Name
                        select x;

            return query;
        }

        public IQueryable<UserGroup> LoadAll()
        {
            var query = from x in Context.UserGroup.Include("User")
                        where !x.Deleted
                        orderby x.Name
                        select x;

            return query;
        }

        public List<string> GetPlayerIds(int userGroupId)
        {
            var query = from x in Context.UsersByUserGroup
                        where x.Active && !x.Deleted
                        && x.UserGroupID == userGroupId
                        select x.User.MobileID;

            return query.ToList();
        }
    }
}

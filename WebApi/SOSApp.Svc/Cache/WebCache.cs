using SOSApp.Svc.DataService;
using SOSApp.Svc.Infrastructure;
using SOSApp.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOSApp.Core.Helper;

namespace SOSApp.Svc.Cache
{
    public class WebCache
    {
        private static volatile WebCache instance;
        private static object syncRoot = new object();

        public Dictionary<int, UserRole> UserRoles = new Dictionary<int, UserRole>();
        public Dictionary<int, User> Users = new Dictionary<int, User>();

        private WebCache()
        {
        }

        public static WebCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new WebCache();
                    }
                }

                return instance;
            }
        }

        public void ReLoad()
        {
            try
            {
                var roles = IoC.Resolve<UserRoleSvc>().LoadAll(x => x.Active).ToList();
                UserRoles = new Dictionary<int, UserRole>(roles.ToDictionary(x => x.ID));

                var users = IoC.Resolve<UserSvc>().LoadActives();
                Users = new Dictionary<int, User>(users.ToDictionary(x => x.ID));
            }
            catch
            {

            }
        }

        public string LoadRolName(int? id)
        {
            if (!id.HasValue)
                return string.Empty;

            if (UserRoles.ContainsKey(id.Value))
                return UserRoles[id.Value].Name;
            else
            {
                var x = IoC.Resolve<UserRoleSvc>().Load(id.Value);

                if (x != null)
                    return x.Name;
            }

            return string.Empty;
        }
    }
}

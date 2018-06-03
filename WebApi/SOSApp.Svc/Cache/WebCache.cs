using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;
using WhiteRaven.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteRaven.Core.Helper;

namespace WhiteRaven.Svc.Cache
{
    public class WebCache
    {
        private static volatile WebCache instance;
        private static object syncRoot = new object();

        public Dictionary<int, UsuarioRol> UserRoles = new Dictionary<int, UsuarioRol>();
        public Dictionary<int, UsuarioAccion> UserActions = new Dictionary<int, UsuarioAccion>();       
        public Dictionary<int, Usuario> Users = new Dictionary<int, Usuario>();

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
                var roles = IoC.Resolve<UsuarioRolSvc>().LoadAll(x => x.Active).ToList();
                UserRoles = new Dictionary<int, UsuarioRol>(roles.ToDictionary(x => x.ID));

                var actions = IoC.Resolve<UsuarioAccionSvc>().LoadAll(x => x.Active).ToList();
                UserActions = new Dictionary<int, UsuarioAccion>(actions.ToDictionary(x => x.ID));             

                var users = IoC.Resolve<UsuarioSvc>().LoadActives();
                Users = new Dictionary<int, Usuario>(users.ToDictionary(x => x.ID));
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
                return UserRoles[id.Value].Nombre;
            else
            {
                var x = IoC.Resolve<UsuarioRolSvc>().Load(id.Value);

                if (x != null)
                    return x.Nombre;
            }

            return string.Empty;
        }
    }
}

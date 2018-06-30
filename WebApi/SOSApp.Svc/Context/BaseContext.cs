using SOSApp.Svc.DataService;
using SOSApp.Svc.Infrastructure;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using SOSApp.Core.Enum;
using SOSApp.Core.Helper;
using SOSApp.Data.DBModel;

namespace SOSApp.Svc.Context
{
    public partial class BaseContext
    {
        private User user;
        private UserRole userRol;

        private HttpContext httpContext = HttpContext.Current;

        public static BaseContext Current
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    object data = Thread.GetData(Thread.GetNamedDataSlot(AppHelper.CONTEXT_KEY));

                    if (data != null)
                        return (BaseContext)data;

                    BaseContext context = new BaseContext();

                    Thread.SetData(Thread.GetNamedDataSlot(AppHelper.CONTEXT_KEY), context);
                    return context;
                }

                if (HttpContext.Current.Items[AppHelper.CONTEXT_KEY] == null)
                {
                    BaseContext context = new BaseContext();
                    HttpContext.Current.Items.Add(AppHelper.CONTEXT_KEY, context);
                    return context;
                }

                return (BaseContext)HttpContext.Current.Items[AppHelper.CONTEXT_KEY];
            }
        }


        public object this[string key]
        {
            get
            {
                if (this.httpContext == null)
                {
                    return null;
                }

                if (this.httpContext.Items[key] != null)
                {
                    return this.httpContext.Items[key];
                }
                return null;
            }
            set
            {
                if (this.httpContext != null)
                {
                    this.httpContext.Items.Remove(key);
                    this.httpContext.Items.Add(key, value);

                }
            }
        }

        public User User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
            }
        }

        public UserRole UserRole
        {
            get
            {
                return this.userRol;
            }
            set
            {
                this.userRol = value;
            }
        }

        public string WebUserHostAddress
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null &&
                    HttpContext.Current.Request.UserHostAddress != null)
                    return HttpContext.Current.Request.UserHostAddress;
                else
                    return string.Empty;
            }
        }

        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public static void SignOut()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                HttpContext.Current.Session.Abandon();

            FormsAuthentication.SignOut();
        }

        public bool IsAdmin
        {
            get
            {
                if (user != null)
                    return true;

                return false;
            }
        }

        public bool IsReadOnly { get; set; }
    }
}

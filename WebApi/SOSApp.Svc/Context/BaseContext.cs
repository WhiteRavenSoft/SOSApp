using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using WhiteRaven.Core.Enum;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.DBModel;

namespace WhiteRaven.Svc.Context
{
    public partial class BaseContext
    {
        private Usuario user;
        private UsuarioRol userRol;

        private HttpContext httpContext = HttpContext.Current;

        private UserSession SaveSession()
        {
            //while (IoC.Resolve<WebSessionSvc>().FindOne(x => x.WebSessionGUID == guid) != null)
            //    guid = Guid.NewGuid();

            var session = new UserSession();

            int current = 0;

            if (this.Usuario != null)
                current = this.Usuario.ID;

            session.UserSessionGUID = Guid.Empty;
            session.ID_Usuario = current;
            session.UltimoAccesso = DateTime.UtcNow;
            session = IoC.Resolve<UserSessionSvc>().Save(session);
            return session;
        }

        public UserSession LoadSession(bool createInDatabase)
        {
            return this.LoadSession(createInDatabase, null);
        }

        public UserSession LoadSession(bool createInDatabase, Guid? sessionID)
        {
            UserSession byId = null;

            object obj2 = Current[AppHelper.SESSION_KEY];

            if (obj2 != null)
                byId = (UserSession)obj2;

            if ((byId == null) && (sessionID.HasValue))
            {
                byId = IoC.Resolve<UserSessionSvc>().FindOne(x => x.UserSessionGUID == sessionID);
                return byId;
            }
            if (byId == null && createInDatabase)
            {
                byId = SaveSession();
            }

            string sessionCookieValue = string.Empty;

            if ((HttpContext.Current.Request.Cookies[AppHelper.SESSION_GUID_KEY] != null) && (HttpContext.Current.Request.Cookies[AppHelper.SESSION_GUID_KEY].Value != null))
                sessionCookieValue = HttpContext.Current.Request.Cookies[AppHelper.SESSION_GUID_KEY].Value;

            if ((byId) == null && (!string.IsNullOrEmpty(sessionCookieValue)))
            {
                var dbSession = IoC.Resolve<UserSessionSvc>().FindOne(x => x.UserSessionGUID == new Guid(sessionCookieValue));

                byId = dbSession;
            }

            Current[AppHelper.SESSION_KEY] = byId;

            return byId;
        }

        public void SaveSessionToClient()
        {
            if (HttpContext.Current != null && this.WebSession != null)
                AppHelper.SetCookie(HttpContext.Current.ApplicationInstance, AppHelper.SESSION_GUID_KEY, this.WebSession.UserSessionGUID.ToString());
        }

        public void ResetSession()
        {
            if (HttpContext.Current != null)
                AppHelper.SetCookie(HttpContext.Current.ApplicationInstance, AppHelper.SESSION_GUID_KEY, string.Empty);

            this.WebSession = null;
            this.Usuario = null;
            this[AppHelper.SESSION_RESET_KEY] = true;
        }

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

        public UserSession WebSession
        {
            get
            {
                return this.LoadSession(false);
            }
            set
            {
                Current[AppHelper.SESSION_KEY] = value;
            }
        }

        public Usuario Usuario
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

        public UsuarioRol UsuarioRol
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

        public int MainCurrency
        {
            get
            {
                return (int)CurrencyEnum.USD;
            }
            set
            {
                if (value == 0)
                    return;

                AppHelper.SetCookie(httpContext.ApplicationInstance, AppHelper.CURRENCY_KEY, value.ToString());
            }
        }

        public int MainLang
        {
            get
            {
                return (int)LangEnum.ES;
            }
            set
            {
                if (value == 0)
                    return;

                AppHelper.SetCookie(httpContext.ApplicationInstance, AppHelper.LANG_KEY, value.ToString());
            }
        }

        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public static void SignOut()
        {
            if (Current != null)
                Current.ResetSession();

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

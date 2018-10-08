using System.Configuration;

namespace SOSApp.Core.Helper
{
    public class Settings
    {
        public static string ApiUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["Application.Api.BaseUrl"].ToString();
                }
                catch
                {
                    return "http://api.sosapp.com/";
                }
            }
        }

        public static bool IsDev
        {
            get
            {
                try
                {
                    return bool.Parse(ConfigurationManager.AppSettings["Api.IsDev"].ToString());
                }
                catch
                {
                    return true;
                }
            }
        }

        public static string GoogleMapsAPIURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMapsAPIURL"].ToString();
                }
                catch
                {
                    return "https://maps.googleapis.com/maps/api/geocode/xml";
                }
            }
        }

        public static string GoogleMapsAPIKey
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMapsAPIKey"].ToString();
                }
                catch
                {
                    return "AIzaSyBf1dXu5kENta2csw4Hdxy_XMLc8WsFwDE";
                }
            }
        }
        

        public static string FacebookUrlPicture
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["FacebookUrlPicture"].ToString();
                }
                catch
                {

                    return "https://graph.facebook.com/{0}/picture?width=250&height=250";
                }
            }
        }

        public static string NewsImagePath
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["App.CDN.Path.News"].ToString();
                }
                catch
                {
                    return "~/Images/News/";
                }
            }
        }

        public static string OneSignalAppId
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["App.OneSignal.AppId"].ToString();
                }
                catch
                {
                    return "594f259c-e74b-4d72-94ee-5f509d38c8fb";
                }
            }
        }

        public static string OneSignalRestAPIKey
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["App.OneSignal.RestAPIKey"].ToString();
                }
                catch
                {
                    return "Y2JhZjQwMWQtZGE2NS00MDgxLWJlMGMtZGM0MDQ1YmUwZDM3";
                }
            }
        }

        public static double ExcludeLatitude
        {
            get
            {
                try
                {
                    return double.Parse(ConfigurationManager.AppSettings["App.Map.ExcludeLatitude"].ToString());
                }
                catch
                {
                    return double.Parse("-30,9451714");
                }
            }
        }

        public static double ExcludeLongitude
        {
            get
            {
                try
                {
                    return double.Parse(ConfigurationManager.AppSettings["App.Map.ExcludeLongitude"].ToString());
                }
                catch
                {
                    return double.Parse("-61,5607353");
                }
            }
        }

        public static string GarbageRootApi
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["App.Basureros.RootApi"].ToString();
                }
                catch 
                {

                    return "http://avltest2.integrarsi.com.ar/Sitio/Basureros/Horarios.aspx"; 
                }
            }
        }
    }

}

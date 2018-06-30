using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        public static string GoogleAPIURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleAPIURL"].ToString();
                }
                catch
                {
                    return "https://www.googleapis.com/oauth2/v1/userinfo?access_token=";
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
    }
}

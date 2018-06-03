using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Core.Helper
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
                    return "http://api.whiteads.com/";
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

        public static string UserImagePath
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["UserImagePath"].ToString();
                }
                catch
                {
                    return "~/Uploads/Images/User/{0}/";
                }
            }
        }

        public static string DriverImagePath
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["EmpresaImagePath"].ToString();
                }
                catch
                {
                    return "~/Uploads/Images/Driver/{0}/";
                }
            }
        }

    }
}

using System;
using System.Configuration;
using System.Xml;

namespace SOSApp.Svc.Configuration
{
    public partial class SOSAppConfig : IConfigurationSectionHandler
    {
        private static bool _initialized = false;
        private static XmlNode _scheduleTasks;
        private const int DefaultValidationCodeHoursExpiration = 10;

        public object Create(object parent, object configContext, XmlNode section)
        {
            _scheduleTasks = section.SelectSingleNode("ScheduleTasks");

            return null;
        }

        public static void Init()
        {
            if (!_initialized)
            {
                ConfigurationManager.GetSection("SOSAppConfiguration");
                _initialized = true;
            }
        }

        public static XmlNode ScheduleTasks
        {
            get
            {
                return _scheduleTasks;
            }
            set
            {
                _scheduleTasks = value;
            }
        }

        public static int ValidationCodeHoursExpiration
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["ValidationCodeHoursExpiration"].ToString());
                }
                catch (Exception)
                {
                    return DefaultValidationCodeHoursExpiration;
                }
            }
        }
    }
}

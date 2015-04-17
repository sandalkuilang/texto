using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SMSTest
{
    class ConfigurationSettingProvider
    {
        private static readonly ConfigurationSettingProvider instance = new ConfigurationSettingProvider();

        public ConfigurationSetting Setting { get; set; }

        private ConfigurationSettingProvider()
        {
            Setting = new ConfigurationSetting();
            Setting.IP = ConfigurationManager.AppSettings["SMSGatewayIP"];
            Setting.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMSGatewayPort"]);
        }

        public static ConfigurationSettingProvider Instance()
        {
            return instance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    public class ApplicationSettings : IConfiguration
    {
        public GeneralSettings General { get; private set; }

        [ConfigurationProperty("serialPorts")] 
        public SerialPortSettings SerialPorts { get; private set; }
        public PluginSettings Plugins { get; private set; }

        public ConnectionStringSettingsCollection ConnectionStrings { get; set; }

        private System.Configuration.Configuration config;

        public ApplicationSettings()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            General = (GeneralSettings)config.GetSection("general");
            SerialPorts = (SerialPortSettings)config.GetSection("serialPorts");
            Plugins = (PluginSettings)config.GetSection("plugin");
            ConnectionStrings = config.ConnectionStrings.ConnectionStrings;
        }
         
        public string GetRawXml(string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                return string.Empty;
            }
            switch (section.ToLower())
            {
                case "general":
                    return General.SectionInformation.GetRawXml(); 
                case "serialports":
                    return SerialPorts.SectionInformation.GetRawXml(); 
                case "plugin":
                    return Plugins.SectionInformation.GetRawXml(); 
                default:
                    return string.Empty;
            } 
        }

        public bool SetRawXml(string section, string raw)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(raw))
            {
                return false;
            }
            switch (section.ToLower())
            {
                case "general":
                    General.SectionInformation.SetRawXml(raw);
                    return true;
                case "serialports":
                    SerialPorts.SectionInformation.SetRawXml(raw);
                    return true;
                case "plugin":
                    Plugins.SectionInformation.SetRawXml(raw);
                    return true;
                default:
                    return false;
            } 
        }

        public bool Save()
        {
            config.Save(ConfigurationSaveMode.Modified);
            return true;
        }
         
    }
}

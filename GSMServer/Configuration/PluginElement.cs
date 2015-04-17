using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    public class PluginElement : ConfigurationSection
    {
        [ConfigurationProperty("assemblyFile")]
        public string AssemblyFile
        {
            get
            {
                return (string)this["assemblyFile"];
            }
            set
            {
                this["assemblyFile"] = value;
            }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }
    }
}

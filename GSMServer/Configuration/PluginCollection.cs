using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    public class PluginCollection : ConfigurationElementCollection
    { 
        protected override ConfigurationElement CreateNewElement()
        {
            return new PluginElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PluginElement)element).AssemblyFile;
        }
    }
}

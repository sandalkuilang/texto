using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{ 
    public class PluginElement
    {
        [XmlAttribute("assemblyFile")]   
        public string AssemblyFile { get; set; }

        [XmlAttribute("type")]   
        public string Type { get; set; }
    }
}

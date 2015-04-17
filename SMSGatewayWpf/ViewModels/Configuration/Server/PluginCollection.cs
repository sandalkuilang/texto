using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{
    [XmlRoot("plugin")]
    [Serializable]
    public class PluginCollection
    {
        [XmlArray("assembly")]
        [XmlArrayItem("add")]
        public PluginElement[] Assembly { get; set; }
    }
}

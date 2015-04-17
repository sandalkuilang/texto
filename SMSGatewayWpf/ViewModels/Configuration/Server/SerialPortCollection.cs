using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{
    [XmlRoot("serialPorts")]
    [Serializable] 
    public class SerialPortCollection
    {
        [XmlArray("serialPort")]
        [XmlArrayItem("add")]
        public SerialPortElement[] SerialPort { get; set; }
    }
}

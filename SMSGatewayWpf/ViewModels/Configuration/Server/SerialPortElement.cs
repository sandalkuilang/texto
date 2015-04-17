using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{
    [Serializable]
    public class SerialPortElement
    {
        [XmlAttribute("portName")]   
        public string PortName { get; set; }

        [XmlAttribute("baudRate")]   
        public int BaudRate { get; set; }

        [XmlAttribute("parity")]    
        public string Parity { get; set; }

        [XmlAttribute("dataBits")]   
        public int DataBits { get; set; }

        [XmlAttribute("stopBits")]    
        public string StopBits { get; set; }

        [XmlAttribute("handshake")]    
        public string Handshake { get; set; }

        [XmlAttribute("serviceCenter")]    
        public string ServiceCenter { get; set; }

        [XmlAttribute("pduMode")]   
        public bool PDUMode { get; set; }
    }
}

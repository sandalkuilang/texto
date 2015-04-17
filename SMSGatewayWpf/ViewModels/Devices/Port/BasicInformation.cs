using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class BasicInformation
    {
        public int SignalQuality { get; set; }
        public string PortName { get; set; }
        public string SoftwareVersion { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string ServiceCenter { get; set; }
        public string Operator { get; set; }
        public string CardID { get; set; }
        public string IntlMobileSubcriberID { get; set; }
        public string OwnNumber { get; set; }
        public bool PDUMode { get; set; }
        public bool IsOpen { get; set; }

    }
}

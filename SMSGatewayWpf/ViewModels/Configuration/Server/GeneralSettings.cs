using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{
    [XmlRoot("general")]
    [Serializable]
    public class GeneralSettings
    { 
        [XmlAttribute("defaultPort")]   
        public string DefaultPort { get; set; }
         
        [XmlAttribute("defaultIP")] 
        public string DefaultIP { get; set; }
         
        [XmlAttribute("defaultEncoding")] 
        public string DefaultEncoding { get; set; }
         
        [XmlAttribute("countryCode")] 
        public string CountryCode { get; set; }
         
        [XmlAttribute("smsGWSignature")] 
        public string SMSGWSignature { get; set; }
         
        [XmlAttribute("prefixOwnNumber")] 
        public string PrefixOwnNumber { get; set; }
         
        [XmlAttribute("intervalWorkerQueue")] 
        public int IntervalWorkerQueue { get; set; }
         
        [XmlAttribute("intervalReadMessage")] 
        public int IntervalReadMessage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class GeneralView
    {
        public ActivityStatus ActivityStatus { get; set; }
        public Functionality Functionality { get; set; }
        public HardwareVersion HardwareVersion { get; set; }
        public IMSI IMSI { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ModelInformation ModelInformation { get; set; }
        public NetworkRegistration NetworkRegistration { get; set; }
        public Operator Operator { get; set; }
        public ReportME ReportME { get; set; }
        public SerialNumber SerialNumber { get; set; }
        public ServiceCenter ServiceCenter { get; set; }
        public SignalQuality SignalQuality { get; set; }
        public SoftwareVersion SoftwareVersion { get; set; }
        public TECharacter TECharacter { get; set; }
        public GeneralView()
        {
            ActivityStatus = new ActivityStatus();
            Functionality = new Functionality();
            HardwareVersion = new HardwareVersion();
            IMSI = new IMSI();
            Manufacturer = new Manufacturer();
            ModelInformation = new ModelInformation();
            NetworkRegistration = new NetworkRegistration();
            Operator = new Operator();
            ReportME = new ReportME();
            SerialNumber = new SerialNumber();
            ServiceCenter = new ServiceCenter();
            SignalQuality = new SignalQuality();
            SoftwareVersion = new SoftwareVersion();
            TECharacter = new TECharacter();
        }

    }
}

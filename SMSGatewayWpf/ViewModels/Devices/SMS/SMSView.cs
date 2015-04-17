using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SMSGatewayWpf.Core; 
using SMSGatewayWpf.ViewModels.Devices.SMS;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class SMSView : BaseBindableModel
    { 
        public SendSMS SendSMS { get; set; }
        public ReadSMS ReadSMS { get; set; }
        public DeleteAllSMS DeleteAllSMS { get; set; }
        public DeleteSMS DeleteSMS { get; set; }

        public SMSView()
        {
            SendSMS = new SendSMS();
            ReadSMS = new ReadSMS();
            DeleteAllSMS = new DeleteAllSMS();
            DeleteSMS = new DeleteSMS(); 
        }
         
    }
}

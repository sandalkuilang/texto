using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class CallView
    {
        public SendUSSD SendUSSD { get; set; }
        public Dial Dial { get; set; }
        public Answer Answer { get; set; }
        public Hangup Hangup { get; set; }
         
        public CallView()
        {
            SendUSSD = new SendUSSD();
            Dial = new Dial();
            Answer = new Answer();
            Hangup = new Hangup(); 
        }
         
    }
}

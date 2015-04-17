using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public enum AudioEnum
    {
        SendMessage = 100,
        ReceiveMessage = 101,
        Call = 600,
        Hangup = 601,
        IncomingCall = 6002
    }
}

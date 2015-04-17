using GSMCommunication.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts.InternalLogging
{
    internal interface ISMSLogging
    {
        void Read(List<BaseResult<SMSReadResult>> arg);
        void Send(BaseResult<SMSSendResult> arg); 
    }
}

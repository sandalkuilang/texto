using GSMServerModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public interface IGatewayService
    {
        BaseGatewayConnection Connection { get; set; }
        string Execute(string command);
        string Execute(Request request);
    }
}


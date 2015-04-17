using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public interface IGatewayConnection : INotifyPropertyChanged
    { 
        bool Connected { get; }
        bool Connect();
        bool Disconnect();

    }
}

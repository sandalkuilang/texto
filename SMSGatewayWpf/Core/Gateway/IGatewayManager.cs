using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public interface IGatewayManager
    {
        void AddService(IGatewayService service);

        IGatewayService GetService();
        IGatewayService GetService<T>();
        void Clear();
    }
}

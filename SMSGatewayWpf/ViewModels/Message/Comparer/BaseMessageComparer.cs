using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message.Comparer
{
    public class BaseMessageComparer : IEqualityComparer<BaseMessageModel>
    {
        public bool Equals(BaseMessageModel x, BaseMessageModel y)
        {
            return x.SeqNbr == y.SeqNbr;
        }

        public int GetHashCode(BaseMessageModel obj)
        {
            return obj.SeqNbr.GetHashCode();
        }
    }
}

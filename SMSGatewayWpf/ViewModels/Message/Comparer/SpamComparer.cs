using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message.Comparer
{
    class SpamComparer : IEqualityComparer<Spam>
    {
        public bool Equals(Spam x, Spam y)
        {
            return x.SeqNbr == y.SeqNbr;
        }

        public int GetHashCode(Spam obj)
        {
            return obj.SeqNbr.GetHashCode();
        }
    }
}

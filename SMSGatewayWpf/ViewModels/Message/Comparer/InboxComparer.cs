using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message.Comparer
{
    internal class InboxComparer : IEqualityComparer<Inbox>
    {
        public bool Equals(Inbox x, Inbox y)
        {
            return x.SeqNbr == y.SeqNbr;
        }

        public int GetHashCode(Inbox obj)
        {
            return obj.SeqNbr.GetHashCode();
        }
    }
}

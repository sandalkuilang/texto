using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message.Comparer
{
    public class OutboxComparer : IEqualityComparer<Outbox>
    {
        public bool Equals(Outbox x, Outbox y)
        {
            return x.SeqNbr == y.SeqNbr;
        }

        public int GetHashCode(Outbox obj)
        {
            return obj.SeqNbr.GetHashCode();
        }
    }
}

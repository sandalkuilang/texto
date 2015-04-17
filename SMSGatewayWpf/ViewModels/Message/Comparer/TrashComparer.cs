using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message.Comparer
{
    public class TrashComparer : IEqualityComparer<Trash>
    {
        public bool Equals(Trash x, Trash y)
        {
            return x.SeqNbr == y.SeqNbr;
        }

        public int GetHashCode(Trash obj)
        {
            return obj.SeqNbr.GetHashCode();
        }
    }
}

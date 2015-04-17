using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class QueueCollectionSource : BaseQueueCollectionSource
    {
        public QueueCollectionSource()
            : base("GetQueue", null)
        {
 
        }

        public override void Refresh()
        {
            GetQueue();
        }
    }
}

using GSMServerModel;
using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class DraftCollectionViewSource : BaseQueueCollectionSource
    { 
        public DraftCollectionViewSource()
            : base("GetDraft", null)
        {
             
        } 
        public override void Refresh()
        {
            GetQueue();
        }
    }
}

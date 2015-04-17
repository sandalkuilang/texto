using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public class BaseContactModel : BaseDataRow
    {
        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                NotifyIfChanged(ref id, value);
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                NotifyIfChanged(ref name, value);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                NotifyIfChanged(ref phoneNumber, value);
            }
        }
    }
}

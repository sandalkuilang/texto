using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public interface IContactCommand
    {
        ICommand MouseDoubleClickCommand { get; set; }
        ICommand EditCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ICommand CheckedHeaderCommand { get; set; }
        ICommand CheckedRowCommand { get; set; }
    }
}

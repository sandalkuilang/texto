using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Message
{
    public interface IMessageCommand
    {
        ICommand MouseDoubleClickCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ICommand MarkAsSpamCommand { get; set; } 
        ICommand ReplyCommand { get; set; }
        ICommand CheckedHeaderCommand { get; set; }
        ICommand CheckedRowCommand { get; set; }
        ICommand SelectionChangedCommand { get; set; }
        ICommand PermanentlyDeleteCommand { get; set; }
        ICommand RestoreCommand { get; set; }
        ICommand ForwardCommand { get; set; }

    }
}

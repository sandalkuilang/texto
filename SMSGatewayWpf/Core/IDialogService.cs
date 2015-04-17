using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMSGatewayWpf.Core
{
    public interface IDialogService
    {
        ReadOnlyCollection<FrameworkElement> Views { get; }

        void Register(FrameworkElement view);
        void Unregister(FrameworkElement view); 
        bool? ShowDialog<T>(object model) where T : Window;
    }
}

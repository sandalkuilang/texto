using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class ApplicationBindingCommands : BaseBindableModel, INotifyPropertyChanged
    { 
        public ICommand SaveCommand { get; set; }
        public ICommand RouteConnectionCommand { get; set; }
           
        private string buttonContent;
        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            { 
                NotifyIfChanged(ref this.buttonContent, value);
            }
        }
         
    }
}

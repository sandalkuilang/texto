using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SMSGatewayWpf.Core;
using SMSGatewayWpf.Navigation;
using MahApps.Metro.Controls;
using SMSGatewayWpf.Views.Dialogs;
using SMSGatewayWpf.ViewModels.Message;

namespace SMSGatewayWpf.Views
{
    /// <summary>
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : UserControl
    {
        private MessageCollaborator model;

        public Messages()
        {
            InitializeComponent(); 
            model = ObjectPool.Instance.Resolve<MessageCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            } 
       }
       
    }
}

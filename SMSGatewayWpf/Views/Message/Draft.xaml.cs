using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Message;
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

namespace SMSGatewayWpf.Views.Message
{
    /// <summary>
    /// Interaction logic for Draft.xaml
    /// </summary>
    public partial class Draft : UserControl
    {
        private MessageCollaborator model;
        public Draft()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<MessageCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.DraftSource;
            } 
        }
    }
}

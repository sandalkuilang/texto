using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Contact;
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

namespace SMSGatewayWpf.Views
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : UserControl
    {
        private DatabaseCollectionViewSource model;
        
        public Contacts()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<DatabaseCollectionViewSource>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            } 
        }
    }
}

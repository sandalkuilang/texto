﻿using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels;
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
    /// Interaction logic for Trash.xaml
    /// </summary>
    public partial class Trash : UserControl
    {
        public Trash()
        {
            InitializeComponent();
            MessageCollaborator model = ObjectPool.Instance.Resolve<MessageCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.TrashSource;
            }
        }
    }
}

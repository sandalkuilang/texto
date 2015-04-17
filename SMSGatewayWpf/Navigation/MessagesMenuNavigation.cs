using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMSGatewayWpf.Core;
using System.Windows;
using System.Windows.Controls;

namespace SMSGatewayWpf.Navigation
{
    public class MessagesMenuNavigation : BaseDataTemplate<ListBoxItem>
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null)
            {
                var content = (string)((ListBoxItem)item).Content;
                if (Templates.Count > 0)
                {
                    for (int i = 0; i < Templates.Count; i++)
                    {
                        if (Templates.ContainsKey(content))
                        {
                            return (System.Windows.DataTemplate)Templates[content];
                        }
                    }
                }
            }
            return (System.Windows.DataTemplate)Templates["Inbox"];
        }
    }
}

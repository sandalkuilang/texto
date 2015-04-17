using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SMSGatewayWpf.Navigation
{
    public class DevicesMenuNavigation : BaseDataTemplate<ItemCollection>
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
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
            return (System.Windows.DataTemplate)Templates["Port"];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace SMSGatewayWpf.ViewModels.ValidationRules
{
    [MarkupExtensionReturnType(typeof(Visibility))]
    public class AuthorizationToVisibilityExtension : MarkupExtension
    {
        public string Operation { get; set; }

        public AuthorizationToVisibilityExtension()
        {
            Operation = String.Empty;
        }

        public AuthorizationToVisibilityExtension(string operation)
        {
            Operation = operation;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Operation))
                return Visibility.Collapsed;
             
            if (AuthorizationProvider.Instance.CheckAccess(Operation))
                return Visibility.Visible;

            return Visibility.Collapsed;
        }
    }


}

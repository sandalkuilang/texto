using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SMSGatewayWpf.ViewModels.ValidationRules
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;
            if (str != null)
            {
                if (str.Length > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message);
        }

        public string Message { get; set; }
    }
}

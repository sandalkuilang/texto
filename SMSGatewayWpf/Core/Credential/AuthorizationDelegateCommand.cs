using SMSGatewayWpf.ViewModels.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public class AuthorizationDelegateCommand : DelegateCommandBase
    {
        public AuthorizationDelegateCommand(Action executeMethod)
            : base((op) => executeMethod(), (op) => AuthorizationProvider.Instance.CheckAccess(op))
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");
        }

        public void Execute()
        {
            base.Execute(null);
        }
    }


    /// <summary>
    /// Generic version of AuthDelegateCommand
    /// </summary>
    public class AuthorizationDelegateCommand<T> : DelegateCommandBase
    {
        public AuthorizationDelegateCommand(Action<T> executeMethod)
            : base((op) => executeMethod((T)op), (op) => AuthorizationProvider.Instance.CheckAccess(op))
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");

            Type genericType = typeof(T);
            if (genericType.IsValueType)
            {
                if ((!genericType.IsGenericType) || (!typeof(Nullable<>).IsAssignableFrom(genericType.GetGenericTypeDefinition())))
                {
                    throw new InvalidCastException("T for AuthDelegateCommand<T> is not an object nor Nullable.");
                }
            }
        }

        public bool CanExecute(T parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(T parameter)
        {
            base.Execute(parameter);
        }
    }
}

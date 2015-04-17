using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.Core
{
    public class DelegateCommand<T> : DelegateCommandBase
    { 
        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, (o) => true)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base((o) => executeMethod((T)o), (o) => canExecuteMethod((T)o))
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod");
        }

        public bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter);
        }

        public void Execute(T parameter)
        {
            base.Execute(parameter);
        }
    }

     
    public class DelegateCommand : DelegateCommandBase
    {

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, () => true)
        {
        }
         
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(o => executeMethod(), o => canExecuteMethod())
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod");
        }

        public void Execute()
        {
            Execute(null);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }



}

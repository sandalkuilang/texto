using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.Core
{
    public abstract class DelegateCommandBase : ICommand
    {
        private readonly Action<object> _executeMethod;
        private readonly Func<object, bool> _canExecuteMethod;

        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        void ICommand.Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecuteMethod(parameter);
        }

        protected void Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        protected bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecuteMethod != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecuteMethod != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMSGatewayWpf.ViewModels
{
    public abstract class BaseBindableModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual bool NotifyIfChanged<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            
            return true;
        } 
      
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string response;
        public string Response
        {
            get
            {
                return response;
            }
            set
            {
                NotifyIfChanged(ref this.response, value);
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            { 
                NotifyIfChanged(ref this.isEnabled, value);  
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                IsEnabled = !value;
                NotifyIfChanged(ref this.isBusy, value);
            }
        }

        public BaseBindableModel()
        {
            this.isEnabled = true;
            this.isBusy = false;
        }


        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get { return string.Empty; }
        }
    }
}

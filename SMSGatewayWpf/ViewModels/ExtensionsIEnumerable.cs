using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels
{
    public static  class ExtensionsIEnumerable
    {
        public static MutableObservableCollection<T> Convert<T>(this IEnumerable<T> original)
        {
            return new MutableObservableCollection<T>(original);
        }
    }
}

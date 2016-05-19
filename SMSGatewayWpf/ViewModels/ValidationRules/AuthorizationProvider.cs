using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.ValidationRules
{
    public abstract class AuthorizationProvider
    {
        private static AuthorizationProvider _instance;

        /// <summary>
        /// This method determines whether the user is authorize to perform 
        /// the requested operation
        /// </summary>
        public abstract bool CheckAccess(string operation);

        /// <summary>
        /// This method determines whether the user is authorize to perform 
        /// the requested operation
        /// </summary>
        public abstract bool CheckAccess(object commandParameter);

        public static void Initialize<TProvider>() where TProvider : AuthorizationProvider, new()
        {
            _instance = new TProvider();
        }

        public static void Initialize<TProvider>(object[] parameters)
        {
            _instance = (AuthorizationProvider)typeof(TProvider).GetConstructor(new Type[]
                { typeof(object[]) }).Invoke(new object[] { parameters });
        }
         
        public static void Destroy()
        {
            _instance = null;
        }

        public static AuthorizationProvider Instance
        {
            get { return _instance; }
        }
    }


}

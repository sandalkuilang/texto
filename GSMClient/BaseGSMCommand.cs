using Cryptography;
using GSMServerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMClient
{
    public abstract class BaseGSMCommand : IGSMCommand
    {
        private const string dataFormat = "{0}</>{1}";
        private Cryptography.Crypter crypter;
        public IKeySym PublicKey { get; set; }
        public IGSMConnection Connection { get; set; } 
            
        public BaseGSMCommand(IGSMConnection connection)
        {
            this.Connection = connection; 
        }

        public Request Request { get; set; }

        protected string Encrypt(string data)
        {
            if (PublicKey != null)
                this.crypter = new Crypter(PublicKey);
            else
                throw new ArgumentNullException("PublicKey");
            
            return crypter.Encrypt(data);
        }

        protected string Encrypt()
        {
            if (PublicKey != null)
                this.crypter = new Crypter(PublicKey);
            else
                throw new ArgumentNullException("PublicKey");

            if (IsRequestValid())
            {
                string command = Newtonsoft.Json.JsonConvert.SerializeObject(this.Request, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                    TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                });
                return crypter.Encrypt(command);
            }
            return null;
        }

        private bool IsRequestValid()
        {
            if (this.Request == null)
                return false;
              
            return true;
        }

        protected string GenerateID()
        {
            Random rnd = new Random();
            string number = rnd.Next(1000000, 9000000).ToString();
            string prefix = "";

            if (this.Request.QueueWorkItem.Schedule == null)
            {
                prefix = "O";
            }
            else if (this.Request.QueueWorkItem.Schedule.GetType() == typeof(DailyTrigger))
            {
                prefix = "D";
            }
            else if (this.Request.QueueWorkItem.Schedule.GetType() == typeof(WeeklyTrigger))
            {
                prefix = "W";
            }
            else if (this.Request.QueueWorkItem.Schedule.GetType() == typeof(MonthlyTrigger))
            {
                prefix = "M";
            }

            return string.Concat(prefix, number);
        }

        public abstract string Write();
        public abstract int Write(string data); 
        public abstract int Write(byte[] data, int startIndex, int length) ;
    }
}

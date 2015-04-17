using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public class ComposeNewContactModel : BaseDataRow, IDataErrorInfo
    {
        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                NotifyIfChanged(ref id, value);
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                NotifyIfChanged(ref name, value);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                NotifyIfChanged(ref phoneNumber, value);
            }
        }

        public ICommand SaveCommand { get; set; }

        public ComposeNewContactModel()
        {
            SaveCommand = new DelegateCommand<ComposeNewContactModel>(new Action<ComposeNewContactModel>(OnSave)); 
        }

        private void OnSave(ComposeNewContactModel args)
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
            if (args.ID == null)
            {
                string id = GenerateID();
                db.Execute("InsertContact", new
                {
                    ID = id,
                    Name = args.Name,
                    PhoneNumber = args.PhoneNumber
                });
            }
            else
            { 
                db.Execute("UpdateContact", new
                {
                    ID = args.ID,
                    Name = args.Name,
                    PhoneNumber = args.PhoneNumber
                });
            } 
            db.Close();
        }

        private string GenerateID()
        {
            //example of ID
            // 2 digit abbreviation of Contact Name
            // 1 digit represent day
            // 1 digit represent month
            // two digit represent year
            // XXXXX is randomize number
            // CN24M15101

            string month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month).Substring(0, 1).ToUpper();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);

            Random rnd = new Random();
            int next9000 = rnd.Next(9000);

            return string.Join("", new string[] 
            { 
                "CN", 
                DateTime.Now.Day.ToString(),
                month,
                year,
                next9000.ToString()
            });
        }
    }
}

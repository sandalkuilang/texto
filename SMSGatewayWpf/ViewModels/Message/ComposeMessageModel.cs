using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization; 
using SMSGatewayWpf.ViewModels.Configuration.Server;
using GSMServerModel;
using Crypto;
using SMSGatewayWpf.Core.Gateway;
using Texaco.Database;
using System.ComponentModel;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using SMSGatewayWpf.Views.Dialogs;
using SMSGatewayWpf.ViewModels.Contact;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class ComposeMessageModel : BaseDataRow, IDataErrorInfo
    {
        private string source;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }

        private string seqNbr;
        public string SeqNbr
        {
            get
            {
                return seqNbr;
            }
            set
            {
                NotifyIfChanged(ref seqNbr, value);
            }
        }

        private string phonenumber;
        public string Phonenumber
        {
            get
            {  
                return phonenumber;
            }
            set
            {
                NotifyIfChanged(ref phonenumber, value);
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                NotifyIfChanged(ref message, value);
            }
        }

        private string date;
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                NotifyIfChanged(ref date, value);
            }
        }

        private string time;
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                NotifyIfChanged(ref time, value);
            }
        }

        private WeeklyOptions weeklyOptions;
        public WeeklyOptions WeeklyOptions
        {
            get
            {
                return weeklyOptions;
            }
            set
            {
                NotifyIfChanged(ref weeklyOptions, value);
            }
        }

        private TriggerOptions triggerOptions;
        public TriggerOptions TriggerOptions
        {
            get
            {
                return triggerOptions;
            }
            set
            {
                NotifyIfChanged(ref triggerOptions, value);
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                NotifyIfChanged(ref enabled, value);
            }
        }

        private DateTime nextExecuted;
        public DateTime NextExecuted
        {
            get
            {
                return nextExecuted;
            }
            set
            {
                NotifyIfChanged(ref nextExecuted, value);
            }
        }

        public List<KeyValueOption> Months { get; set; }
        public List<KeyValueOption> Days { get; set; }  
        public string SelectedMonth { get; set; }
        public int RecursDay { get; set; }
        public int RecursWeek { get; set; }
    
        public ICommand SaveCommand { get; set; }

        public ICommand LookupContact { get; set; }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "PhoneNumber":
                        if (string.IsNullOrEmpty(Phonenumber))
                            return "Phone number cannot be empty";
                        break;
                    case "Message":
                        if (string.IsNullOrEmpty(Message))
                            return "You must enter Message";
                        break;
                }
                if (!string.IsNullOrEmpty(Phonenumber) && (!string.IsNullOrEmpty(Message)))
                {
                    IsEnabled = true;
                }
                else
                {
                    IsEnabled = false;
                }
                return string.Empty;
            }
        }

        public ComposeMessageModel()
        {
            Days = new List<KeyValueOption>();
            for(int i= 1; i <= 31;i++)
            {
                Days.Add(new KeyValueOption()
                {
                    ID = i.ToString(),
                    Title = i.ToString()
                });
            }

            Days.Add(new KeyValueOption()
            {
                ID = "32",
                Title = "Last"
            });

            Months = new List<KeyValueOption>();
            for (int i = 1; i <= 12; i++)
            {
                Months.Add(new KeyValueOption()
                {
                    ID = i.ToString(),
                    Title = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)
                });
            }

            Date = DateTime.Now.ToString();
            Time = DateTime.Now.ToString("HH:mm");
            Enabled = true;
            TriggerOptions = TriggerOptions.OneTime;
            RecursDay = 1;
            RecursWeek = 1; 
            weeklyOptions = new WeeklyOptions();
            SaveCommand = new DelegateCommand<ComposeMessageModel>(new Action<ComposeMessageModel>(OnSave));
            LookupContact = new DelegateCommand<object>(new Action<object>(OnLookupContact));
        }

        private void OnSave(ComposeMessageModel model)
        {
            if (string.IsNullOrEmpty(model.Phonenumber))
                return;

            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            IKeySym keySym = new ApplicationSettingKeySym();
            Header header;
            Request request;
            QueueWorkItem item;
            IGatewayService service;
            IAudioService audio;

            string[] multipleRecipient = model.Phonenumber.Split(';');
            foreach (string phonenumber in multipleRecipient)
            {
                header = new Header(settings.General.Signature, "desktop client", "SMSSend");
                request = new Request(null, header, null);
                item = new QueueWorkItem();
                 

                item.Command = string.Format(GSMClient.Command.CommandCollection.SMSSend, phonenumber, model.Message);
                item.Enabled = model.Enabled;
                if (date.Length > 10)
                {
                    item.Created = DateTime.Parse(date);
                }
                else
                {
                    item.Created = DateTime.Parse(string.Join(" ", new string[] { date, time }));
                }

                switch (model.TriggerOptions)
                {
                    case ViewModels.Message.TriggerOptions.Daily:
                        DailyTrigger dt = new DailyTrigger();
                        dt.RecursEvery = model.RecursDay;

                        item.Schedule = dt;
                        break;
                    case ViewModels.Message.TriggerOptions.Weekly:
                        WeeklyTrigger wt = new WeeklyTrigger();
                        wt.RecursEvery = model.RecursWeek;
                        wt.Sunday = model.WeeklyOptions.Sunday;
                        wt.Monday = model.WeeklyOptions.Monday;
                        wt.Thursday = model.WeeklyOptions.Thursday;
                        wt.Wednesday = model.WeeklyOptions.Wednesday;
                        wt.Friday = model.WeeklyOptions.Friday;
                        wt.Saturday = model.WeeklyOptions.Saturday;

                        item.Schedule = wt;
                        break;
                    case ViewModels.Message.TriggerOptions.Monthly:
                        MonthlyTrigger mt = new MonthlyTrigger();

                        mt.January = model.Months[0].IsSelected;
                        mt.February = model.Months[1].IsSelected;
                        mt.March = model.Months[2].IsSelected;
                        mt.April = model.Months[3].IsSelected;
                        mt.May = model.Months[4].IsSelected;
                        mt.June = model.Months[5].IsSelected;
                        mt.July = model.Months[6].IsSelected;
                        mt.August = model.Months[7].IsSelected;
                        mt.September = model.Months[8].IsSelected;
                        mt.October = model.Months[9].IsSelected;
                        mt.November = model.Months[10].IsSelected;
                        mt.December = model.Months[11].IsSelected;

                        StringBuilder selected = new StringBuilder();
                        foreach (KeyValueOption option in model.Days)
                        {
                            if (option.IsSelected == 1)
                            {
                                selected.Append(option.Title + ",");
                            }
                        }

                        if (selected.Length > 1)
                            mt.Days = selected.ToString().Remove(selected.Length - 1, 1);

                        item.Schedule = mt;
                        break;
                }

                if (!string.IsNullOrEmpty(model.source))
                {
                    if (model.source == "E")
                    {
                        IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                        IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

                        db.Execute("DeleteQueue", new
                        {
                            SeqNbr = model.SeqNbr
                        });
                        db.Close();
                    }

                }

                request.QueueWorkItem = item;

                service = GatewayManager.Instance.GetService<DbService>();

                this.IsBusy = true;
                this.IsEnabled = false;

                if (settings.General.Sounds)
                {
                    audio = ObjectPool.Instance.Resolve<IAudioService>();
                    audio.Play(AudioEnum.SendMessage);
                }
                Response = service.Execute(request);

                this.IsEnabled = true;
                this.IsBusy = false;  
            } 
        }

        public void OnLookupContact(object args)
        {
            LookupContactSource data = new LookupContactSource();
            ObjectPool.Instance.Register<LookupContactSource>().ImplementedBy(data);
            bool? result = DialogService.Instance.ShowDialog<LookupContact>(data);
            if (result.Value == true)
            {

                StringBuilder phone = new StringBuilder();
                foreach (Contact.Contact selected in data.Source)
                {
                    if (selected.IsSelected) 
                        phone.AppendFormat("{0}; ", selected.PhoneNumber); 
                }

                if (phone.Length > 0) 
                {
                    if (!string.IsNullOrEmpty(Phonenumber))
                    {
                        if (Phonenumber.Substring(Phonenumber.Length - 1, 1) == ";")
                            Phonenumber = Phonenumber.Remove(Phonenumber.Length - 1, 1);
                    }
                    
                    Phonenumber = string.Join(";", Phonenumber, phone.ToString().Substring(0, phone.Length - 1).Replace(" ", string.Empty));
                }
                else
                    Phonenumber = string.Join(";", Phonenumber, phone.ToString());

                if (Phonenumber.Substring(0, 1) == ";") 
                    Phonenumber = Phonenumber.Substring(1); 

                if (Phonenumber.Substring(Phonenumber.Length - 1, 1) == ";") 
                    Phonenumber = Phonenumber.Remove(Phonenumber.Length - 1, 1); 
            }
        }
    }
}

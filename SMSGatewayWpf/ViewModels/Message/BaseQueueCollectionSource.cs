using GSMServerModel;
using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Message
{
    public abstract class BaseQueueCollectionSource : BaseMessageViewModel<ComposeMessageModel>
    {

        private MutableObservableCollection<ComposeMessageModel> source;
        public override MutableObservableCollection<ComposeMessageModel> Source
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

        private string sql;
        private object[] args;

        public BaseQueueCollectionSource(string sql, params object[] args)
        {
            Source = new MutableObservableCollection<ComposeMessageModel>();
            this.sql = sql;
            this.args = args;
        }

        public abstract void Refresh();

        protected void GetQueue()
        {
            GetQueue(this.sql, this.args);
        }

        protected void GetQueue(string sql)
        {
            GetQueue(sql, null);
        }
        
        protected void GetQueue(string sql, params object[] args)
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

            List<QueueWork> workItem = db.Query<QueueWork>(sql, args);
            MutableObservableCollection<ComposeMessageModel> buffer = new MutableObservableCollection<ComposeMessageModel>();

            if (workItem.Any())
            { 
                List<DailyTrigger> daily;
                List<WeeklyTrigger> weekly;
                List<MonthlyTrigger> monthly;
                CommandParser parser;
                Dictionary<string, string> parsingCommand;
                DynamicData parameters;
                ComposeMessageModel model;

                foreach (QueueWork item in workItem)
                {  
                    model = new ComposeMessageModel(); 
  
                    parser = new CommandParser();
                    parsingCommand = parser.Parse(item.Command); 
                    parameters = new DynamicData();
                    foreach (string key in parsingCommand.Keys)
                    {
                        if (!key.Equals("Command"))
                        {
                            parameters.Add(key, parsingCommand[key]);
                        }
                    }

                    model.Source = item.Source;
                    model.SeqNbr = item.SeqNbr;
                    model.Phonenumber = (string)parameters.GetDictionary()["number"];
                    model.Message = (string)parameters.GetDictionary()["message"];
                    model.Date = item.Created.ToString("dd/MM/yyyy");
                    model.Time = item.Created.ToString("HH:mm");
                    model.Enabled = item.Enabled;

                    if (item.NextExecuted.Ticks > 0)
                        model.NextExecuted = DateTime.Parse(item.NextExecuted.ToString("dd/MM/yyyy HH:mm"));

                    string triggerPrefix = string.Empty;
                    if (!string.IsNullOrEmpty(item.ScheduleID))
                        triggerPrefix = item.ScheduleID.Substring(0, 1); 
                    switch (triggerPrefix)
                    { 
                        case "D":
                            daily = db.Query<DailyTrigger>("GetDailyTriggerByID", new { ID = item.ScheduleID });
                            if (daily.Any())
                            {
                                model.TriggerOptions = TriggerOptions.Daily;
                                model.RecursDay = daily[0].RecursEvery;
                            }
                            break;
                        case "W":
                            weekly = db.Query<WeeklyTrigger>("GetWeeklyTriggerByID", new { ID = item.ScheduleID });
                            if (weekly.Any())
                            {
                                model.TriggerOptions = TriggerOptions.Weekly;
                                model.RecursWeek = weekly[0].RecursEvery;
                                model.WeeklyOptions.Monday = weekly[0].Monday;
                                model.WeeklyOptions.Tuesday = weekly[0].Tuesday;
                                model.WeeklyOptions.Wednesday = weekly[0].Wednesday;
                                model.WeeklyOptions.Thursday = weekly[0].Thursday;
                                model.WeeklyOptions.Friday = weekly[0].Friday;
                                model.WeeklyOptions.Saturday = weekly[0].Saturday;
                                model.WeeklyOptions.Sunday = weekly[0].Sunday;
                            }
                            break;
                        case "M":
                            monthly = db.Query<MonthlyTrigger>("GetMonthlyTriggerByID", new { ID = item.ScheduleID });
                            if (monthly.Any())
                            {
                                model.TriggerOptions = TriggerOptions.Monthly;

                                model.Months[0].IsSelected = monthly[0].January;
                                model.Months[1].IsSelected = monthly[0].February;
                                model.Months[2].IsSelected = monthly[0].March;
                                model.Months[3].IsSelected = monthly[0].April;
                                model.Months[4].IsSelected = monthly[0].May;
                                model.Months[5].IsSelected = monthly[0].June;
                                model.Months[6].IsSelected = monthly[0].July;
                                model.Months[7].IsSelected = monthly[0].August;
                                model.Months[8].IsSelected = monthly[0].September;
                                model.Months[9].IsSelected = monthly[0].October;
                                model.Months[10].IsSelected = monthly[0].November;
                                model.Months[11].IsSelected = monthly[0].December;

                                string[] days = monthly[0].Days.Split(',');
                                foreach (string day in days)
                                {
                                    model.Days[Convert.ToInt32(day)].IsSelected = 1;   
                                }

                            }
                            break;
                        case "O":
                            model.TriggerOptions = TriggerOptions.OneTime;
                            break;
                    }
                    buffer.Add(model);                    
                }
            }
            Source = buffer; 
        }

        private T CopyTo<F, T>(F from)
        {
            T to = Activator.CreateInstance<T>();
            PropertyInfo[] props = from.GetType().GetProperties();
            Type targetType = to.GetType();
            PropertyInfo targetPropertyInfo;
            foreach (PropertyInfo prop in props)
            {
                targetPropertyInfo = targetType.GetProperty(prop.Name);
                if (targetPropertyInfo != null)
                    targetPropertyInfo.SetValue(to, prop.GetValue(from, null), null);
            }
            return to;
        }

    }
}

using Crypto;
using GSMServer.Configuration;
using GSMServer.Worker.Model;
using Newtonsoft.Json;
using Sockets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Texaco.Database;

namespace GSMServer.Worker
{
    internal class DatabaseWorkerPool : IQueue<PacketEventArgs>
    {
        Crypto.Crypter crypter = new Crypto.Crypter(new DefaultConfigurationKey());

        private readonly object syncLock = new object();

        public PacketEventArgs Get()
        {
            lock(syncLock)
            {
                List<DailyTrigger> daily = new List<DailyTrigger>();
                List<WeeklyTrigger> weekly = new List<WeeklyTrigger>();
                List<MonthlyTrigger> monthly = new List<MonthlyTrigger>();

                PacketEventArgs result = null;
                bool valid = false;

                List<QueueWorkItem> workItem;
                ApplicationSettings configuration = (ApplicationSettings)ObjectPool.Instance.Resolve<IConfiguration>();
                
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                try
                { 
                    try
                    {
                        workItem = db.Query<QueueWorkItem>("GetQueueWorkItem", null);
                        if (!workItem.Any())
                            return null;

                        foreach(QueueWorkItem item in workItem.ToList())
                        {
                            daily = null;
                            weekly = null;
                            monthly = null;
                            if (workItem.Count > 0)
                            {
                                if (!item.Enabled)
                                    return null; 

                                result = new PacketEventArgs(null, null, ASCIIEncoding.ASCII);
                                DateTime currentTime = DateTime.Now;
                                if (item.Status == "R")
                                {
                                    string triggerPrefix = string.Empty;
                                    if (!string.IsNullOrEmpty(item.ScheduleID))
                                        triggerPrefix = item.ScheduleID.Substring(0, 1);

                                    switch (triggerPrefix)
                                    {
                                        case "O":
                                            if (item.Created <= currentTime)
                                            {
                                                valid = true;
                                            }
                                            break;
                                        case "D":
                                            daily = db.Query<GSMServer.Worker.Model.DailyTrigger>("GetDailyTriggerByID", new { ID = item.ScheduleID });
                                            if (daily.Any())
                                            {
                                               if (item.NextExecuted.Ticks > 0 &&
                                                   item.NextExecuted <= currentTime &&
                                                   item.LastExecuted < currentTime)
                                                {    
                                                    valid = true; 
                                                }
                                                else if (item.Created.Ticks > 0)
                                                {
                                                    if (item.Created <= currentTime) 
                                                        valid = true;  
                                                } 
                                            }
                                            break;
                                        case "W":
                                            weekly = db.Query<GSMServer.Worker.Model.WeeklyTrigger>("GetWeeklyTriggerByID", new { ID = item.ScheduleID });
                                            if (weekly.Any())
                                            { 
                                                DayOfWeek day = currentTime.DayOfWeek;
                                                Type weeklyType = weekly[0].GetType();
                                                PropertyInfo prop = weeklyType.GetProperty(day.ToString());
                                                int valueOfDay;
                                                if (prop != null)
                                                {
                                                    valueOfDay = (int)prop.GetValue(weekly[0], null);
                                                    if (valueOfDay == 1 && (item.NextExecuted <= currentTime || item.NextExecuted.Ticks == 0))
                                                    {
                                                        valid = true; 
                                                    }
                                                }
                                            }
                                            break;
                                        case "M":
                                            monthly = db.Query<GSMServer.Worker.Model.MonthlyTrigger>("GetMonthlyTriggerByID", new { ID = item.ScheduleID });
                                            if (monthly.Any())
                                            {  
                                                string[] days = monthly[0].Days.Split(',');
                                                foreach (string day in days)
                                                {
                                                    if (Convert.ToInt32(day) == currentTime.Day && (item.NextExecuted <= currentTime || item.LastExecuted.Ticks == 0))
                                                    {
                                                        valid = true; 
                                                    } 
                                                }
                                            }
                                            break;
                                    }
                                } 
                                else if (item.Status == "D")
                                {
                                    db.Execute("DeleteQueueWorkItem", new { SeqNbr = item.SeqNbr });
                                } 
                                else if (item.Status == "E")
                                {
                                    if (item.NextExecuted < currentTime && 
                                        (item.LastExecuted.Day < currentTime.Day &&
                                        item.LastExecuted.Month <= currentTime.Month &&
                                        item.LastExecuted.Year <= currentTime.Year))
                                    {
                                        db.Execute("UpdateQueueLastExecuted", new
                                        {
                                            Status = "R",
                                            SeqNbr = item.SeqNbr
                                        }); 
                                    }
                                }

                                if (valid)
                                {
                                    GSMServerModel.QueueWorkItem resultQueue = CopyTo<QueueWorkItem, GSMServerModel.QueueWorkItem>(item);
                                    if (daily != null || weekly != null || monthly != null)
                                    {
                                        if (daily != null)
                                        {
                                            if (daily.Any())
                                                resultQueue.Schedule = CopyTo<DailyTrigger, GSMServerModel.DailyTrigger>(daily[0]);
                                        }
                                        else if (weekly != null)
                                        {
                                            if (weekly.Any())
                                                resultQueue.Schedule = CopyTo<WeeklyTrigger, GSMServerModel.WeeklyTrigger>(weekly[0]);
                                        }
                                        else if (monthly != null)
                                        {
                                            if (monthly.Any())
                                                resultQueue.Schedule = CopyTo<MonthlyTrigger, GSMServerModel.MonthlyTrigger>(monthly[0]);
                                        }
                                    }
                                    result.Data = ConvertToByte(resultQueue);
                                    break;
                                }
                            }
                        }  
                    }
                    finally
                    {
                        db.Close();
                    }
                }
                catch(Exception)
                {
                    throw;
                }
                return result.Data == null ? null : result;
            } 
        }

        private byte[] ConvertToByte(GSMServerModel.QueueWorkItem queue)
        {
            GSMServerModel.Request request = new GSMServerModel.Request(null, null, null);  
            request.QueueWorkItem = queue;
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            return ASCIIEncoding.ASCII.GetBytes(jsonData);
        }

        private T CopyTo<F, T>(F from)
        {
            T to = Activator.CreateInstance<T>();
            PropertyInfo[] props = from.GetType().GetProperties();
            Type targetType = to.GetType();
            PropertyInfo  targetPropertyInfo;
            foreach(PropertyInfo prop in props)
            {
                targetPropertyInfo = targetType.GetProperty(prop.Name);
                if (targetPropertyInfo != null)
                    targetPropertyInfo.SetValue(to, prop.GetValue(from, null), null);
            }
            return to;
        }

        private string GetDayName(DateTime dt)
        {
            return CultureInfo.InvariantCulture.DateTimeFormat.DayNames[(int)dt.DayOfWeek];
        }
         
        public void Add(PacketEventArgs item)
        {
            // do nothing
        }

        public List<PacketEventArgs> ToList()
        { 
            List<WorkItem> queryResult; 

            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
            queryResult = db.Query<WorkItem>("GetAllQueueWorkItem", null);
            db.Close();

            List<PacketEventArgs> result = new List<PacketEventArgs>();
            if (queryResult.Count > 0)
            {
                foreach(WorkItem workItem in queryResult)
                {
                    result.Add(new PacketEventArgs(null, ASCIIEncoding.ASCII.GetBytes(queryResult[0].Command), null));
                }
            }

            return result;
        }

        public int Count
        {
            get 
            {
                List<int> count; 
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                count = db.Query<int>("CountQueueWorkItem", null);
                db.Close();
                return count[0];
            }
        }

        public void Clear()
        {
            // do nothing
        }
    }
}

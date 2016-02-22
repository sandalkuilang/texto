/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crypto;
using GSMCommunication.Feature;
using GSMServer.Contracts;
using GSMServerModel;
using Sockets;
using GSMServer.Contracts.InternalLogging;
using System.Threading;
using Texaco.Database;
using System.Reflection;

namespace GSMServer.Worker
{
    internal class DefaultPipeline : BasePipeline
    {
        private ActionInvokerLookup actions;
        public const string NAME = "DefaultWorker";
        private string[] days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private int[] dayIndex = new int[] { 1, 2, 3, 4, 5, 6, 7 };

        public DefaultPipeline(ActionInvokerLookup actions)
        {
            this.actions = actions; 
        }

        public override string OnPush(ParameterizedMap map)
        {
            dynamic response = null;
            bool deviceRemoved = false;
            StringBuilder responseJson = new StringBuilder(); 
            GSMCommunication.Feature.BasicInformation basic = map.TryGet<BasicInformation>("base");
            if (basic != null)
            {
                try
                {
                    basic.OnBeginExecuting();
                    List<string> command = map.TryGet<List<string>>("command");

                    if (command.Where(s => s.ToLower() == "help").Any())
                    { // if command contains 'help' word
                        ActionInvoker invoker = actions.ToList().Where(d => d.InterfaceType.Name.ToLower() == command[1].ToLower()).SingleOrDefault();
                        if (invoker != null)
                        {
                            responseJson = new StringBuilder(Newtonsoft.Json.JsonConvert.SerializeObject(invoker.Commands, 
                                Newtonsoft.Json.Formatting.None, 
                                new Newtonsoft.Json.JsonSerializerSettings() 
                                { 
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                }));
                        }
                    }
                    else 
                    {
                        ActionInvoker invoker = actions.ToList().Where(d => d.InterfaceType.Name.ToLower() == command[0].ToLower()).SingleOrDefault();
                        if (invoker != null)
                        {
                            response = invoker.TryInvoke(command[1], new object[] { map });
                            if (response != null)
                            {

                                responseJson = new StringBuilder(Newtonsoft.Json.JsonConvert.SerializeObject(response,
                                    Newtonsoft.Json.Formatting.None,
                                    new Newtonsoft.Json.JsonSerializerSettings()
                                    {
                                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                    }));
                            }
                        }
                    } 
                }
                catch (UnauthorizedAccessException uae)
                {
                    IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
                    log.Write(uae.Message + " - push command (unauthorized access)");
                    log.Write("plug the device...");
                    deviceRemoved = true;
                }
                catch (System.IO.IOException ioe)
                {
                    IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
                    log.Write(ioe.Message + " - push command (I/O)");
                    log.Write("plug the device...");
                    deviceRemoved = true;
                }
                finally
                {
                    basic.OnEndExecuting();
                }
            } 
            if (deviceRemoved)
            { 
                Thread.Sleep(1000);
                IServer server = ObjectPool.Instance.Resolve<IServer>();
                server.OnDeviceRemoved();
            }
            return responseJson.ToString();
        }
         
        public override string OnPull(ParameterizedMap map)
        {
            StringBuilder responseJson = new StringBuilder();
            bool deviceRemoved = false;
            GSMCommunication.Feature.BasicInformation basic = map.TryGet<BasicInformation>("base");
            if (basic != null)
            {
                try
                {
                    basic.OnBeginExecuting();
                    List<BaseResult<SMSReadResult>> list = actions.Get<ISMS>().ReadAll(map);
                    actions.Get<ISMS>().DeleteAll(map);
                    responseJson = new StringBuilder(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                }
                catch (System.IO.IOException ex)
                {
                    /// port closed
                    IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
                    log.Write(ex.Message + " - pull command (I/O)");
                    log.Write("plug the device...");
                    deviceRemoved = true;
                }
                catch(System.InvalidOperationException ioe)
                {
                    // port closed
                    IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
                    log.Write(ioe.Message + " - pull command (invalid operation)");
                    log.Write("plug the device...");
                    deviceRemoved = true;
                }
                finally
                {
                    basic.OnEndExecuting();
                }
            }
            if (deviceRemoved)
            {
                Thread.Sleep(1000);
                IServer server = ObjectPool.Instance.Resolve<IServer>();
                server.OnDeviceRemoved();
            }
            return responseJson.ToString();
        } 

        public override string OnBeforePush(ParameterizedMap map)
        { 
            return null;
        }

        public override string OnAfterPush(ParameterizedMap map)
        {
            PacketEventArgs packet = map.TryGet<PacketEventArgs>("packet");
            string dataEncode = ASCIIEncoding.GetEncoding("ibm850").GetString(packet.Data);
            Request request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(dataEncode, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects
            });

            if (request.QueueWorkItem != null)
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

                if (request.QueueWorkItem.Schedule == null && !string.IsNullOrEmpty(request.QueueWorkItem.SeqNbr))
                { 
                    db.Execute("DeleteQueueWorkItem", new 
                    { 
                        SeqNbr = request.QueueWorkItem.SeqNbr
                    });
                
                }
                else if (request.QueueWorkItem.Schedule != null)
                {
                    db.Execute("UpdateQueueLastExecuted", new 
                    { 
                        SeqNbr = request.QueueWorkItem.SeqNbr,
                        Status = "E"
                    });
                    DateTime nextExecuted;

                    if (request.QueueWorkItem.Schedule.GetType() == typeof(MonthlyTrigger))
                    {
                        MonthlyTrigger monthlyItem = (MonthlyTrigger)request.QueueWorkItem.Schedule;
                        string[] daysList = monthlyItem.Days.Split(',');
                        int[] days = Sorting(daysList);

                        for (int i = 0; i <= days.Length - 1; i++)
                        {
                            if (Convert.ToInt32(days[i]) == DateTime.Now.Day 
                                && (request.QueueWorkItem.NextExecuted <= DateTime.Now || request.QueueWorkItem.LastExecuted.Ticks == 0))
                            {
                                int monthIndex = 1;

                                if (monthlyItem.January == 1)
                                    monthIndex = 1;
                                else if (monthlyItem.February == 1)
                                    monthIndex = 2;
                                else if (monthlyItem.March == 1)
                                    monthIndex = 3;
                                else if (monthlyItem.April == 1)
                                    monthIndex = 4;
                                else if (monthlyItem.May == 1)
                                    monthIndex = 5;
                                else if (monthlyItem.June == 1)
                                    monthIndex = 6;
                                else if (monthlyItem.July == 1)
                                    monthIndex = 7;
                                else if (monthlyItem.August == 1)
                                    monthIndex = 8;
                                else if (monthlyItem.September == 1)
                                    monthIndex = 9;
                                else if (monthlyItem.October == 1)
                                    monthIndex = 10;
                                else if (monthlyItem.November == 1)
                                    monthIndex = 11;
                                else if (monthlyItem.December == 1)
                                    monthIndex = 12; 

                                nextExecuted = DateTime.Now.AddYears(1);
                                if (i + 1 < days.Length)
                                {
                                    nextExecuted = DateTime.Now.AddDays(days[i + 1]);
                                }
                                else 
                                {
                                    if (DateTime.Now.Month == 12 && monthIndex != 12)
                                    {
                                        nextExecuted = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", nextExecuted.Year + 1, monthIndex, days[0], nextExecuted.Hour, nextExecuted.Minute, nextExecuted.Second));
                                    } 
                                }

                                db.Execute("UpdateQueueNextExecuted", new
                                {
                                    SeqNbr = request.QueueWorkItem.SeqNbr,
                                    NextExecuted = nextExecuted
                                });  
                                break;
                            }
                        }
                    }
                    else if (request.QueueWorkItem.Schedule.GetType() == typeof(WeeklyTrigger))
                    {
                        WeeklyTrigger weekly = (WeeklyTrigger)request.QueueWorkItem.Schedule;  
                        if (request.QueueWorkItem.NextExecuted.Ticks ==  0)
                            nextExecuted = request.QueueWorkItem.Created;
                        else
                            nextExecuted = request.QueueWorkItem.NextExecuted;

                        int between = GetBetweenDay(weekly);
                        DateTime recursPoint;
                        nextExecuted = nextExecuted.AddDays(between);

                        if ((nextExecuted - request.QueueWorkItem.RecursPoint).Days >= 7)
                        {
                            between = (7 * weekly.RecursEvery);
                            nextExecuted = nextExecuted.AddDays(between);
                            recursPoint = DateTime.Now;

                            db.Execute("UpdateQueueRecursPoint", new
                            {
                                SeqNbr = request.QueueWorkItem.SeqNbr,
                                RecursPoint = recursPoint
                            });
                        }

                        db.Execute("UpdateQueueNextExecuted", new
                        {
                            SeqNbr = request.QueueWorkItem.SeqNbr,
                            NextExecuted = nextExecuted
                        }); 
                    }
                    else if(request.QueueWorkItem.Schedule.GetType() == typeof(DailyTrigger))
                    {
                        DailyTrigger daily = (DailyTrigger)request.QueueWorkItem.Schedule;
                        if (request.QueueWorkItem.NextExecuted.Ticks == 0)
                            nextExecuted = request.QueueWorkItem.Created;
                        else
                            nextExecuted = request.QueueWorkItem.NextExecuted;

                        nextExecuted = nextExecuted.AddDays(daily.RecursEvery);

                        db.Execute("UpdateQueueNextExecuted", new
                        {
                            SeqNbr = request.QueueWorkItem.SeqNbr,
                            NextExecuted = nextExecuted
                        }); 
                    }
                }
                db.Close();
            }
            return dataEncode;
        }

        private int GetBetweenDay(WeeklyTrigger trigger)
        {

            int between = 0;
            int startCounting = -1;
            string current = DateTime.Now.DayOfWeek.ToString();
             
            Type typeWeekly = trigger.GetType();
            PropertyInfo propInfo;
            int value;
            for (int x = 2; x >= 1; x--)
            { 
                for (int i = 0; i < days.Length; i++)
                {
                    if (days[i] == current && (startCounting == -1))
                    {
                        if (startCounting == -1)
                            startCounting = i + 1;  
                    }
                    else if (startCounting > 0)
                    {
                        between += 1;
                        propInfo = typeWeekly.GetProperty(days[i]);
                        value = (int)propInfo.GetValue(trigger, null);

                        if (value == 1)
                        { 
                            x = 1;
                            break; 
                        }
                    }  
                }
            }  
            return between;
        }

        private int[] Sorting(string[] source)
        {
            int temp;
            int[] buffer = new int[source.Length];
            for (int i = 0; i <= source.Length - 1; i++)
            {
                for (int j = i + 1; j <= source.Length - 1; j++)
                {
                    if (Convert.ToInt32(source[i]) > Convert.ToInt32(source[i + 1]))
                    {
                        temp = Convert.ToInt32(source[i + 1]);
                        buffer[i + 1] = Convert.ToInt32(source[i]);
                        buffer[i] = temp;
                    }  
                    else
                    {
                        buffer[i] = Convert.ToInt32(source[i]);
                    }
                } 
                if (i == source.Length - 1)
                    buffer[i] = Convert.ToInt32(source[i]);
            }
            return buffer;
        }

        public override string OnBeforePull(ParameterizedMap map)
        {
            throw new NotImplementedException();
        }

        public override string OnAfterPull(ParameterizedMap map)
        {
            throw new NotImplementedException();
        }
    }
}

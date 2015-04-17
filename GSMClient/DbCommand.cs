﻿using Texaco.Database.SqlLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texaco.Database.Petapoco;
using Texaco.Database;
using GSMServerModel;
using Cryptography;

namespace GSMClient
{
    public class DbCommand : BaseGSMCommand, IDisposable
    {
        private bool disposed = false;
        private Texaco.Database.Petapoco.PetapocoDbManager dbManager; 
        public ISqlLoader SqlLoader { get; set; }
         
        public DbCommand(string connectionString, string providerName) 
            : base(new DbConnection(connectionString, providerName))
        {
            this.Initiliaze((DbConnection)this.Connection);
        }

        public DbCommand(DbConnection connection)
            : base(connection)
        {
            this.Initiliaze(connection);
        }
         
        private void Initiliaze(DbConnection connection)
        {
            ResourceSqlLoader loader = new ResourceSqlLoader("resources.sql.loader", 
                string.Join(".", new string[] { this.GetType().Namespace, "SqlFiles" }), 
                System.Reflection.Assembly.GetAssembly(this.GetType()));

            dbManager = new Texaco.Database.Petapoco.PetapocoDbManager(null, null);

            dbManager.AddSqlLoader(loader);
            dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
            {
                ConnectionString = ((DbConnection)connection).ConnectionString,
                IsDefault = true,
                ProviderName = ((DbConnection)connection).ProviderName,
                Name = "smsgw"
            });

        }
         
        private string GetID()
        {
            //example of ID
            // 1 or 2 digit represent day
            // 1 digit represent month
            // two digit represent year
            // C is indicates generated by gsm client
            // the rest is randomize
            //9J15H11082335

            string month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month).Substring(0,1).ToUpper();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
              
            return string.Join("", new string[] 
            { 
                DateTime.Now.Day.ToString(),
                month,
                year,
                "C",
                Generate(Guid.NewGuid().ToString().Replace("-", ""), NumberArray, 7)
            }); 
        }

        private const string LetterArray = "WECBUFGHAJKLYRQOPTIXVDMZ";
        private const string NumberArray = "8094263157";

        public static string Generate(string PatternLetter, string PatterNumber, byte Length)
        {
            double random1 = 1.0;
            int rp;
            int stoprp;
            Int32 arrIndex;
            StringBuilder sb = new StringBuilder();
            string randomLetter;
            Random rnd = new Random();
            char[] Key_Letters;
            char[] Key_Numbers;

            Key_Letters = PatternLetter.ToCharArray();
            Key_Numbers = PatterNumber.ToCharArray();

            for (int i = 0; i <= (Length - 1); i++)
            {
                stoprp = 0;
                rp = rnd.Next(1, 10);
                while (stoprp < rp)
                {
                    random1 = rnd.NextDouble();
                    stoprp += 1;
                }

                arrIndex = -1;
                if ((int)(random1 * 111) % 2 == 0)
                {
                    do
                    {
                        arrIndex = Convert.ToInt16(Key_Letters.GetUpperBound(0) * random1);
                    } while (arrIndex < 0);
                    randomLetter = Key_Letters[arrIndex].ToString();
                    if ((int)((arrIndex * random1) * 99) % 2 != 0)
                    {
                        randomLetter = Key_Letters[arrIndex].ToString();
                        randomLetter = randomLetter.ToUpper();
                    }
                    sb.Append(randomLetter);
                }
                else
                {
                    do
                    {
                        arrIndex = Convert.ToInt16(Key_Numbers.GetUpperBound(0) * random1);
                    } while (arrIndex < 0);
                    sb.Append(Key_Numbers[arrIndex]);
                }
            }
            return sb.ToString();
        }

        public override string Write()
        {
            if (Request == null)
                throw new ArgumentNullException("Request");

            int executed  = -1;
            Texaco.Database.IDataCommand db = dbManager.GetDatabase("smsgw");
            string scheduleID = GenerateID();
            string seqNbr = GetID();

            executed = db.Execute("InsertQueueWorkItem", new
            {
                SeqNbr = seqNbr,
                Command = this.Request.QueueWorkItem.Command,
                ScheduleID = scheduleID == null ? "" : scheduleID,
                Created = this.Request.QueueWorkItem.Created,
                Enabled = Convert.ToInt32(this.Request.QueueWorkItem.Enabled)
            });

            if (executed > 0 && !string.IsNullOrEmpty(scheduleID))
            {
                switch(scheduleID.Substring(0,1))
                {
                    case "D" :
                        DailyTrigger dailyTrigger = (DailyTrigger)this.Request.QueueWorkItem.Schedule;
                        executed = db.Execute("InsertDailyTrigger", new
                        {
                            ID = scheduleID,
                            RecursEvery = dailyTrigger.RecursEvery 
                        });
                        break;
                    case "W" :
                        WeeklyTrigger weeklyTrigger = (WeeklyTrigger)this.Request.QueueWorkItem.Schedule;
                        executed = db.Execute("InsertWeeklyTrigger", new
                        {
                            ID = scheduleID,
                            RecursEvery = weeklyTrigger.RecursEvery,
                            Sunday = Convert.ToInt32(weeklyTrigger.Sunday),
                            Monday = Convert.ToInt32(weeklyTrigger.Monday),
                            Tuesday = Convert.ToInt32(weeklyTrigger.Tuesday),
                            Wednesday = Convert.ToInt32(weeklyTrigger.Wednesday),
                            Thursday = Convert.ToInt32(weeklyTrigger.Thursday),
                            Friday = Convert.ToInt32(weeklyTrigger.Friday),
                            Saturday = Convert.ToInt32(weeklyTrigger.Saturday)
                        });
                        break;
                    case "M" :
                        MonthlyTrigger monthlyTrigger = (MonthlyTrigger)this.Request.QueueWorkItem.Schedule;
                        executed = db.Execute("InsertMonthlyTrigger", new
                        {
                            ID = scheduleID, 
                            Days = monthlyTrigger.Days,
                            January = monthlyTrigger.January,
                            February = monthlyTrigger.February,
                            March = monthlyTrigger.March,
                            April = monthlyTrigger.April,
                            May = monthlyTrigger.May,
                            June = monthlyTrigger.June,
                            July = monthlyTrigger.July,
                            August = monthlyTrigger.August,
                            September = monthlyTrigger.September,
                            October = monthlyTrigger.October,
                            November = monthlyTrigger.November,
                            December = monthlyTrigger.December
                        });
                        break;
                }
            }
            
            db.Close();

            if (executed == 1)
                return seqNbr;
            else
                return string.Empty;
        }

        public override int Write(string data)
        {
            Request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(data);
            string result = this.Write();
            if (string.IsNullOrEmpty(result))
                return 0;
            else
                return 1; 
        }

        public override int Write(byte[] data, int startIndex, int length)
        {
            string dataString = ASCIIEncoding.ASCII.GetString(data, startIndex, length);
            Request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(dataString);
            string result = this.Write();
            if (string.IsNullOrEmpty(result))
                return 0;
            else
                return 1;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                base.Connection.Dispose(); 
            }
            disposed = true;
        } 
    }
}

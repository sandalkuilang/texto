using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSMServerModel;
using Crypto;

namespace UnitTestGSMClient
{
    [TestClass]
    public class GSMClientUnitTest
    {
        [TestMethod]
        public void TestMonthlyTrigger()
        { 
            Header header = new Header("FRITZ", "SMS-GW", "sendSMS");
            Request request = new Request(null, header, null);
            QueueWorkItem item = new QueueWorkItem();
            item.Command = string.Format(GSMClient.Command.CommandCollection.SMSSend, "082125164012", "hai");
            item.Enabled = true;
            MonthlyTrigger trigger = new MonthlyTrigger();
            trigger.December = 1;
            trigger.Days = "14,19";
            item.Schedule = trigger;
             
            request.QueueWorkItem = item;
            GSMClient.BaseGSMCommand dbCommand = new GSMClient.DbCommand("Data Source=.;Initial Catalog=SMSGW;User Id=sa;Password=sa123;Integrated Security=False;MultipleActiveResultSets=True", "System.Data.SqlClient");
            dbCommand.Request = request;
            dbCommand.Write(); 

        }

        [TestMethod]
        public void InsertWeeklyTrigger()
        {
            Header header = new Header("FRITZ", "SMS-GW", "sendSMS");
            Request request = new Request(null, header, null);
            QueueWorkItem item = new QueueWorkItem();
            item.Command = string.Format(GSMClient.Command.CommandCollection.SMSSend, "082125164012", "hai");
            item.Enabled = true;
            WeeklyTrigger week = new WeeklyTrigger();
            week.Monday = 1;
            week.Thursday = 1;
            week.RecursEvery = 1;
            item.Schedule = week;

            IKeySym keySym = new PrivateKey();
            request.QueueWorkItem = item;
            GSMClient.BaseGSMCommand dbCommand = new GSMClient.DbCommand("Data Source=.;Initial Catalog=SMSGW;User Id=sa;Password=sa123;Integrated Security=False;MultipleActiveResultSets=True", "System.Data.SqlClient");
            dbCommand.Request = request;
            dbCommand.Write(); 
        }

        [TestMethod]
        public void InserDailyTrigger()
        {
            Header header = new Header("FRITZ", "SMS-GW", "sendSMS");
            Request request = new Request(null, header, null);
            QueueWorkItem item = new QueueWorkItem();
            item.Command = string.Format(GSMClient.Command.CommandCollection.SMSSend, "082125164012", "hai");
            item.Enabled = true;
            DailyTrigger trigger = new DailyTrigger();
            trigger.RecursEvery = 1;
            item.Schedule = trigger;

            IKeySym keySym = new PrivateKey();
            request.QueueWorkItem = item;
            GSMClient.BaseGSMCommand dbCommand = new GSMClient.DbCommand("Data Source=.;Initial Catalog=SMSGW;User Id=sa;Password=sa123;Integrated Security=False;MultipleActiveResultSets=True", "System.Data.SqlClient");
            dbCommand.Request = request;
            dbCommand.Write();
        }   


        [TestMethod]
        public void SendTcpConnection()
        {
            Header header = new Header("TMMIN", "SMS-GW", "sendSMS");
            Request request = new Request(null, header, null);
            QueueWorkItem item = new QueueWorkItem();
            item.Command = string.Format(GSMClient.Command.CommandCollection.SMSSend, "082125164012", "hallo");
            item.Enabled = true;
            DailyTrigger trigger = new DailyTrigger();
            trigger.RecursEvery = 1;
            item.Schedule = trigger;

            try
            {
                IKeySym keySym = new PrivateKey();
                request.QueueWorkItem = item;
                GSMClient.BaseGSMCommand dbCommand = new GSMClient.TcpCommand("192.168.0.198", 13005, keySym);
                dbCommand.Connection.Open();
                dbCommand.Request = request;
                dbCommand.Write();
                dbCommand.Connection.Close();
            }
            catch (Exception)
            {

            }
        }
    }

    [TestClass]
    public class PrivateKey : IKeySym
    {
         
        public byte[] GetKey()
        {
            return new byte[] { 0, 36, 0, 0, 4, 128, 0, 0, 148, 0, 0, 0, 6, 2, 0, 0, 0, 36, 0, 0, 82, 83, 65, 49, 0, 4, 0, 0, 1, 0, 1, 0, 235, 154, 120, 189, 24, 181, 220, 20, 179, 177, 213, 21, 175, 214, 184, 55, 144, 45, 41, 140, 227, 123, 3, 178, 132, 197, 217, 25, 44, 154, 213, 246, 210, 21, 225, 3, 219, 119, 88, 132, 244, 175, 168, 224, 199, 184, 98, 229, 66, 231, 83, 61, 89, 95, 225, 218, 145, 197, 202, 149, 100, 7, 32, 235, 46, 129, 120, 110, 131, 113, 109, 170, 197, 16, 255, 174, 240, 118, 130, 99, 101, 126, 107, 9, 75, 189, 64, 56, 178, 86, 40, 11, 56, 90, 160, 247, 209, 57, 84, 151, 47, 224, 42, 50, 126, 251, 182, 55, 3, 172, 224, 227, 163, 54, 184, 215, 13, 130, 29, 198, 73, 80, 157, 69, 230, 54, 97, 168 };
        }
    }
}

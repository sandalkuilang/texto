using GSMClient.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSTest
{
    class Program
    {
        static void Main(string[] args)
        {  
            Console.WriteLine("This application used to send SMS request every minute/hour");
            string phoneNumber, message;
            StringBuilder phonenumberRandom = new StringBuilder();

            int timeSleep = 1;
            string betweenTimeSleep;

            /// read minute
            Console.Write("Input ticks in minute : ");
            betweenTimeSleep = Console.ReadLine();

            /// generate next value sleep time
            if (betweenTimeSleep.Contains("-"))
            {
                string[] betweenValue = betweenTimeSleep.Split('-');
                Random rnd = new Random();
                timeSleep = rnd.Next(Convert.ToInt32(betweenValue[0]), Convert.ToInt32(betweenValue[1]));
            }
            else
            {
                timeSleep = Convert.ToInt32(betweenTimeSleep);
            }

            /// read phone number
            Console.Write("Input phone number : ");
            phoneNumber = Console.ReadLine();

            /// read message
            Console.Write("Input message : ");
            message = Console.ReadLine();

            Console.WriteLine("Press Ctrl+C to exit");
            Console.WriteLine("");

            GSMClient.IClient client;
            int count = 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("This application used to send SMS request every minute/hour");
                Console.WriteLine("Input ticks in minute : {0}", timeSleep);
                Console.WriteLine("Input phone number : {0}", phoneNumber);
                Console.WriteLine("Input message : {0}", message); 
                Console.WriteLine("Press Ctrl+C to exit");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("sending request : {0}, last: {1}, next: {2}", count, DateTime.Now.ToShortTimeString(), DateTime.Now.AddMinutes(timeSleep).ToShortTimeString());
                Console.ForegroundColor = ConsoleColor.Gray;
                
                try
                {
                    client = new GSMClient.Client(ConfigurationSettingProvider.Instance().Setting.IP, ConfigurationSettingProvider.Instance().Setting.Port);
                    client.Open();

                    /// this is random phonenumber mode
                    /// you can't trace that message!!!
                    if (phoneNumber.Equals("random"))
                    {
                        phonenumberRandom.Clear();
                        phonenumberRandom.AppendLine(string.Format("08{0}", RandomizeGenerator.Generate(10)));
                        client.Send(string.Format(CommandCollection.SMSSend, phoneNumber, string.Format("{0} {1}", message, count)));
                    }
                    else
                    {/// normally phonenumber mode
                        client.Send(string.Format(CommandCollection.SMSSend, phoneNumber, string.Format("{0} {1}", message, count)));
                    }

                    /// generate next value sleep time
                    if (betweenTimeSleep.Contains("-"))
                    {
                        string[] betweenValue = betweenTimeSleep.Split('-');
                        Random rnd = new Random();
                        timeSleep = rnd.Next(Convert.ToInt32(betweenValue[0]), Convert.ToInt32(betweenValue[1]));
                    } 
                    client.Close();
                }
                catch(System.Net.Sockets.SocketException se)
                {
                    Console.WriteLine(se.Message);
                } 
                count += 1;
                System.Threading.Thread.Sleep(timeSleep * (60000));
            }
        }
    }
}

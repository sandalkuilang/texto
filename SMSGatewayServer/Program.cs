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
using System.Net.Sockets; 
using System.IO;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using GSMServer;
using GSMCommunication.Feature;
using GSMServer.Configuration; 

namespace SMSGatewayServer
{
    class Program
    {
        static Server server;
        static bool threadCreate;
        static IConfiguration configuration = new ApplicationSettings();
        static TimeSpan lifetime;
        static bool deviceUndetect;
        static bool alreadyWrite;

        static void Main(string[] args)
        {
            InitializeServer();
            GetInformation();
            ReceiveInput();
        }

        private static void InitializeServer()
        { 
            StringBuilder stopChar = new StringBuilder();
            threadCreate = false;

            if (server != null)
            {
                server.Dispose();   
            }
             
            ThreadPool.QueueUserWorkItem(new WaitCallback(createServer));

            char[] spinChars = new char[] { '|', '/', '-', '\\' }; 
            while (!threadCreate)
            { 
                foreach (char spinChar in spinChars)
                { 
                    Console.WriteLine("detecting GSM device " + spinChar.ToString());
                    System.Threading.Thread.Sleep(100);
                    Console.Clear(); 
                } 
            } 
        }

        private static void GetInformation()
        {
            Console.Clear();
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string productNAme = fvi.ProductName;
            string legalCopyright = fvi.LegalCopyright;
            string version = assembly.GetName().Version.ToString(3);

            Console.WriteLine(string.Format("{0} [Version {1}]\n{2}\n", productNAme, version, legalCopyright));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connection created with IP {0}, Port {1}", server.LocalEndPoint.Address, server.LocalEndPoint.Port + "\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (server.GetAvailableConnections().Count > 0)
            {
                foreach (GSMCommunication.Feature.BasicInformation bc in server.GetAvailableConnections())
                {
                    if (!alreadyWrite)
                    {
                        alreadyWrite = true;
                        Console.WriteLine("SIM Cards : {0} (last updated : {1})", server.GetAvailableConnections().Count, DateTime.Now.ToString());
                        Console.WriteLine("---------------------------------------------------");
                    } 
                    BaseResult<GenericTypeResult<string>> manufaturer, model, softVersion; 
                    try
                    {
                        manufaturer = bc.GetManufacturer();
                        model = bc.GetModelInformation();
                        softVersion = bc.GetSoftwareVersion();
                        BaseResult<GenericTypeResult<int>> signalResult = bc.GetSignalQuality();
                        int signal = signalResult.Response.Result;

                        string signalCondition = "Unknown";
                        if (signal <= -111 & signal >= -255)
                            signalCondition = "No Network";
                        else if (signal <= -105 & signal >= -110)
                            signalCondition = "Bad";
                        else if (signal <= -95 & signal >= -104)
                            signalCondition = "Marginal";
                        else if (signal <= -83 & signal >= -94)
                            signalCondition = "OK";
                        else if (signal <= -75 & signal >= -82)
                            signalCondition = "Good";
                        else if ((signal <= -50) && (signal >= -74))
                            signalCondition = "Excellent";

                        Console.WriteLine("Manufacturer: {0}", manufaturer.Response.Result);
                        Console.WriteLine("Model: {0}", model.Response.Result);
                        Console.WriteLine("Software Ver: {0}", softVersion.Response.Result);
                        if (!string.IsNullOrEmpty(bc.OwnNumber))
                            Console.WriteLine("Port: {0}\nNumber/Operator: {1}/{2}\nSignal Quality: {3} dBm/{4}", bc.Connector.PortName, bc.OwnNumber, bc.Operator, signal.ToString(), signalCondition);
                        else
                            Console.WriteLine("Port: {0}\nOperator: {1}\nSignal Quality: {2} dBm/{3}", bc.Connector.PortName, bc.Operator, signal.ToString(), signalCondition);


                        if (server.Plugins.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Plugins : {0}", server.Plugins.Count);
                            foreach (string key in server.Plugins.Keys)
                            {
                                Console.WriteLine("{0}", server.Plugins[key].GetName());
                            }
                        }

                        server.DeviceRemoved += server_DeviceRemoved;
                        Console.WriteLine();
                        Console.WriteLine("-s   Shutdown the server");
                        Console.WriteLine("-i   Refresh SIM Card information");
                        Console.WriteLine("-r   Restart the server, all clients connection will be closed");
                        Console.WriteLine("-t   Show lifetime the server");
                        deviceUndetect = false;
                    }
                    catch (InvalidOperationException)
                    {
                        // the port is closed.
                        deviceUndetect = true;
                        alreadyWrite = false;
                    } 
                } 
            }
            else
            {
                deviceUndetect = true;
            }

            if (deviceUndetect)
            {
                Detecting();
            }
        }

        static void ReceiveInput()
        {
            while (true)
            {
                alreadyWrite = false;
                Console.Write(">");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "-s":
                        server.EndAcceptClient();
                        server.Dispose();
                        return;
                    case "-i":
                        GetInformation();
                        break;
                    case "-r":
                        server.EndAcceptClient();
                        InitializeServer();
                        GetInformation();
                        break;
                    case "-t":
                        TimeSpan life = new TimeSpan(DateTime.Now.Ticks - lifetime.Ticks);
                        Console.WriteLine("server tickcount {0}", life.ToString());
                        break;
                }
            }
        }

        static void Detecting()
        {
            /// detect for stack overflow
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames) 
            if (stackFrames.Length > 4)
            {
                if (stackFrames[2].GetNativeOffset() == stackFrames[4].GetNativeOffset())
                {
                    server.Dispose();
                    createServer(null);
                    GetInformation();
                    ReceiveInput();
                    return;
                }
            }

            while (deviceUndetect)
            {
                if (!alreadyWrite)
                {
                    alreadyWrite = true;
                    Console.WriteLine();
                    Console.WriteLine("> detecting GSM device...");
                }
                Thread.Sleep(1000);
                if (server.Detect())
                {
                    deviceUndetect = false;
                    alreadyWrite = false;
                    server.Refresh();
                    break;
                }
            }
            GetInformation();
            ReceiveInput();
        }

        static void server_DeviceRemoved(object sender, EventArgs e)
        {
            Detecting(); 
        }

        private static void createServer(object state)
        {
            lifetime = new TimeSpan(DateTime.Now.Ticks);
            string portStr = ((ApplicationSettings)configuration).General.DefaultPort;
            int port = 0;
            if (!string.IsNullOrEmpty(portStr))
                port = Convert.ToInt32(portStr);

            string ipAddres = ((ApplicationSettings)configuration).General.DefaultIP;
           
            server = new Server(ipAddres, port);
             
            const string errorFormat = "{0} {1}     {2}     {3}";
            try
            {
                server.BeginAcceptClient();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(errorFormat, ex.ToString(), ex.Source, ex.Message, ex.StackTrace));
            }
            threadCreate = true;
        } 
    }
}

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
using System.Threading.Tasks;
using System.Threading;
using Sockets;
using GSMCommunication;
using System.Xml.Linq;
using System.Xml;
using System.IO.Ports;
using System.Reflection;
using System.Net;
using GSMServer.Contracts; 
using System.Diagnostics;
using GSMCommunication.Feature;
using Cryptography;
using GSMServerModel;
using GSMServer.Plugin;
using GSMServer.Contracts.InternalLogging;
using GSMServer.Configuration;
using GSMServer.Worker;
using System.Text.RegularExpressions;
using GSMServer.Security;
using System.Net.Sockets;
using Texaco.Database.Petapoco;
using Texaco.Database.SqlLoader;
using Texaco.Database; 

namespace GSMServer
{
    [ActionServer]
    [Config]
    [ActionGeneral]
    [PhoneBook]
    [Call]
    [SMS] 
    [AuthorizationRequestFilter]
    public class Server : TcpMonitor, IDisposable, IServer
    { 
        private bool disposed; 
          
        private System.Timers.Timer timerReadQueue;
        private System.Timers.Timer timerProcessRequestQueue;

        private List<BasicInformation> availableConnections;
        private IQueue<BasicInformation> portColletion; 
        private WorkerPoolManager workerPoolManager;  
        private PluginObserver pluginMap;
          
        public event MessageSentEventHandler DataSent;
        public event MessageReceivedEventHandler DataReceived;
        public event DeviceRemovedEventHandler DeviceRemoved;

        public List<BasicInformation> Connection
        { 
            get 
            { 
                return portColletion.ToList(); 
            } 
        }

        public IDictionary<string, IPlugin> Plugins
        { 
            get 
            { 
                return pluginMap.ToDictionary(); 
            } 
        }

        public Server(int port)
            : base("", port)
        { 
            Initialize("", port);
        }

        public Server(string ipAddress, int port)
            : base(ipAddress, port)
        { 
            Initialize(ipAddress, port); 
        } 
        public void Refresh()
        {
            Initialize(this.LocalEndPoint.Address.ToString(), this.LocalEndPoint.Port);
        }

        private void InitializeDatabase()
        {
            ApplicationSettings applicationSettings = (ApplicationSettings)ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();  

            PetapocoDbManager dbManager = new Texaco.Database.Petapoco.PetapocoDbManager(null, null);
            dbManager.AddSqlLoader(new ResourceSqlLoader("resources-sql-loader", "SqlFiles", Assembly.GetCallingAssembly()));
            dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
            {
                ConnectionString = applicationSettings.ConnectionStrings[DatabaseNames.SMSGW].ConnectionString,
                IsDefault = true,
                Name = applicationSettings.ConnectionStrings[DatabaseNames.SMSGW].Name,
                ProviderName = applicationSettings.ConnectionStrings[DatabaseNames.SMSGW].ProviderName
            });

            ObjectPool.Instance.Register<IDbManager>().ImplementedBy(dbManager); 
        }

        private void Initialize(string ipAddress, int port)
        {
            /// set culture to US
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            /// register configuration & worker  
            ObjectPool.Instance.Register<IServer>().ImplementedBy(this);
            ObjectPool.Instance.Register<GSMServer.Configuration.IConfiguration>().ImplementedBy(new ApplicationSettings());
            ObjectPool.Instance.Register<Worker.IPipeline>().ImplementedBy(new DefaultPipeline(new ActionInvokerLookup(this)));

            /// register logging IInternalLogging
            ObjectPool.Instance.Register<IInternalLogging>().ImplementedBy(new BaseInternalLogging());
            ObjectPool.Instance.Register<ISMSLogging>().ImplementedBy(new ArchiveSMSLogging()); 

            /// initialize database
            InitializeDatabase();
             
            GSMServer.Configuration.IConfiguration settings = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();

            portColletion = new RandomConnectionProvider(); 
            workerPoolManager = new WorkerPoolManager();
            workerPoolManager.AddPool(new DatabaseWorkerPool());
            workerPoolManager.AddPool(new MemoryWorkerPool());

            pluginMap = new PluginObserver();

            string countryCode = ((ApplicationSettings)settings).General.CountryCode;
            string IsNullintervalProcessQueue = ((ApplicationSettings)settings).General.IntervalWorkerQueue.ToString();
            string IsNullintervalReadMessage = ((ApplicationSettings)settings).General.IntervalReadMessage.ToString();

            int intervalProcessQueue;
            int intervalReadMessage = TimeConstant.READ_TIMEOUT;

            if (string.IsNullOrEmpty(IsNullintervalProcessQueue))
                throw new System.Configuration.ConfigurationErrorsException("Interval Queue cannot be null or zero.");
            else
            {
                intervalProcessQueue = Convert.ToInt32(IsNullintervalProcessQueue);
                if (intervalProcessQueue < 1000)
                {
                    throw new InvalidOperationException("Interval Queue may cause invalid operation while sending SMS because value is to fast.");
                }
            }

            BasicInformation connection;
            StringBuilder portname, sparity, sstop, shand;
            StringBuilder serviceCenter = new StringBuilder();
            bool pduMode = false; 
            int baudrate, databits;
            Parity parity;
            StopBits stopBits;
            Handshake handshake;

            /// for comparison is the configuration in accordance with the detected serial ports by OS?
            string[] ports = System.IO.Ports.SerialPort.GetPortNames(); 

            foreach (GSMServer.Configuration.SerialPort node in ((ApplicationSettings)settings).SerialPorts.Items)
            {
                portname = new StringBuilder(node.PortName);
                pduMode = false;
                pduMode = node.PDUMode; 
                if (!string.IsNullOrEmpty(node.ServiceCenter))
                {
                    serviceCenter = new StringBuilder(countryCode + node.ServiceCenter.Substring(1, node.ServiceCenter.Length - 1));
                } 

                baudrate = Convert.ToInt32(node.BaudRate);
                databits = Convert.ToInt32(node.DataBits);
                parity = new Parity();
                sparity = new StringBuilder(node.Parity.ToLower());
                if (sparity.ToString() == "even")
                    parity = Parity.Even;
                else
                    if (sparity.ToString() == "mark")
                        parity = Parity.Mark;
                    else
                        if (sparity.ToString() == "none")
                            parity = Parity.None;
                        else
                            if (sparity.ToString() == "odd")
                                parity = Parity.Odd;
                            else
                                if (sparity.ToString() == "space")
                                    parity = Parity.Space; 
                stopBits = new StopBits();
                sstop = new StringBuilder(node.StopBits.ToLower());
                if (sstop.ToString() == "none")
                    stopBits = StopBits.None;
                else
                    if (sstop.ToString() == "one")
                        stopBits = StopBits.One;
                    else
                        if (sstop.ToString() == "onepointfive")
                            stopBits = StopBits.OnePointFive;
                        else
                            if (sstop.ToString() == "two")
                                stopBits = StopBits.Two;
                handshake = new Handshake();
                shand = new StringBuilder(node.Handshake.ToLower());
                if (shand.ToString() == "none")
                    handshake = Handshake.None;
                else
                    if (shand.ToString() == "requesttosend")
                        handshake = Handshake.RequestToSend;
                    else
                        if (shand.ToString() == "RequestToSendXOnXOff")
                            handshake = Handshake.RequestToSendXOnXOff;
                        else
                            if (shand.ToString() == "xonxoff")
                                handshake = Handshake.XOnXOff;

                /// check whether port there is in collection?
                if (ports.Where(b => b.ToLower().Equals(portname.ToString().ToLower())).SingleOrDefault() != null)
                {
                    connection = new BasicInformation(portname.ToString(), baudrate, parity, stopBits, databits, handshake, serviceCenter.ToString(), pduMode);
                    InitSerialPort(connection);
                }
            }
                  
            GSMServer.Plugin.Plugin plugin;
            foreach (PluginElement pluginElement in ((ApplicationSettings)settings).Plugins.Items)
            {
                plugin = PluginActivator.Create(pluginElement.AssemblyFile, pluginElement.Type);
                if (plugin != null)
                {
                    pluginMap.Add(pluginElement.AssemblyFile, plugin);
                }
            } 

            availableConnections = portColletion.ToList();
            if (portColletion.Count > 0)
            { 
                intervalProcessQueue = (intervalProcessQueue / portColletion.Count) + TimeConstant.DEFAULT_INTERVAL_QUEUE;
            }
            else
            {
                intervalProcessQueue = (intervalProcessQueue) + TimeConstant.DEFAULT_INTERVAL_QUEUE;
            }

            timerReadQueue = new System.Timers.Timer(intervalReadMessage);
            timerReadQueue.Elapsed += timerReadQueue_Elapsed;

            timerProcessRequestQueue = new System.Timers.Timer(intervalProcessQueue);
            timerProcessRequestQueue.Elapsed += TimerProcessingRequest_Elapsed;

            if (portColletion.Count > 0)
            {
                if (!this.Active) 
                    this.BeginAcceptClient();
            }

            base.PacketReceived += OnPacketReceived;
            base.Connected += OnClientConnected;
            base.Disconnected += OnClientDisconnect;
            base.Closed += OnClosed;
            base.Open += OnOpen;
        }

        private void InitSerialPort(BasicInformation connection)
        {
            //// let the connection remains open until the server is closed/disposed
            connection.Connector.Open();

            connection.OnBeginExecuting();
 
            BaseResult<GenericTypeResult<string>> imsi = connection.GetIMSI();
            if (!string.IsNullOrEmpty(imsi.Response.Result))
            {
                connection.GetServiceCenter();
                connection.SetErrorMessageFormat(1);
                connection.GetOperator();

                PhoneBook pb = new PhoneBook(connection);
                pb.SetPhoneBookMemory(MemoryStorage.SIMOwnNumber);
                pb.GetInfo();

                SMS sms = new SMS(connection);
                sms.SetMessageStorage(MemoryStorage.MobilePhonebook, MemoryStorage.MobilePhonebook, MemoryStorage.MobilePhonebook);
                sms.SetMessageFormat(connection.PDUMode);

                string prefixOwnNumber = string.Empty;
                if (string.IsNullOrEmpty(prefixOwnNumber))
                {
                    GSMServer.Configuration.IConfiguration configuration = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();
                    prefixOwnNumber = ((ApplicationSettings)configuration).General.PrefixOwnNumber;
                }

                BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> list = pb.ReadPhoneBook(MemoryStorage.SIMOwnNumber, 1, -1);
                if (list.Response.Result.Count > 0)
                {
                    foreach (PhoneNumberInfo info in list.Response.Result)
                    {
                        if (info.Name.Equals(prefixOwnNumber))
                        {
                            connection.OwnNumber = info.PhoneNumber;
                            break;
                        }
                    }
                }
                portColletion.Add(connection);
            }
            connection.OnEndExecuting();
        }

        private void timerReadQueue_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (portColletion)
            {
                BasicInformation connection = portColletion.Get();
                if (connection == null) return;
                ParameterizedMap param = new ParameterizedMap();
                param.Add("base", connection);

                string responseOut = string.Empty;
                Worker.IPipeline currentWorker = ObjectPool.Instance.Resolve<Worker.IPipeline>();
                if (currentWorker != null)
                { 
                    responseOut = currentWorker.Pull(param);

                    if (!string.IsNullOrEmpty(responseOut) && (responseOut != "[]"))
                    {
                        OnDataReceived(responseOut);
                    } 
                }  
            } 
        }
         
        private void TimerProcessingRequest_Elapsed(object sender, EventArgs e)
        {  
            ////ensures that one thread enter a processing request section, in the same time reading sms section may will be occured.
            ////If another thread tries to enter a locked code, it will wait, block, until the object is released.
            lock(portColletion)
            {
                PacketEventArgs workItem = workerPoolManager.GetJob();
                if (workItem != null)
                { 
                    BasicInformation currentConnection = portColletion.Get();
                    if (currentConnection != null)
                    {
                        List<string> command = new List<string>();
                        ParameterizedMap parsingCommand;
                        try
                        {
                            string dataEncode = Encoding.GetEncoding("ibm850").GetString(workItem.Data);

                            Request request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(dataEncode, new Newtonsoft.Json.JsonSerializerSettings
                            {
                                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects
                            });  
                            ICommandParser<ParameterizedMap> parser = new CommandParser();
                            parsingCommand = parser.Parse(request.QueueWorkItem.Command);

                            DynamicData parameters = new DynamicData();
                            foreach (string key in parsingCommand.ToDictionary().Keys)
                            {
                                if (!key.Equals("Command"))
                                {
                                    parameters.Add(key, parsingCommand.TryGet<object>(key));
                                }
                            }
                            parameters.Add("id", request.QueueWorkItem.SeqNbr == null ? "" : request.QueueWorkItem.SeqNbr);
                            command = parsingCommand.TryGet<string>("Command").Split('.').ToList();

                            ParameterizedMap param = new ParameterizedMap();
                            param.Add("base", currentConnection);
                            param.Add("packet", workItem);
                            param.Add("command", command);
                            param.Add("parameters", parameters); 

                            string responseOut;
                            string encrypted;

                            Worker.IPipeline pipe = ObjectPool.Instance.Resolve<Worker.IPipeline>();

                            pipe.BeforePush(param);
                            responseOut = pipe.Push(param); 
                            OnDataSent(responseOut.ToString());
                            pipe.AfterPush(param);

                            // send back to client if request workItem is send from tcp
                            if (workItem.Client != null)
                            {
                                Crypter crypter = new Crypter(new DefaultConfigurationKey());
                                encrypted = crypter.Encrypt(responseOut.ToString());
                                try
                                {
                                    this.Send(((IPEndPoint)workItem.Client.RemoteEndPoint).Port, ASCIIEncoding.ASCII.GetBytes(encrypted.ToString()));
                                }
                                catch (System.Net.Sockets.SocketException)
                                { 
                                    // client was closed, dont raise error. 
                                }
                                catch (ObjectDisposedException)
                                { 
                                    // client was closed, dont raise error.  
                                }  
                            }   
                        }
                        catch (Exception ex)
                        {
                            RaiseError(currentConnection, ex, "process request");
                        }  
                    }
                    else
                    {
                        workerPoolManager.CurrentWorker.Add(workItem);
                    }
                }
            } 
        }
         
        private void RaiseError(BasicInformation connection, Exception ex, string location)
        {
            portColletion.Add(connection);
            if (portColletion.Count > 0)
                Console.WriteLine("connection has been restored.");
            IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
            log.Write(string.Format("{0} - {1}", ex, location));
        }

        private void OnDataSent(string data)
        {
            try
            {
                //GSMDatabaseLogging.Logging log = new GSMDatabaseLogging.Logging();
                //log.OnDataSent(data);
                pluginMap.NotifySent(data);
            }
            finally
            {
                if (DataSent != null)
                    DataSent(this, data); 
            }
        }

        private void OnDataReceived(string data)
        {
            try
            {
                //GSMDatabaseLogging.Logging log = new GSMDatabaseLogging.Logging();
                //log.OnDataReceived(data);
                pluginMap.NotifyReceived(data);
            } 
            finally
            {
                if (DataReceived != null)
                    DataReceived(this, data);  
            } 
        }

        public void OnDeviceRemoved()
        { 
            if (DeviceRemoved != null)
                DeviceRemoved(this, null); 
        }
         
        private void OnPacketReceived(object sender, PacketEventArgs e)
        {
            lock (workerPoolManager)
            {
                workerPoolManager.AddJob(1, e); 
            }
        }

        private void OnClientConnected(object sender, SocketEventArgs e)
        { 
            // notif when client connected 
        }

        private void OnClientDisconnect(object sender, SocketEventArgs e)
        {// notif when client disconnected

        }

        private void OnClosed(object sender, EventArgs e)
        {
            // notif when server close connection
            if (timerReadQueue != null) timerReadQueue.Stop();
            if (timerProcessRequestQueue != null) timerProcessRequestQueue.Stop(); 
        }

        private void OnOpen(object sender, EventArgs e)
        {
            // refuse connection when no device detected
            if (portColletion.Count == 0)
            {
                this.EndAcceptClient();
            }
            // notif when server open connectiontimerRead.Start();
            if (timerReadQueue != null) timerReadQueue.Start(); 
            if (timerProcessRequestQueue != null) timerProcessRequestQueue.Start(); 
        }
          
        protected override void Dispose(bool disposing)
        { 
            if(!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if(disposing)
                {
                    this.ReleaseResources();
                } 
            }
            disposed = true;
            base.Dispose(disposing);
        }

        private void ReleaseResources()
        {
            if (timerReadQueue != null)
            {
                timerReadQueue.Stop();
                timerReadQueue.Elapsed -= timerReadQueue_Elapsed;
                timerReadQueue.Dispose();
            }
            if (timerProcessRequestQueue != null)
            {
                timerProcessRequestQueue.Stop();
                timerProcessRequestQueue.Elapsed -= TimerProcessingRequest_Elapsed;
                timerProcessRequestQueue.Dispose();
            }

            lock (portColletion)
            {
                BasicInformation port = null;
                while (portColletion.Count > 0)
                {
                    Thread.Sleep(200);
                    port = portColletion.Get();
                    if (port != null)
                    {
                        port.Dispose();
                    }
                }
            }

            portColletion.Clear();
            workerPoolManager.Clear();
            pluginMap.Close();

            base.PacketReceived -= OnPacketReceived;
            base.Connected -= OnClientConnected;
            base.Disconnected -= OnClientDisconnect;
            base.Closed -= OnClosed;
            base.Open -= OnOpen;
             
        }

        public override void BeginAcceptClient()
        {
            base.BeginAcceptClient();
        }

        public override void EndAcceptClient()
        {
            base.EndAcceptClient();
        }

        public override void Start()
        {
            base.BeginAcceptClient(); 
        }

        public override void Stop()
        {
            base.EndAcceptClient();
        }

        public List<BasicInformation> GetAvailableConnections()
        {
            return availableConnections;
        }

        public List<string> GetRegisteredPlugins()
        {
            return Plugins.Keys.ToList();
        } 

        public bool Detect()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            ATController serial;
            string response;
            bool result = false;
            foreach(string com in ports)
            {
                serial = new ATController(com);
                try
                {
                    serial.Open(); 
                    response = serial.Execute(Commands.AT, true);
                    if (response.Contains(Commands.RESULT_OK))
                    {
                        result = true;
                    }
                    serial.Close();
                }
                catch(System.IO.IOException)
                {
                    result = false;
                }
                catch (System.UnauthorizedAccessException)
                {
                    result = false;
                }  
            }
            return result;
        }
         
    }
}

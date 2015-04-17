/*
    Socket
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sockets
{
    public class TcpMonitor : BaseTcpMonitor, IDisposable
    {
        private TcpListener listener;
        private bool listening;  
        private ManualResetEvent eventThread;
        private Thread threadStartAsync;
        private ActionInvoker action;

        public event DisconnectedHandler Disconnected;
        public event ConnectedHandler Connected;
        public event OpenHandler Open;
        public event ClosedHandler Closed;
        public event PacketHandler PacketReceived;
        public event PacketHandler PacketSent;

        public TcpMonitor(int port = 13005) : base(port)
        {
            this.Initialize();
        }

        public TcpMonitor(string ipAddress, int port)
            : base(ipAddress, port)
        {
            this.Initialize();
        }

        protected virtual void Initialize()
        { 
            Encoding = Encoding.GetEncoding("ascii");
            eventThread = new ManualResetEvent(false);
            listener = new TcpListener(LocalEndPoint);
            action = new ActionInvoker(this, "IRequestFilter");
        }

        public override void BeginAcceptClient()
        { 
            listening = true; 
            listener.Start(); 
            OnOpen(new SocketEventArgs(listener.Server));
            threadStartAsync = new Thread(new ThreadStart(this._StartAsync));
            threadStartAsync.IsBackground = true;
            threadStartAsync.Start();
        }

        public override void EndAcceptClient()
        {
            listening = false;
            foreach (int port in Clients.Keys)
            {
                try
                {
                    Clients[port].Shutdown(SocketShutdown.Both);
                    Clients[port].Close();
                }
                catch (ObjectDisposedException)
                { }
                catch (SocketException) { }
            }
            listener.Stop();
            if (threadStartAsync != null)
            {
                threadStartAsync.Abort();
            }
            OnClosed(new SocketEventArgs(listener.Server));
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Send(Socket client, byte[] data)
        {
            client.Send(data);
            OnPacketSent(new PacketEventArgs(client, data, Encoding));
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Send(int port, byte[] data)
        {
            Socket Client;
            if (Clients.ContainsKey(port))
            {
                Client = Clients[port];
                Send(Client, data);
            }
        }

        private void _StartAsync()
        {
            Socket client;
            while (listening)
            {
                try
                {
                    eventThread.Reset();
                    client = listener.AcceptSocket();
                    ThreadPool.QueueUserWorkItem(BeginAsyncCallback, client);
                    eventThread.WaitOne();
                }
                catch (ThreadAbortException)
                { }
                catch (SocketException)
                { }
                catch (ObjectDisposedException)
                { }
            }
        }

        private void BeginAsyncCallback(object ar)
        { 
            try
            { 
                OnConnected(new SocketEventArgs((Socket)ar));
                if (((Socket)ar).Connected)
                    Process(ar); 
            }
            catch (ObjectDisposedException) { } /// shallow exception 
            /// check whether handle is closed or not to avoid ObjectDisposedException: Safe handle has been closed
            if (!eventThread.SafeWaitHandle.IsClosed)
            {
                eventThread.Set();
            }
        } 

        private void Process(object target)
        {
            Socket client = (Socket)target;
            Clients.Add(((IPEndPoint)client.RemoteEndPoint).Port, client); 
            byte[] receive = new byte[client.ReceiveBufferSize];
            try
            {
                int rec;
                while (client.Connected)
                {
                    // sleep 250ms to avoid race condition
                    Thread.Sleep(250);
                    receive = new byte[client.ReceiveBufferSize];
                    rec = client.Receive(receive);
                    if (rec == 0)
                    {
                        break;
                    }
                    Array.Resize<byte>(ref receive, rec);
                    OnPacketReceived(new PacketEventArgs(client, receive, Encoding));
                }
            }
            catch (ObjectDisposedException)
            { }
            catch (SocketException soex)
            {
                if (soex.ErrorCode != 10004)
                {
                    if ((soex.SocketErrorCode == SocketError.ConnectionReset)
                    || (soex.SocketErrorCode == SocketError.ConnectionAborted)
                    || (soex.SocketErrorCode == SocketError.NotConnected)
                    || (soex.SocketErrorCode == SocketError.Shutdown)
                    || (soex.SocketErrorCode == SocketError.Disconnecting))
                    {
                        System.Diagnostics.Debug.WriteLine(ResourceMessages.CONNETION_CLOSED, soex.SocketErrorCode);
                    }
                }
            }
            finally
            {  
                Clients.Remove(((IPEndPoint)client.RemoteEndPoint).Port);
                OnDisconnected(new SocketEventArgs(client));
                client.Close();
                client.Dispose(); 
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected virtual void OnDisconnected(SocketEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected virtual void OnConnected(SocketEventArgs e)
        {
            if (Connected != null)
                Connected(this, e);
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected virtual void OnOpen(SocketEventArgs e)
        {
            if (Open != null)
                Open(this, e);
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected virtual void OnClosed(SocketEventArgs e)
        {
            if (Closed != null)
                Closed(this, e);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public virtual void OnPacketReceived(PacketEventArgs e)
        { 
            lock (e) 
            {       
                if (e.Data.Length > 0)
                {
                    object ret = action.TryInvoke("OnRequesting", new object[] { e });
                    if (ret == null) return;
                    if (ret.GetType().Equals(typeof(bool)))
                    {
                        if (PacketReceived != null)
                        {
                            if ((bool)ret == true)
                                PacketReceived(this, e);
                        }
                    }
                }
                
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected virtual void OnPacketSent(PacketEventArgs e)
        {
            if (PacketSent != null)
                PacketSent(this, e);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources 
                if (eventThread != null)
                {
                    eventThread.Dispose();
                }
                if (listener != null)
                {
                    listener.Stop();
                    listener.Server.Dispose();
                } 
            }
        }
    }
}

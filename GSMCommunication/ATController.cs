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
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using GSMCommunication;
 
namespace GSMCommunication
{
    public class ATController : BaseConnector, IDisposable
    { 
        private static object s_InternalSyncObject = new object();
         
        private bool disposed; 
        private System.Text.Encoding encode;
        private AutoResetEvent waitResponse;
        private StringBuilder response; 
        private StringBuilder dataReceived;
        //private Thread readThread;

        public event ExecutingEventHandler Executing;
        public delegate void ExecutingEventHandler(object sender, EventArgs e);
          

        /// <summary>
        /// Initializes a new instance of the SerialPort class using the specified port name.
        /// </summary>
        /// <param name="portName">The port to use (for example, COM1).</param>
        public ATController(string portName) : base(portName)
        { 
            this.Init(portName, 115000, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.None, 8, System.IO.Ports.Handshake.RequestToSend); 
        }

        public ATController(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handShake)
            : base(portName, baudRate, parity, stopBits, dataBits, handShake)
        { 
            this.Init(portName, baudRate, parity, stopBits, dataBits, handShake);    
        }

        private void Init(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handShake)
        {
            
            base.driver.ReadTimeout = 500;
            base.driver.WriteTimeout = 500;
            
            base.driver.ErrorReceived += driver_ErrorReceived;

            //receiveResponse = true;
            encode = new System.Text.UTF8Encoding(); 
            response = new StringBuilder();
            dataReceived = new StringBuilder();
            waitResponse = new AutoResetEvent(false); 
        }
         
        private void driver_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ((SerialPort)sender).DiscardInBuffer();
            ((SerialPort)sender).DiscardOutBuffer();
        }
         
        public void Read()
        {
            while (base.driver.ReceivedBytesThreshold > 0)
            {
                try
                { 
                    dataReceived.Append(base.driver.ReadLine()); 
                }
                catch (OverflowException)
                {
                    break;
                }
                catch (IOException)
                {
                    break;
                }
                catch (TimeoutException)
                {
                    break;
                }
            }
        }

        public override string Execute(string cmd, bool receiveResponse = true, int delayWhenRead = 500)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                if (!cmd.EndsWith(((char)13).ToString()))
                {
                    if (!(cmd.EndsWith(ControlChars.Cr.ToString()) | cmd.EndsWith(ControlChars.CrLf)))
                        cmd += ControlChars.Cr;
                }
            }

            byte[] packetBuffer = base.Encoding.GetBytes(cmd);
            InitExecute();
             
            lock (s_InternalSyncObject)
            {
                base.driver.Write(packetBuffer, 0, packetBuffer.Length);
            }
            Thread.Sleep(delayWhenRead);
            if (receiveResponse)
            {
                this.Read();
            } 
            return dataReceived.ToString();
        }

        private void InitExecute()
        {
            if (Executing != null)
            { 
                Executing(this, new EventArgs()); 
            }
            waitResponse.Reset(); 
            base.DiscardBuffer();
            dataReceived.Clear(); 
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implementation of Dispose pattern. 
        /// </summary>
        /// <param name="disposing">If disposing equals true, the method has been called directly 
        /// or indirectly by a user's code. Managed and unmanaged resources 
        /// can be disposed. 
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                { 
                    base.driver.ErrorReceived -= driver_ErrorReceived;
                     
                    if (waitResponse != null)
                    {
                        waitResponse.Close();
                        waitResponse.Dispose();
                    } 
                    base.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize. 
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~ATController() 
        {
            this.Dispose(true);
        }
    }
     
} 
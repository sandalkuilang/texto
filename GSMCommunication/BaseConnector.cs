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
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace GSMCommunication
{
    /// <summary>
    /// Represents a serial port resource.
    /// </summary>
    public abstract class BaseConnector
    {
        private static object s_InternalSyncObject = new object();

        protected const int WRITE_TIMEOUT = 500;
        protected const int READ_TIMEOUT = 3100;
        protected const int MAX_LOOP = 10; 

        protected SerialPort driver;

        /// <summary> 
        /// Check whether the connection is open.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return driver.IsOpen;
            }
        }

        /// <summary>
        /// Gets or sets the port for communications, including but not limited to all available COM ports.
        /// </summary>
        public string PortName 
        { 
            get 
            {
                return driver.PortName;
            } 
            set 
            {
                driver.PortName = value;
            } 
        }

        /// <summary>
        /// Gets or sets the serial baud rate.
        /// </summary>
        public int BaudRate 
        { 
            get
            {
                return driver.BaudRate;
            }
            set
            {
                driver.BaudRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the parity-checking protocol.
        /// </summary>
        public Parity Parity
        {
            get
            {
                return driver.Parity;
            }
            set
            {
                driver.Parity = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the standard number of stopbits per byte.
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return driver.StopBits;
            }
            set
            {
                driver.StopBits = value;
            }
        }

        /// <summary>
        /// Gets or sets the standard length of data bits per byte.
        /// </summary>
        public int DataBits 
        { 
            get
            {
                return driver.DataBits;
            } 
            set
            {
                driver.DataBits = value;
            }
        }

        /// <summary>
        /// Gets or sets the handshaking protocol for serial port transmission of data.
        /// </summary>
        public Handshake Handshake 
        { 
            get
            {
                return driver.Handshake;
            }
            set
            {
                driver.Handshake = value;
            }
        }

        /// <summary>
        /// Gets or sets the byte encoding for pre- and post-transmission conversion of text.
        /// </summary>
        public Encoding Encoding 
        { 
            get 
            { 
                return driver.Encoding; 
            } 
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.
        /// </summary>
        public int WriteTimeout 
        { 
            get 
            { 
                return driver.WriteTimeout; 
            } 
            set 
            { 
                driver.WriteTimeout = value; 
            } 
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.
        /// </summary>
        public int ReadTimeout 
        { 
            get 
            { 
                return driver.ReadTimeout; 
            } 
            set
            { 
                driver.ReadTimeout = value; 
            } 
        }

        /// <summary>
        /// Initializes a new instance of the SerialPort class using the specified port name.
        /// </summary>
        /// <param name="portName">The port to use (for example, COM1).</param>
        public BaseConnector(string portName) : this(portName, 115200)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the SerialPort class using the specified port name, baud rate, parity, stop bits, data bits and handshake.
        /// </summary>
        /// <param name="portName">The port to use (for example, COM1).</param>
        /// <param name="baudRate">The baud rate.</param> 
        public BaseConnector(string portName, int baudRate)
        {
            driver = new SerialPort(portName, baudRate); 
        }
         
        /// <summary>
        /// Initializes a new instance of the SerialPort class using the specified port name, baud rate, parity, stop bits, data bits and handshake.
        /// </summary>
        /// <param name="portName">The port to use (for example, COM1).</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="parity">One of the Parity values.</param>
        /// <param name="stopBits">The stop bits value.</param>
        /// <param name="dataBits">The data bits value.</param>
        /// <param name="handShake">The hand shake value.</param>
        public BaseConnector(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handShake)
        {
            driver = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            driver.Handshake = handShake;
        }
          
        /// <summary>
        /// Opens a new connection.
        /// </summary>
        public virtual bool Open()
        { 
            if (!driver.IsOpen)
            {
                SerialPortFixer.Execute(driver.PortName); 
                driver.Open();  
            }
            return driver.IsOpen;
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        public void Close()
        {
            driver.Close();
        }

        /// <summary>
        /// Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the SerialPort object.
        /// </summary>
        /// <returns></returns>
        public virtual string ReadExisting()
        {
            return driver.ReadExisting();
        }

        /// <summary>
        /// Discards data from the serial driver's transmit buffer.
        /// </summary>
        public void DiscardBuffer()
        {
            try
            {
                driver.DiscardOutBuffer();
                driver.DiscardInBuffer();
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        /// Discards data from the serial driver's receive buffer.
        /// </summary>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual void DiscardInBuffer()
        {
            driver.DiscardInBuffer();
        }

        /// <summary>
        /// Discards data from the serial driver's transmit buffer.
        /// </summary>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual void DiscardOutBuffer()
        {
            driver.DiscardOutBuffer();
        }

        /// <summary>
        /// Writes a specified number of bytes to the streamt using data from a buffer.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port. </param>
        /// <param name="offset">The zero-based byte offset in the buffer parameter at which to begin copying bytes to the port. </param>
        /// <param name="count">The number of bytes to write. </param>
        /// <remarks>The number of bytes write.</remarks>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual void Write(byte[] buffer, int offset, int count)
        {
            driver.Write(buffer, offset, count);
        }

        /// <summary>
        /// Reads a number of bytes from the stream input buffer and writes those bytes into a byte array at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to. </param>
        /// <param name="offset">The offset in the buffer array to begin writing. </param>
        /// <param name="count">The number of bytes to read.    </param>
        /// <returns>The number of bytes read.</returns>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual int Read(byte[] buffer, int offset, int count)
        {
            return driver.Read(buffer, offset, count);
        }
         
        /// <summary>
        /// Send the command to serial port.
        /// </summary>
        public abstract string Execute(string cmd, bool receiveResponse = true, int delayWhenRead = 0);

        /// <summary>
        /// Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the stream object.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual string ReadLine()
        {
            return driver.ReadLine();
        }
         
        /// <summary>
        /// Gets the number of bytes of data in the receive buffer.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepperBoundary]
        public virtual int BytesToRead()
        {
            return driver.BytesToRead;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        internal void Dispose()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Dispose();
            }
        }
 
    }
}

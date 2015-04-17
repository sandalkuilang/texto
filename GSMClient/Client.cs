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
using GSMServerModel;
using System.Net.Sockets;
using Cryptography;
using System.Configuration;
using System.Dynamic; 

namespace GSMClient
{
    public class Client : IClient
    {
        private const string Format = "{0}</>{1}";

        private TcpClient tcpClient { get; set; }
        private Crypter crypter;
        private Encoding encode;
        private IParser<ParamArgs> parser;
        private IKeySym privateKey;
        public string Signature { get; set; }

        private string ip;
        private int port;

        public Client(string ip, int port, IKeySym privateKey)
        {
            this.ip = ip;
            this.port = port;
            if (privateKey == null)
            {
                throw new ArgumentNullException("privateKey");
            }
            this.privateKey = privateKey;
            tcpClient = new TcpClient();
            crypter = new Crypter(privateKey);
            encode = Encoding.GetEncoding("ibm850");
            parser = new CommandParser(); 
        }
         
        public void Send(string command)
        {
            string finalEncryption = CreateCommand(Signature, command, this.privateKey);
            byte[] dataToSend = encode.GetBytes(finalEncryption);
            tcpClient.Client.Send(dataToSend);
        }

        public static string CreateCommand(string signature, string input, IKeySym privateKey)
        {
            IParser<ParamArgs> parser = new CommandParser(); 
            ParamArgs output = parser.Parse(input);
            if (output.Count == 0) throw new OperationCanceledException("Could not parse command.");

            Header header = new Header(signature, "", output.Get<string>("Command"));
            string jsonHeader = Newtonsoft.Json.JsonConvert.SerializeObject(header);

            DynamicData detail = new DynamicData();
            foreach (string key in output.ToDictionary().Keys)
            {
                if (!key.Equals("Command"))
                {
                    detail.Add(key, output.Get<object>(key));
                }
            }
            string jsonDetail = Newtonsoft.Json.JsonConvert.SerializeObject(detail.GetDictionary());
            Crypter crypter = new Crypter(privateKey);

            string final = string.Format(Format, jsonHeader, jsonDetail);
            return crypter.Encrypt(final); 
        }
         
        public void Close()
        {
            tcpClient.Client.Close();
            tcpClient.Close();
        }
         
        public void Open()
        {
            tcpClient.Connect(ip, port); 
        }

        public TcpClient GetTcpClient()
        {
            return tcpClient;
        }

    }
}

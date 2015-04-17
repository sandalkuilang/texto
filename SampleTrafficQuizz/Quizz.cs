/*
    Sample Quizz SMS
 
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
using System.Text.RegularExpressions; 
using System.Configuration;
using GSMCommunication;
using GSMCommunication.Feature; 

namespace SampleTrafficQuizz
{
    public class Quizz
    {

        private IEnumerable<QuizModel> keywords;
        private GSMClient.IClient smsSender;
        private const string SMSCommandFormat = "ISMS.Send(+param: Number={0}, +param: Message={1})";
        private string ipStr, portStr;

        public void Main()
        {
            using (IQuery con = new DapperDbContext(DatabaseConst.SMSGateway))
            {
                keywords = con.Query<QuizModel>("GetKeyword");
            }
            ipStr = ConfigurationManager.AppSettings["SMSGWIPServer"];
            portStr = ConfigurationManager.AppSettings["SMSGWPortServer"];;
        }

        //public void OnDataSent(string data)
        //{
            
        //}
        
        public void OnDataReceived(string data)
        {
            List<BaseResult<SMSReadResult>> list = JsonHelper.JsonDeserialize<List<BaseResult<SMSReadResult>>>(data);
            if (list.Count > 0)
            {
                foreach (BaseResult<SMSReadResult> read in list)
                {
                    if (read.Response.TypeName.Contains("SMSRead"))
                    { 
                        if (keywords.Any())
                        {
                            foreach (QuizModel keyword in keywords)
                            {
                                Match match = new Regex(@"(.*[a-zA-Z]) (.*[a-zA-Z])").Match(keyword.Keyword); 
                                if (match.Success)
                                {
                                    if (read.Response.Message.ToLower().Contains(match.Groups[1].Value.ToLower()))
                                    {
                                        string[] arg = read.Response.Message.ToLower().Split(new string[] { match.Groups[1].Value.ToLower() }, StringSplitOptions.RemoveEmptyEntries);
                                        using (IQuery con = new DapperDbContext(DatabaseConst.SMSGateway))
                                        {
                                            List<Answer> result = con.Query<Answer>(keyword.Response, new { KeyID = arg[0].Trim() });
                                            if (result.Any())
                                            {
                                                smsSender = new GSMClient.Client(ipStr, Convert.ToInt32(portStr));
                                                smsSender.Open();
                                                smsSender.Send(string.Format(GSMClient.Command.CommandCollection.SMSSend, read.Response.From, result.SingleOrDefault().Description));
                                                smsSender.Close();
                                            }
                                        }
                                        break;
                                    }
                                }  
                            }  
                        } 
                    }
                } 
            }
        }
    }

    internal class DatabaseConst
    {
        public const string SMSGateway = "SMSGW";
    }
}

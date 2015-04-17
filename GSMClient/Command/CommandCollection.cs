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
using System.Reflection;
using System.Text;

namespace GSMClient.Command
{
    public class CommandCollection : Collection
    {
        public const string SMSSend = "ISMS.Send(+param: number={0} +param: message={1})";
        public const string SMSRead = "ISMS.Read()";
        public const string SMSReadAll = "ISMS.ReadAll()";
        public const string SMSDelete = "ISMS.Delete(+param: index={0})";
        public const string SMSDeleteAll = "ISMS.DeleteAll()";

        public const string CallDial = "ICall.Dial(+param: number={0})";
        public const string CallHang = "ICall.Hang(+param: number={0})";
        public const string CallAnswer = "ICall.Answer()";
        public const string CallSendUSSD = "ICall.SendUSSD(+param: code=1 +param: number={0})";

        public const string PhoneBookFindPhoneBook = "IPhoneBook.FindPhoneBook(+param: memory={0} +param: name={1})";
        public const string PhoneBookReadPhoneBook = "IPhoneBook.ReadPhoneBook(+param: memory={0} +param: fromIndex={1} +param: toIndex={2})";
        public const string PhoneBookGetInfo = "IPhoneBook.GetInfo()";
        public const string PhoneBookSetPhoneBookMemory = "IPhoneBook.SetPhoneBookMemory(+param: memory={0})";
        public const string PhoneBookWritePhoneBook = "IPhoneBook.WritePhoneBook(+param: memory={0} +param: index={1} +param: name={2} +param; phoneNumber={3})";

        public const string GeneralSetFunctionality = "IGeneral.SetFunctionality(+param: level={0})";
        public const string GeneralGetRegistrationStatus = "IGeneral.GetRegistrationStatus()";
        public const string GeneralGetSignalQuality = "IGeneral.GetSignalQuality()";
        public const string GeneralSetErrorMessageFormat = "IGeneral.SetErrorMessageFormat(+param: format={0})";
        public const string GeneralGetPossibleCharacterSet = "IGeneral.GetPossibleCharacterSet()";
        public const string GeneralGetCharacterSet = "IGeneral.GetCharacterSet()";
        public const string GeneralSetCharacterSet = "IGeneral.SetCharacterSet(+param: characterSet={0})";
        public const string GeneralGetErrorMessageFormat = "IGeneral.GetErrorMessageFormat()";
        public const string GeneralGetManufacturer = "IGeneral.GetManufacturer()";
        public const string GeneralGetServiceCenter = "IGeneral.GetServiceCenter()";
        public const string GeneralGetSoftwareVersion = "IGeneral.GetSoftwareVersion()";
        public const string GeneralGetModelInformation = "IGeneral.GetModelInformation()";
        public const string GeneralGetIMSI = "IGeneral.GetIMSI()";
        public const string GeneralGetOperator = "IGeneral.GetOperator()";
        public const string GeneralGetActivityStatus = "IGeneral.GetActivityStatus()";
        public const string GeneralGetSerialNumber = "IGeneral.GetSerialNumber()";
        public const string GeneralGetHardwareVersion = "IGeneral.GetHardwareVersion()";

        public const string ConfigGetRawXml = "IConfiguration.GetRawXML(+param: section={0})";
        public const string ConfigSetRawXml = "IConfiguration.SetRawXML(+param: section={0} +param: raw={1})";
        public const string ConfigSave = "IConfiguration.Save()";

        public const string ServerGetRegisteredPlugins = "IServer.GetRegisteredPlugins()";
        public const string ServerGetAvailableConnections = "IServer.GetAvailableConnections()";

        #region Initialize
        private static volatile CommandCollection instance;

        private static object syncRoot = new Object();

        private CommandCollection() 
        { 
            Type typeCommand = GetType();
            System.Reflection.FieldInfo[] commands = typeCommand.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Static);
            foreach (FieldInfo field in commands)
            {
                instance.Add(field.Name, field.GetValue(field).ToString());
            }
        }

        public static CommandCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CommandCollection();
                    }
                }
                return instance;
            }
        }         
        #endregion
         
    }
}

/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
namespace GSMServer
{
    public enum DeviceStatus
    {
        SlotFull,
        DeviceNotReady
    }

    internal class PluginRoutine
    {
        public const string Main = "Main";
        public const string OnDataSent = "OnDataSent";
        public const string OnDataReceived = "OnDataReceived";
        public const string OnDisposed = "OnDisposed";
    }

    internal class DatabaseNames
    {
        public const string SMSGW = "SMSGW";
    }
}

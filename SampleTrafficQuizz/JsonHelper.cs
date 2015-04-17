﻿/*
    Sample Quizz SMS
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SampleTrafficQuizz
{
    public class JsonHelper  
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static string JsonSerializer(object t)
        { 
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static T JsonDeserialize<T>(string jsonString)
        { 
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}

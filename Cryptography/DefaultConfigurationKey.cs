/*
    Cryptography
 
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

namespace Cryptography
{
    public class DefaultConfigurationKey : IKeySym
    { 
        public byte[] GetKey()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["PublicKey"];
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("App setting \'PublicKey\' cannot be null.");
            else
            {
                string[] KeySym = value.Split(',');
                byte[] publicKey = new byte[KeySym.Length];
                for (int i = 0; i < KeySym.Length; i++)
                {
                    publicKey[i] = Convert.ToByte(KeySym[i]);
                }
                return publicKey;
            }
        }
    }
}

/*
    Crypto
 
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
using System.IO;
using System.Reflection;

namespace Crypto
{
    public class Crypter
    {
        private IKeySym privateKey;

        public Crypter(IKeySym keySym)
        {
            if (keySym == null)
                throw new ArgumentNullException("\'KeySym\' cannot be null.");
            privateKey = keySym;
        }

        public string Decrypt(string data)
        {
            byte[] base64StringData; 
            base64StringData = Convert.FromBase64String(data);

            byte[] resultInByte = new byte[] { };

            SymmetricEncryption sym = new SymmetricEncryption(new System.Security.Cryptography.RijndaelManaged());
            sym.Key = privateKey.GetKey();
            resultInByte = sym.Decrypt(base64StringData);
            sym.Dispose();
            return ASCIIEncoding.ASCII.GetString(resultInByte); 
        }

        public string Encrypt(string data)
        {
            string result = string.Empty;
            using (SymmetricEncryption sym = new SymmetricEncryption(new System.Security.Cryptography.RijndaelManaged()))
            {
                sym.Key = privateKey.GetKey();
                result = Convert.ToBase64String(sym.Encrypt(ASCIIEncoding.ASCII.GetBytes(data)));
            }
            return result;
        }
         
    }
}

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
using System.Security;
using System.Security.Cryptography;
using System.IO;
using Crypto;

namespace Crypto
{
    [System.Diagnostics.DebuggerNonUserCode]
    public class SymmetricEncryption : IEncryption, IDisposable
    {
        private readonly SymmetricAlgorithm algorithm;
        private byte[] key; 

        private readonly Encoding encoding;
        DeriveBytes saltGenerator;

        public virtual byte[] Key
        {
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                byte[] reserveKey = encoding.GetBytes(new string(value.ToString().ToCharArray().Reverse().ToArray()));
                saltGenerator = new System.Security.Cryptography.Rfc2898DeriveBytes(value, reserveKey, 4);
                key = saltGenerator.GetBytes(32);
                algorithm.Key = key;
                algorithm.IV = saltGenerator.GetBytes(16);
            }
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return key;
            }
        }

        public virtual byte[] IV
        {
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                algorithm.IV = value;
            }
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return algorithm.IV;
            }
        }

        [SecuritySafeCritical]
        public SymmetricEncryption(SymmetricAlgorithm algorithm)
        {
            encoding = Encoding.GetEncoding("ibm850");
            this.algorithm = algorithm;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] Encrypt(byte[] data)
        {
            byte[] result = null;
            byte[] buffer = data;
            using (MemoryStream bufferMemory = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(bufferMemory, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                    cs.FlushFinalBlock();
                }
                result = bufferMemory.ToArray();
            }
            return result;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] Decrypt(byte[] data)
        { 
            byte[] result = new byte[data.Length];
            int DecryptedCount;
            using (MemoryStream bufferMemory = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(bufferMemory, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    DecryptedCount = cs.Read(data, 0, data.Length);
                    result = new byte[DecryptedCount];
                    bufferMemory.Position = 0;
                    bufferMemory.Read(result, 0, result.Length);
                }
            }
            return result;
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
                if (saltGenerator != null)
                {
                    saltGenerator.Dispose(); 
                }
                if (algorithm != null)
                {
                    algorithm.Dispose();
                }
            }
             
        }
    }
}

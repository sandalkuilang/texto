using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSMCommunication.PDUDecoder;

namespace GSMCommunication.PDUDecoder
{
    public abstract class SmsPdu : ITimestamp
    {
        private byte DCS;
        private const byte maxOctets = 140;
        private const int maxSeptets = 160;
        public const int MaxTextLength = 160;
        public const int MaxUnicodeTextLength = 70;
        private byte PID;
        private string smscAddress;
        private byte smscTOA;
        private byte[] userData;
        private byte userDataLength;

        private int constructLength;

        /// <summary>
        /// Gets the number of characters that have been actually been used for decoding upon construction.
        /// </summary>
        public int ConstructLength
        {
            get
            {
                return this.constructLength;
            }
            protected set
            {
                this.constructLength = value;
            }
        }

        /// <summary>
        /// Gets or sets if a user data header is present.
        /// </summary>
        public abstract bool UserDataHeaderPresent
        {
            get;
            set;
        }

        /// <summary>
        /// Extracts the user data header out of the user data.
        /// </summary>
        /// <returns>A byte array containing the extracted header.</returns>
        /// <exception cref="T:System.InvalidOperationException">There is no user data header is present in this message.</exception>
        /// <remarks>Use <see cref="P:GSMCommunication.PDUDecoder.SmsPdu.UserDataHeaderPresent" /> to determine whether a user data header is present.</remarks>
        public byte[] GetUserDataHeader()
        {
            if (!this.UserDataHeaderPresent)
            {
                throw new InvalidOperationException("There is no user data header is present in this message.");
            }
            else
            {
                byte num = this.userData[0];
                num = (byte)(num + 1);
                byte[] numArray = new byte[num];
                Array.Copy(this.userData, numArray, num);
                return numArray;
            }
        }

        /// <summary>
        /// Extracts the section of the user data that is not occupied by the user data header.
        /// </summary>
        /// <returns>A byte array containing the extracted data.</returns>
        /// <exception cref="T:System.InvalidOperationException">There is no user data header is present in this message.</exception>
        /// <remarks>Use <see cref="P:GSMCommunication.PDUDecoder.SmsPdu.UserDataHeaderPresent" /> to determine whether a user data header is present.</remarks>
        public byte[] GetUserDataWithoutHeader()
        {
            if (!this.UserDataHeaderPresent)
            {
                throw new InvalidOperationException("There is no user data header is present in this message.");
            }
            else
            {
                byte num = this.userData[0];
                num = (byte)(num + 1);
                int length = (int)this.userData.Length - num;
                byte[] numArray = new byte[length];
                Array.Copy(this.userData, num, numArray, 0, length);
                return numArray;
            }
        }

        /// <summary>
        /// Adds a user data header to an existing user data.
        /// </summary>
        /// <param name="header">The user data header to add.</param>
        /// <exception cref="T:System.InvalidOperationException">There is already a user data header present in this message.</exception>
        /// <exception cref="T:System.ArgumentException">The resulting total of user data header and existing user data exceeds the allowed
        /// maximum data length.</exception>
        /// <remarks>
        /// <para>The user data must already be set before adding a user data header.</para>
        /// <para>Adding a user data header reduces the available space for the remaining user data. If the resulting total of
        /// user data header and existing user data exceeds allowed maximum data length, an exception is raised.</para>
        /// </remarks>
        public void AddUserDataHeader(byte[] header)
        {
            int num;
            if (this.UserDataHeaderPresent)
            {
                throw new InvalidOperationException("There is already a user data header present in this message.");
            }
            else
            {
                int length = (int)header.Length;
                byte num1 = (byte)((double)length * 8 / 7);
                DataCodingScheme dataCodingScheme = GSMCommunication.PDUDecoder.DataCodingScheme.Decode(this.DCS);
                if (dataCodingScheme.Alphabet != 0)
                {
                    if (dataCodingScheme.Alphabet == 1 || dataCodingScheme.Alphabet == 2)
                    {
                        num = length + this.userDataLength;
                    }
                    else
                    {
                        num = num1 + this.userDataLength;
                    }
                }
                else
                {
                    num = num1 + this.userDataLength;
                }
                byte[] numArray = new byte[(int)header.Length + (int)this.userData.Length];
                header.CopyTo(numArray, 0);
                this.userData.CopyTo(numArray, (int)header.Length);
                this.SetUserData(numArray, (byte)num);
                this.UserDataHeaderPresent = true;
                return;
            }
        }

        /// <summary>
        /// Extracts the section of the user data that is not occupied by the user data header
        /// and returns it as text.
        /// </summary>
        /// <returns>A string containing the extracted text.</returns>
        /// <exception cref="T:System.InvalidOperationException">There is no user data header is present in this message.</exception>
        /// <remarks>Use <see cref="P:GSMCommunication.PDUDecoder.SmsPdu.UserDataHeaderPresent" /> to determine whether a user data header is present.</remarks>
        public string GetUserDataTextWithoutHeader()
        {
            byte[] userDataWithoutHeader = this.GetUserDataWithoutHeader();
            return PduParts.DecodeText(userDataWithoutHeader, this.DCS);
        }

        protected SmsPdu()
        {
            this.SmscAddress = string.Empty;
            this.PID = 0;
            this.DCS = 0;
            this.userDataLength = 0;
            this.userData = null;
        }

        protected string CreateAddressOfType(string address, byte type)
        {
            if (address != string.Empty)
            {
                if ((type == 0x91) && !address.StartsWith("+"))
                {
                    return ("+" + address);
                }
                AddressType type2 = new AddressType(type);
                if (type2.Ton == 5)
                {
                    string s = BcdWorker.EncodeSemiOctets(address);
                    return TextDataConverter.SevenBitToString(TextDataConverter.OctetsToSeptetsStr(BcdWorker.GetBytes(s, 0, BcdWorker.CountBytes(s))), false);
                }
            }
            return address;
        }

        protected string Decode7BitText()
        {
            string s = TextDataConverter.OctetsToSeptetsStr(this.userData);
            int length = s.Length;
            return TextDataConverter.SevenBitToString(s, true);
        }

        protected static void DecodeGeneralAddress(string pdu, ref int index, out string address, out byte addressType)
        {
            byte @byte = BcdWorker.GetByte(pdu, index++);
            addressType = BcdWorker.GetByte(pdu, index++);
            if (@byte > 0)
            {
                bool flag = false;
                int length = (((@byte % 2) != 0) ? (@byte + 1) : @byte) / 2;
                if (((index * 2) + (length * 2)) > (pdu.Length - (index * 2)))
                {
                    length = (pdu.Length - (index * 2)) / 2;
                    flag = true;
                }
                string data = BcdWorker.GetBytesString(pdu, index, length);
                index += length;
                if (!flag)
                {
                    address = BcdWorker.DecodeSemiOctets(data).Substring(0, @byte);
                }
                else
                {
                    address = BcdWorker.DecodeSemiOctets(data).Substring(0, length * 2);
                }
            }
            else
            {
                address = string.Empty;
            }
        }

        protected static void DecodeSmscAddress(string pdu, ref int index, out string address, out byte addressType)
        {
            byte @byte = BcdWorker.GetByte(pdu, index++);
            if (@byte > 0)
            {
                byte num2 = BcdWorker.GetByte(pdu, index++);
                int length = @byte - 1;
                string data = BcdWorker.GetBytesString(pdu, index, length);
                index += length;
                string str2 = BcdWorker.DecodeSemiOctets(data);
                if (str2.EndsWith("F") || str2.EndsWith("f"))
                {
                    str2 = str2.Substring(0, str2.Length - 1);
                }
                addressType = num2;
                address = str2;
            }
            else
            {
                addressType = 0;
                address = string.Empty;
            }
        }

        protected string DecodeUcs2Text()
        {
            return Encoding.BigEndianUnicode.GetString(this.userData);
        }

        protected static void DecodeUserData(string pdu, ref int index, out byte userDataLength, out byte[] userData)
        {
            byte @byte = BcdWorker.GetByte(pdu, index++);
            if (@byte <= 0)
            {
                userDataLength = 0;
                userData = null;
            }
            else
            {
                int length = BcdWorker.CountBytes(pdu) - index;
                string s = BcdWorker.GetBytesString(pdu, index, length);
                index += length;
                string str2 = string.Empty;
                for (int i = 0; i < (s.Length / 2); i++)
                {
                    string byteString = BcdWorker.GetByteString(s, i);
                    if (!Calc.IsHexString(byteString))
                    {
                        break;
                    }
                    str2 = str2 + byteString;
                }
                userDataLength = @byte;
                userData = Calc.HexToInt(str2);
            }
        }

        protected void Encode7BitText(string text)
        {
            string data = TextDataConverter.StringTo7Bit(text);
            int length = data.Length;
            if (length > 160)
            {
                string[] strArray = new string[] { "Text is too long. A maximum of ", 160.ToString(), " resulting septets is allowed. The current input results in ", length.ToString(), " septets." };
                throw new ArgumentException(string.Concat(strArray));
            }
            this.SetUserData(TextDataConverter.SeptetsToOctetsInt(data), (byte)length);
        }

        protected void EncodeUcs2Text(string text)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(text);
            int length = bytes.Length;
            if (length > 140)
            {
                string[] strArray = new string[] { "Text is too long. A maximum of ", ((byte)140).ToString(), " resulting octets is allowed. The current input results in ", length.ToString(), " octets." };
                throw new ArgumentException(string.Concat(strArray));
            }
            this.SetUserData(bytes, (byte)length);
        }

        protected void FindTypeOfAddress(string address, out byte type, out string useThisAddress)
        {
            if (address != string.Empty)
            {
                byte num = 0x81;
                while (address.StartsWith("+"))
                {
                    num = 0x91;
                    address = address.Substring(1);
                }
                useThisAddress = address;
                type = num;
            }
            else
            {
                useThisAddress = address;
                type = 0;
            }
        }

        public static string GetSafeText(string data)
        {
            bool flag;
            bool flag2;
            return GetSafeText(data, out flag, out flag2);
        }

        public static string GetSafeText(string data, out bool lengthCorrected, out bool charsCorrected)
        {
            string str;
            string str2;
            lengthCorrected = false;
            charsCorrected = false;
            if (data.Length > 160)
            {
                str = data.Substring(0, 160);
                lengthCorrected = true;
            }
            else
            {
                str = data;
            }
            do
            {
                bool flag;
                str2 = TextDataConverter.StringTo7Bit(str, false, out flag);
                if (flag)
                {
                    charsCorrected = true;
                }
                if (str2.Length > 160)
                {
                    str = data.Substring(0, str.Length - 1);
                    lengthCorrected = true;
                }
            }
            while (str2.Length > 160);
            return TextDataConverter.SevenBitToString(str2);
        }

        public void GetSmscAddress(out string address, out byte addressType)
        {
            address = this.smscAddress;
            addressType = this.smscTOA;
        }

        public static int GetTextLength(string text)
        {
            return TextDataConverter.StringTo7Bit(text).Length;
        }

        public abstract SmsTimestamp GetTimestamp();
        public void SetSmscAddress(string address, byte addressType)
        {
            this.smscAddress = address;
            this.smscTOA = addressType;
        }

        public void SetUserData(byte[] data, byte dataLength)
        {
            if (data.Length > 140)
            {
                string[] strArray = new string[] { "User data is too long. A maximum of ", ((byte)140).ToString(), " octets is allowed. ", data.Length.ToString(), " octets were passed." };
                throw new ArgumentException(string.Concat(strArray));
            }
            this.userData = data;
            this.userDataLength = dataLength;
        }

        public override string ToString()
        {
            return this.ToString(false);
        }

        public abstract string ToString(bool excludeSmscData);

        public int ActualLength
        {
            get
            {
                return (this.ToString(true).Length / 2);
            }
        }

        public byte DataCodingScheme
        {
            get
            {
                return this.DCS;
            }
            set
            {
                this.DCS = value;
            }
        }

        public byte ProtocolID
        {
            get
            {
                return this.PID;
            }
            set
            {
                this.PID = value;
            }
        }

        public string SmscAddress
        {
            get
            {
                return this.CreateAddressOfType(this.smscAddress, this.smscTOA);
            }
            set
            {
                byte num;
                string str;
                this.FindTypeOfAddress(value, out num, out str);
                this.smscAddress = str;
                this.smscTOA = num;
            }
        }

        public byte SmscAddressType
        {
            get
            {
                return this.smscTOA;
            }
        }

        public int TotalLength
        {
            get
            {
                return (this.ToString().Length / 2);
            }
        }

        public byte[] UserData
        {
            get
            {
                return this.userData;
            }
        }

        public byte UserDataLength
        {
            get
            {
                return this.userDataLength;
            }
        }

        public string UserDataText
        {
            get
            {
                byte alphabet;

                DataCodingScheme scheme = new GeneralDataCoding(0);
                if (scheme is GeneralDataCoding)
                {
                    GeneralDataCoding coding = (GeneralDataCoding)scheme;
                    alphabet = coding.Alphabet;
                }
                else
                {
                    alphabet = 0;
                }
                if (alphabet == 2)
                {
                    return this.DecodeUcs2Text();
                }
                return this.Decode7BitText();
            }
            set
            {
                byte alphabet;
                DataCodingScheme scheme = new GeneralDataCoding(0);
                if (scheme is GeneralDataCoding)
                {
                    GeneralDataCoding coding = (GeneralDataCoding)scheme;
                    alphabet = coding.Alphabet;
                }
                else
                {
                    alphabet = 0;
                }
                if (alphabet == 2)
                {
                    this.EncodeUcs2Text(value);
                }
                else
                {
                    this.Encode7BitText(value);
                }
            }
        }
    }
}

namespace GSMCommunication.PDUDecoder
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;
    using GSMCommunication.PDUDecoder;

    public class TextDataConverter
    {
        private static char[] sevenBitDefault = new char[] { '@', '\x00a3', '$', '\x00a5', '\x00e8', '\x00e9', '\x00f9', '\x00ec', '\x00f2', '\x00c7', '\n', '\x00d8', '\x00f8', '\r', '\x00c5', '\x00e5',
        'Δ', '_', 'Φ', 'Γ', 'Λ', 'Ω', 'Π', 'Ψ', 'Σ', 'Θ', 'Ξ', '\x001b', '\x00c6', '\x00e6', '\x00df', '\x00c9',
        ' ', '!', '"', '#', '\x00a4', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
        '\x00a1', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '\x00c4', '\x00d6', '\x00d1', '\x00dc', '\x00a7',
        '\x00bf', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '\x00e4', '\x00f6', '\x00f1', '\x00fc', '\x00e0' };

        private TextDataConverter()
        {
        }

        private static byte CharToSevenBitExtension(char c)
        {
            switch (c)
            {
                case '[':
                    return 60;

                case '\\':
                    return 0x2f;

                case ']':
                    return 0x3e;

                case '^':
                    return 20;

                case '\f':
                    return 10;

                case '{':
                    return 40;

                case '|':
                    return 0x40;

                case '}':
                    return 0x29;

                case '~':
                    return 0x3d;

                case '€':
                    return 0x65;
            }
            throw new ArgumentException("The character '" + c.ToString() + "' does not exist in the GSM 7-bit default alphabet extension table.");
        }

        public static string OctetsToSeptetsStr(byte[] data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            string str = string.Empty;
            string s = string.Empty;
            int index = 0;
            while (index < data.Length)
            {
                string str3 = Calc.IntToBin(data[index], 8);
                string str4 = string.Empty;
                string str5 = str3.Substring((index % 7) + 1, 7 - (index % 7));
                if ((index != 0) && ((index % 7) != 0))
                {
                    str4 = str5 + s;
                }
                else
                {
                    if (index != 0)
                    {
                        str = str + ((char) Calc.BinToInt(s));
                    }
                    str4 = str5;
                }
                s = str3.Substring(0, (index % 7) + 1);
                str = str + ((char) Calc.BinToInt(str4));
                index++;
            }
            if (((index != 0) && ((index % 7) == 0)) && (s != "0000000"))
            {
                str = str + ((char) Calc.BinToInt(s));
            }
            return str;
        }

        public static string SeptetsToOctetsHex(string data)
        {
            string str = string.Empty;
            string s = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                string str3 = Calc.IntToBin((byte) data[i], 7);
                if ((i != 0) && ((i % 8) != 0))
                {
                    string str5 = str3.Substring(7 - (i % 8)) + s;
                    str = str + Calc.IntToHex(Calc.BinToInt(str5));
                }
                s = str3.Substring(0, 7 - (i % 8));
                if ((i == (data.Length - 1)) && (s != string.Empty))
                {
                    str = str + Calc.IntToHex(Calc.BinToInt(s));
                }
            }
            return str;
        }

        public static byte[] SeptetsToOctetsInt(string data)
        {
            ArrayList list = new ArrayList();
            string s = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                string str2 = Calc.IntToBin((byte) data[i], 7);
                if ((i != 0) && ((i % 8) != 0))
                {
                    string str4 = str2.Substring(7 - (i % 8)) + s;
                    list.Add(Calc.BinToInt(str4));
                }
                s = str2.Substring(0, 7 - (i % 8));
                if ((i == (data.Length - 1)) && (s != string.Empty))
                {
                    list.Add(Calc.BinToInt(s));
                }
            }
            byte[] array = new byte[list.Count];
            list.CopyTo(array);
            return array;
        }

        private static char SevenBitExtensionToChar(byte b)
        {
            switch (b)
            {
                case 40:
                    return '{';

                case 0x29:
                    return '}';

                case 20:
                    return '^';

                case 10:
                    return '\f';

                case 60:
                    return '[';

                case 0x3d:
                    return '~';

                case 0x3e:
                    return ']';

                case 0x40:
                    return '|';

                case 0x65:
                    return '€';

                case 0x2f:
                    return '\\';
            }
            throw new ArgumentException("The value " + b.ToString() + " is not part of the 7-bit default alphabet extension table.");
        }

        public static string SevenBitToString(string s)
        {
            return SevenBitToString(s, true);
        }

        public static string SevenBitToString(string s, bool throwExceptions)
        {
            string str = string.Empty;
            bool flag = false;
            for (int i = 0; i < s.Length; i++)
            {
                byte b = (byte) s[i];
                if (flag)
                {
                    try
                    {
                        str = str + SevenBitExtensionToChar(b);
                    }
                    catch (Exception)
                    {
                        if (throwExceptions)
                        {
                            throw;
                        }
                        str = str + '?';
                    }
                    flag = false;
                }
                else
                    if (b != 0x1b)
                    {
                        if (b <= sevenBitDefault.GetUpperBound(0))
                        {
                            str = str + sevenBitDefault[b];
                        }
                        else
                        {
                            if (throwExceptions)
                            {
                                object[] objArray = new object[] { "Character '", (char) b, "' at position ", (i + 1).ToString(), " is not part of the GSM 7-bit default alphabet." };
                                throw new ArgumentException(string.Concat(objArray));
                            }
                            str = str + '?';
                        }
                    }
                    else
                    {
                        flag = true;
                    }
            }
            return str;
        }

        public static string StringTo7Bit(string s)
        {
            bool flag;
            return StringTo7Bit(s, true, out flag);
        }

        public static string StringTo7Bit(string s, bool throwExceptions, out bool charsReplaced)
        {
            StringBuilder builder = new StringBuilder();
            charsReplaced = false;
            bool flag = false;
            char c = ' ';
            int startIndex = 0;
            int index = 0;
            for (startIndex = 0; startIndex < s.Length; startIndex++)
            {
                flag = false;
                c = s.Substring(startIndex, 1)[0];
                for (index = 0; index <= sevenBitDefault.GetUpperBound(0); index++)
                {
                    if (sevenBitDefault[index] == c)
                    {
                        builder.Append((char) index);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    try
                    {
                        byte num3 = CharToSevenBitExtension(c);
                        builder.Append('\x001b');
                        builder.Append((char) num3);
                        flag = true;
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!flag)
                {
                    if (throwExceptions)
                    {
                        object[] objArray = new object[] { "The character '", c, "' at position ", (startIndex + 1).ToString(), " does not exist in the GSM 7-bit default alphabet." };
                        throw new ArgumentException(string.Concat(objArray));
                    }
                    builder.Append('?');
                    charsReplaced = true;
                }
            }
            return builder.ToString();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts.InternalLogging
{
    public class BaseRandomizeGenerator
    {
        public static string Generate(string PatternLetter, string PatterNumber, byte Length)
        {
            double random1 = 1.0;
            int rp;
            int stoprp;
            Int32 arrIndex;
            StringBuilder sb = new StringBuilder();
            string randomLetter;
            Random rnd = new Random();
            char[] Key_Letters;
            char[] Key_Numbers;

            Key_Letters = PatternLetter.ToCharArray();
            Key_Numbers = PatterNumber.ToCharArray();
            
            for (int i = 0; i <= (Length - 1); i++)
            {
                stoprp = 0;
                rp = rnd.Next(1, 10);
                while (stoprp < rp)
                {
                    random1 = rnd.NextDouble();
                    stoprp += 1;
                }
                
                arrIndex = -1;
                if ((int)(random1 * 111) % 2 == 0)
                {
                    do
                    {
                        arrIndex = Convert.ToInt16(Key_Letters.GetUpperBound(0) * random1);
                    } while (arrIndex < 0);
                    randomLetter = Key_Letters[arrIndex].ToString();
                    if ((int)((arrIndex * random1) * 99) % 2 != 0)
                    {
                        randomLetter = Key_Letters[arrIndex].ToString();
                        randomLetter = randomLetter.ToUpper();
                    }
                    sb.Append(randomLetter);
                }
                else
                {
                    do
                    {
                        arrIndex = Convert.ToInt16(Key_Numbers.GetUpperBound(0) * random1);
                    } while (arrIndex < 0);
                    sb.Append(Key_Numbers[arrIndex]);
                }
            }
            return sb.ToString();
        }
    }
}

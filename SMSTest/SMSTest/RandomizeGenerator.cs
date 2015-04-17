using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSTest
{
    public class RandomizeGenerator
    {
        private const string LetterArray = "0123456789"; //"wecbufghajklysrqoptixvdmz";
        private const string NumberArray = "8942603157";

        /// <summary>
        /// Randomize character from a to z and 1 to 9
        /// </summary>
        /// <param name="Length">The length for result.</param> 
        public static string Generate(byte Length)
        {
            return Generate(LetterArray, NumberArray, Length);
        }
         
        public static string Generate(string PatternLetter, string PatterNumber, byte Length)
        {
            double random1;
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
                random1 = rnd.NextDouble();
                arrIndex = -1;
                if ((int)(random1 * 111) % 2 == 0)
                {
                    do
                    {
                        arrIndex = Convert.ToInt16(Key_Letters.GetUpperBound(0) * random1);
                    } while (arrIndex < 0);
                    randomLetter = Key_Letters[arrIndex].ToString().ToLower();
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

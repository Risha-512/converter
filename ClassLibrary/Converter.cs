using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Converter
    {
        static private int IntPow(int x, uint pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
        static public string Convert(string data, int origNumBase, int resNumBase)
        {
            var numDec = origNumBase != 10
                ? ConvertOtherBaseToDec(data, origNumBase)
                : origNumBase;

            var res = ConvertDecToOtherBase(numDec, resNumBase);

            Console.WriteLine("num(dec): ", numDec);
            Console.WriteLine("res: ", res);

            return res;
        }

        static private int ConvertOtherBaseToDec(string numString, int numBase)
        {
            int n = numString.Length;
            int decimalNumber = 0;
     
            for (int i = 0; i < n; i++)
                decimalNumber += (
                    numString[i] < 'A'
                        ? numString[i] - '0'
                        : numString[i] - 'A' + 10
                    ) * IntPow(numBase, (uint)(n - i - 1));

            return decimalNumber;
        }

        static private string ConvertDecToOtherBase(int num, int numBase)
        {
            string resultString = "";
            int result = num, remainder;

            while (result != 0)
            {
                remainder = result % numBase;

                resultString.Append<char>(remainder < 10 
                    ? remainder.ToString()[0]
                    : (remainder + 55).ToString()[0]);

                result /= numBase;  
            }

            return resultString;
        }
    }
}

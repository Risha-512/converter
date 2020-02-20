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
            if (origNumBase == resNumBase)
                return data;

            var numSplit = data.Split('.');

            var numDec = 0;
            var fractNumDec = 0.0d;

            if (numSplit[0] != "0")
            {
                numDec = origNumBase != 10
                    ? ConvertOtherBaseToDec(numSplit[0], origNumBase)
                    : Int32.Parse(numSplit[0]);
            }

            if (numSplit.Length == 2)
            {
                fractNumDec = origNumBase != 10
                    ? ConvertFractionalToDec(numSplit[1], origNumBase)
                    : Double.Parse("0," + numSplit[1]);
            }

            return (numDec == 0 ? "0" : ConvertDecToOtherBase(numDec, resNumBase)) +
                (fractNumDec == 0.0d ? "" : "." + ConvertFractionalToOtherBase(fractNumDec, resNumBase));
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

                resultString = resultString.Insert(0, remainder < 10 
                    ? remainder.ToString()
                    : ((char)(remainder + 55)).ToString());

                result /= numBase;  
            }

            return resultString;
        }

        static private double ConvertFractionalToDec(string numString, int numBase)
        {
            var n = numString.Length;
            double fractionalDecNum = 0.0d;

            for (int i = 0; i < n; i++)
            {
                fractionalDecNum += (numString[i] < 'A'
                        ? numString[i] - '0'
                        : numString[i] - 'A' + 10
                    ) * Math.Pow(numBase, -i - 1);
            }

            return fractionalDecNum;
        }

        static private string ConvertFractionalToOtherBase(double num, int numBase)
        {
            var resultString = "";

            for (int i = 0; i < 15 && num != 0; i++)
            {
                num *= numBase;
                var intPart = Math.Truncate(num);

                resultString += (char)(intPart < 10
                    ? intPart + '0'
                    : intPart + 'A' - 10);

                num -= intPart;
            }

            return resultString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Converter
    {
        static public string Convert(string data, int origNumBase, int resNumBase)
        {
            if (origNumBase == resNumBase)
                return data;

            var numSplit = data.Split('.');

            UInt64 numDec = 0;
            double fractNumDec = 0.0d;

            if (numSplit[0] != "0")
            {
                numDec = origNumBase != 10
                    ? ConvertOtherBaseToDec(numSplit[0], origNumBase)
                    : UInt64.Parse(numSplit[0]);
            }

            if (numSplit.Length == 2)
            {
                fractNumDec = origNumBase != 10
                    ? ConvertFractionalToDec(numSplit[1], origNumBase)
                    : Double.Parse("0," + numSplit[1]);
            }

            return (
                numDec == 0
                    ? "0"
                    : ConvertDecToOtherBase(numDec, resNumBase))
                + (fractNumDec == 0.0d
                    ? ""
                    : $".{ConvertFractionalToOtherBase(fractNumDec, resNumBase)}"
            );
        }

        static private UInt64 ConvertOtherBaseToDec(string numString, int numBase)
        {
            int n = numString.Length;
            UInt64 decimalNumber = 0;

            for (int i = 0; i < n; i++)
                decimalNumber += (UInt64)((
                    numString[i] < 'A'
                        ? numString[i] - '0'
                        : numString[i] - 'A' + 10
                    ) * Math.Pow(numBase, n - i - 1));

            return decimalNumber;
        }

        static private string ConvertDecToOtherBase(UInt64 num, int numBase)
        {
            string resultString = "";
            UInt64 result = num, remainder;

            while (result != 0)
            {
                remainder = result % (UInt64)numBase;

                resultString = resultString.Insert(0, remainder < 10
                    ? remainder.ToString()
                    : ((char)(remainder + 55)).ToString());

                result /= (UInt64)numBase;
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

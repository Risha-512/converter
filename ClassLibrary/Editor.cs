using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Editor
    {
        public string number { get; private set; }

        public int startIndex { get; private set; }

        public int symbolCount { get; private set; }

        const string delim = ".";

        const string zero = "0";

        public Editor()
        {
            number = "0";
            startIndex = 0;
            symbolCount = 0;
        }

        public void SetSelection(int start, int length)
        {
            startIndex = start;
            symbolCount = length;
        }

        public Editor AddDigit(string n, int origBase)
        {
            if (symbolCount != 0)
                RemoveSymbolsFromPosition();

            if (number == "0")
                number = n;
            else
            {
                number = number.Insert(startIndex, n);
                startIndex++;
            }

            return this;
        }

        public Editor AddZero()
        {
            if (symbolCount != 0)
                RemoveSymbolsFromPosition();

            if (startIndex == 0 && !number.Contains(delim))
            {
                number = number.Insert(0, zero + delim);
                startIndex = 2;
            }
            else if (number == "0")
            {
                number = number.Insert(startIndex, delim);
                startIndex = 2;
            }
            else if (startIndex != 0 && number != "0")
            {
                number = number.Insert(startIndex, zero);
                startIndex++;
            }

            return this;
        }

        public Editor AddDelim()
        {
            if (symbolCount != 0)
                RemoveSymbolsFromPosition();

            if (startIndex == 0 && !number.Contains(delim))
            {
                number = number.Insert(0, zero + delim);
                startIndex = 2;
            }
            else if (!number.Contains(delim))
            {
                number = number.Insert(startIndex, delim);
                startIndex++;
            }

            return this;
        }

        public Editor RemoveSymbolsFromPosition()
        {
            if (symbolCount != 0 && symbolCount == number.Length)
                return Clear();
            else if (symbolCount == 0 && number != "" && startIndex != 0)
            {
                number = number.Remove(startIndex - 1, 1);
                startIndex--;
            }
            else
                number = number.Remove(startIndex, symbolCount);

            if (number == "")
            {
                number += "0";
                startIndex = 1;
            }

            return this;
        }

        public Editor Clear()
        {
            number = zero;
            startIndex = 1;
            return this;
        }
    }
}

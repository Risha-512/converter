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

        const string delim = ".";

        const string zero = "0";

        public Editor()
        {
            number = "";
        }

        public Editor AddDigit(string n, int startIndex)
        {
            number = number.Insert(startIndex, n);
            return this;
        }

        public Editor AddZero(int startIndex)
        {
            return this.AddDigit(zero, startIndex);
        }

        public Editor AddDelim(int startIndex)
        {
            if (!number.Contains(delim))
                number = number.Insert(startIndex, delim);

            return this;
        }

        public Editor RemoveSymbolsFromPosition(int startIndex, int symbolCount = 1)
        {
            number.Remove(startIndex, symbolCount);
            return this;
        }

        public Editor Clear()
        {
            number = zero;
            return this;
        }
    }
}

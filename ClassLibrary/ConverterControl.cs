using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class ConverterControl
    {
        public int originalBase { get; set; }

        public int resultBase { get; set; }

        const int accuracy = 10;

        public History history = new History();

        public ConverterControl()
        {
            originalBase = 10;
            resultBase = 16;
        }

        public Editor editor = new Editor();
       
    }
}

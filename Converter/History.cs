using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Converter
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        static public void AddConvertData(string origNum, int origNumBase, string resNum, int resNumBase)
        {
            string path = Directory.GetCurrentDirectory() + "\\History.bin";
            BinaryWriter binWriter;

            if (!File.Exists(path))
                using (binWriter = new BinaryWriter(new FileStream(path, FileMode.Create), Encoding.ASCII)) { }

            using (binWriter = new BinaryWriter(File.Open(path, FileMode.Append)))
            {
                try
                {
                    binWriter.Write($"{origNum}({origNumBase}) -> {resNum}({resNumBase})\n");
                }
                catch { throw new Exception("History.bin: file isn't correct"); }
            }
        }
    }
}

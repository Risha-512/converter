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
        static private string path = Directory.GetCurrentDirectory() + "\\History.bin";

        public History()
        {
            InitializeComponent();
        }

        static public void AddConvertData(string origNum, int origNumBase, string resNum, int resNumBase)
        {
            BinaryWriter binWriter;

            using (binWriter = new BinaryWriter(File.Open(path, FileMode.Append)))
            {
                try
                {
                    binWriter.Write($"{origNum}({origNumBase}) -> {resNum}({resNumBase})\n");
                }
                catch { throw new Exception("History.bin: file isn't correct"); }
            }
        }

        private void History_Load(object sender, EventArgs e)
        {
            BinaryReader binReader;

            if (File.Exists(path))
            {
                string fileLine;
                using (binReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        for (int i = 1; binReader.BaseStream.Position != binReader.BaseStream.Length; i++)
                            historyList.Text += $"{i}. {binReader.ReadString()}";
                    }
                    catch { throw new Exception("History.bin: Can't read file"); }
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (historyList.Text != "")
            {
                historyList.Text = "";
                BinaryWriter binWriter;

                using (binWriter = new BinaryWriter(File.Open(path, FileMode.Truncate))) { }
            }
        }
    }
}

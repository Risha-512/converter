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
        static private string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Converter";

        public History()
        {
            InitializeComponent();
        }

        static public void CreateConverterDirectory()
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "\\History.bin";
        }

        static public void AddConvertData(string origNum, int origNumBase, string resNum, int resNumBase)
        {
            using (var binWriter = new BinaryWriter(File.Open(path, FileMode.Append)))
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
            if (File.Exists(path))
            {
                using (var binReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        var list = new List<string>();

                        for (int i = 1; binReader.BaseStream.Position != binReader.BaseStream.Length; i++)
                            historyList.Items.Add($"{i}. {binReader.ReadString()}");
                    }
                    catch { throw new Exception("History.bin: can't read file"); }
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (historyList.Items.Count > 0)
            {
                historyList.Dispose();

                using (var binWriter = new BinaryWriter(File.Open(path, FileMode.Truncate))) { }
            }
            Close();
        }
    }
}

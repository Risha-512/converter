using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class History
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Converter";
        public List<string> historyList { get; private set; }

        public History(string saveFileName = "History")
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += $"\\{saveFileName}.bin";

            this.ReadEntriesFromFile();
        }

        private void ReadEntriesFromFile()
        {
            historyList = new List<string>();

            if (File.Exists(filePath))
            {
                using (var binReader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        for (int i = 1; binReader.BaseStream.Position != binReader.BaseStream.Length; i++)
                            historyList.Add($"{i}. {binReader.ReadString()}");
                    }
                    catch { throw new Exception($"{filePath}: problem reading file"); }
                }
            }
        }

        private void WriteLastEntryToFile()
        {
            using (var binWriter = new BinaryWriter(File.Open(filePath, FileMode.Append)))
            {
                try { binWriter.Write(historyList.Last() + "\n"); }
                catch { throw new Exception($"{filePath}: problem writing to file"); }
            }
        }

        public void AddEntry(string entry)
        {
            historyList.Add(entry);
            WriteLastEntryToFile();
        }

        public string GetEntryAt(int index)
        {
            return historyList.ElementAt(index);
        }

        public int GetLength()
        {
            return historyList.Count;
        }

        public void Clear()
        {
            historyList.Clear();

            using (var binWriter = new BinaryWriter(File.Open(filePath, FileMode.Truncate))) { }
        }
    }
}

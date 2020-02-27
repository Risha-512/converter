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
        List<string> history;

        History(string saveFileName = "History")
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += $"{saveFileName}.txt";

            this.ReadEntriesFromFile();
        }

        private void ReadEntriesFromFile()
        {
            if (File.Exists(filePath))
                foreach (string entry in File.ReadLines(filePath))
                    this.AddEntry(entry);
        }

        private void WriteLastEntryToFile()
        {
            using (var binWriter = new StreamWriter(File.Open(filePath, FileMode.Append)))
            {
                try { binWriter.WriteLine(history.Last()); }
                catch { throw new Exception($"{filePath}: problem writing to file"); }
            }
        }

        public void AddEntry(string entry)
        {
            history.Add(entry);
            WriteLastEntryToFile();
        }

        public string GetEntryAt(int index)
        {
            return history.ElementAt(index);
        }

        public int GetLength()
        {
            return history.Count;
        }

        public void Clear()
        {
            history.Clear();
        }
    }
}

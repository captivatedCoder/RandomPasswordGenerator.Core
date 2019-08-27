using System.Collections.Generic;
using System.IO;

namespace PasswordGenerator.Tools
{
    internal class CsvReader
    {
        private readonly string _fileName;
        private readonly char _separator;
        public List<string> CsvList { get; private set; }

        public CsvReader(string fileName, char separator)
        {
            _fileName = fileName;
            _separator = separator;

            ReadPasswordFile();
        }

        public void ReadPasswordFile()
        {
            var csvList = new List<string>();

            using (var csvReader = new StreamReader(File.OpenRead(_fileName)))
            {
                while (!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    if (line == null) continue;
                    var words = line.Split(_separator);
                    csvList.AddRange(words);
                }
            }

            CsvList = csvList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;

namespace PasswordGenerator.Diceware
{
    public class DicewareFileReader
    {
        public List<DicewareFileData> FileData = new List<DicewareFileData>();
        private string FilePath { get; set; }

        public DicewareFileReader(string filePath)
        {
            FilePath = filePath;
        }

        public void WordList()
        {
            var wordsList = File.ReadAllLines(FilePath);    
            foreach (var line in wordsList)
            {
                var fileData = new DicewareFileData();
                var tmpLine = line.Split('\t');

                fileData.IdNumber = tmpLine[0];
                fileData.Word = tmpLine[1];

                FileData.Add(fileData);
            }
        }
    }
}
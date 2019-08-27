using System.IO;
using System.Text;

namespace PasswordGenerator.Data
{
    public static class SpecialCharacters
    {
        public static string SpecialCharactersList => ReadSpecialCharactersFile();

        private static string ReadSpecialCharactersFile()
        {
            var specialCharacters = new StringBuilder();

            using(var csvReader = new StreamReader(File.OpenRead("specialcharacters.csv")))
            {
                while(!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    if(line == null) continue;
                    var words = line.Split(',');

                    foreach (var word in words)
                    {
                        specialCharacters.Append(word);
                    }
                }
            }

            return specialCharacters.ToString();
        }
    }
}
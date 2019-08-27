using System.Collections.Generic;
using System.IO;

namespace PasswordGenerator.Data
{
    public class PasswordDictionaries
    {
        public static List<string> PasswordList => ReadPasswordFile();

        private static List<string> ReadPasswordFile()
        {
            var passwordList = new List<string>();

            using(var csvReader = new StreamReader(File.OpenRead("passwordlist.csv")))
            {
                while(!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    if(line == null) continue;
                    var words = line.Split(',');
                    passwordList.AddRange(words);
                }
            }

            return passwordList;
        }
    }
}
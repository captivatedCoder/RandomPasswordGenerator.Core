using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using PasswordGenerator.Diceware;
using PasswordGenerator.Generators;

namespace PasswordGenerator
{
    class GetPassphrase
    {
        private const int NumberOfRolls = 5;
        private const int NumberOfSides = 6;
        private static int _wordsNeeded = 7;
        private static string _filePath;
        private static string _divider;

        public static SecureString MakePassphrase()
        {
            Menu();
            var rolls = GetRolls();
            var words = GetWords(_filePath);

            return BuildPassphrase(rolls, words).ToSecureString();
        }

        private static void Menu()
        {
            var choice = GetInput("Press 1 for automated, 2 for custom: ");

            if (string.IsNullOrEmpty(choice)) return;
            switch (choice)
            {
                case "1":
                    {
                        _divider = "-";
                        _wordsNeeded = 7;
                        GetFilePath(@"diceware.wordlist.txt");
                        break;
                    }

                case "2":
                    var input = GetInput("Specify filename? (Y or N)");

                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    else
                    {
                        if (input.ToUpper() == "Y")
                        {
                            var fileName = GetInput("Enter file name: ");
                            GetFilePath(fileName);
                        }
                        else
                        {
                            GetFilePath(@"\diceware.wordlist.txt");
                        }
                    }

                    input = GetInput("Select word count? (Y or N)");

                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    else
                    {
                        _wordsNeeded = input.ToUpper() == "Y" ? int.Parse(GetInput("How many words: ")) : 7;
                    }

                    input = GetInput("Select word divider? (Y or N)");

                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    else
                    {
                        _divider = input.ToUpper() == "Y" ? GetInput("Specify word divider: ") : "-";
                    }
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        private static string GetInput(string text)
        {
            Console.Write(text);

            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException();
            }

            return input;
        }
        
        private static IEnumerable<string> GetRolls()
        {
            var rnd = new CryptoRandom();
            var allValues = new List<string>();

            var value = new int[NumberOfRolls];

            for(var i = 0; i < _wordsNeeded; i++)
            {
                for(var roll = 0; roll < NumberOfRolls; roll++)
                {
                    value[roll] = rnd.Next(1, NumberOfSides);
                }

                allValues.Add(string.Join("", value));
            }

            return allValues;
        }

        private static List<DicewareFileData> GetWords(string filePath)
        {
            var fileReader = new DicewareFileReader(filePath);

            fileReader.WordList();

            return fileReader.FileData;
        }

        private static string BuildPassphrase(IEnumerable<string> rolls, List<DicewareFileData> words)
        {
            var passWords = rolls.Select(roll => words.Find(x => x.IdNumber == roll)).Select(result => result.Word)
                .ToList();

            var password = new StringBuilder();

            foreach (var word in passWords)
            {
                password.Append($"{word}{_divider}");
            }

            return password.ToString().Substring(0,(password.Length-1));
        }

        private static void GetFilePath(string fileName)
        {
            var filePath = Directory.GetCurrentDirectory();
            _filePath = Path.GetFullPath(Path.Combine(fileName));
        }
    }
}

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.Generators
{
    public static class WeakPassphraseGenerator
    {
        private static readonly StringBuilder Password = new StringBuilder();
        private static readonly RNGCryptoServiceProvider RngSeed = new RNGCryptoServiceProvider();
        private static readonly Random RandomDigit = new Random();

        public static string ReturnWeakPassword()
        {
            GetWord();
            GetDigits();
            
            return Password.ToString();
        }

        private static void GetWord()
        {
            var randomNumber = new byte[1];
            RngSeed.GetBytes(randomNumber);

        }

        private static void GetDigits()
        {
            var passwordDigits = new int[3];

            for (var i = 0; i <= 2; i++)
            {
                passwordDigits[i] = RandomDigit.Next(0, 9);
            }

            Password.Append(passwordDigits.Aggregate(string.Empty, (s, i) => s + i.ToString()));
        }
    }
}
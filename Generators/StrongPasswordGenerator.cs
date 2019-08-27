using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.Generators
{
    public static class StrongPasswordGenerator
    {
        private static readonly StringBuilder Password = new StringBuilder();
        private static readonly string ValidCharacters = "abcdefghjkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789#@%&*$";
        private static readonly RNGCryptoServiceProvider RngSeed = new RNGCryptoServiceProvider();
        private const int MinimumLength = 8;  //Yes evil magic number but 8 is a reasonable minimum

        public static SecureString ReturnGoodPassword(int passwordLength)
        {
            if (passwordLength < MinimumLength)
                return null;

            var validPassword = false;
            while (!validPassword)
            {
                Password.Clear();
                CharSetPwd(passwordLength, ValidCharacters);
                if (CheckComplexity(Password.ToString()))
                {
                    validPassword = true;
                }
            }

            return Password.ToString().ToSecureString();
        }

        private static bool CheckComplexity(string password)
        {
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(char.IsPunctuation);
        }

        private static void CharSetPwd(int numberOfCharacters, string characterStrings)
        {
            var randomNumbersArray = new byte[numberOfCharacters];
            RngSeed.GetBytes(randomNumbersArray);
            byte[] badValueBuffer = null;

            var maxRandomNumber = byte.MaxValue - ((byte.MaxValue + 1) % characterStrings.Length);

            for (var i = 0; i < numberOfCharacters; i++)
            {
                var randomNumber = randomNumbersArray[i];

                while (randomNumber > maxRandomNumber)
                {
                    if (badValueBuffer == null)
                    {
                        badValueBuffer = new byte[1];
                    }

                    RngSeed.GetBytes(badValueBuffer);
                    randomNumber = badValueBuffer[0];
                }

                Password.Append(characterStrings[randomNumber % characterStrings.Length]);
            }
        }
    }
}

using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator
{
    public static class StrongPasswordGenerator
    {
        private static readonly StringBuilder _password = new StringBuilder();
        private static readonly string _validCharacters = "abcdefghjkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789#@%&*$";
        private static readonly RNGCryptoServiceProvider _rngSeed = new RNGCryptoServiceProvider();
        private static readonly int _minimumLength = 8;  //Yes evil magic number but 8 is a reasonable minimum

        public static SecureString ReturnGoodPassword(int passwordLength)
        {
            if (passwordLength < _minimumLength)
                return null;

            var validPassword = false;
            while (!validPassword)
            {
                _password.Clear();
                CharSetPwd(passwordLength, _validCharacters);
                if (CheckComplexity(_password.ToString()))
                {
                    validPassword = true;
                }
            }

            return _password.ToString().ToSecureString();
        }

        private static bool CheckComplexity(string password)
        {
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(char.IsPunctuation);
        }

        private static void CharSetPwd(int numberOfCharacters, string characterStrings)
        {
            var randomNumbersArray = new byte[numberOfCharacters];
            _rngSeed.GetBytes(randomNumbersArray);
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

                    _rngSeed.GetBytes(badValueBuffer);
                    randomNumber = badValueBuffer[0];
                }

                _password.Append(characterStrings[randomNumber % characterStrings.Length]);
            }
        }
    }
}

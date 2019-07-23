using System;
using System.Security;
using RandomPasswordGenerator;

namespace PasswordGenerator
{
    public static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            var exit = 1;

            while (exit == 1)
            {
                var _password = new SecureString();

                Console.WriteLine("What type of password do you need?");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Weak passphrase");
                Console.WriteLine("2. Strong password");
                Console.WriteLine("3. Exit");
                Console.WriteLine("----------------------------------");


                var input = Console.ReadKey(true).KeyChar;

                switch (input)
                {
                    case '1':
                        _password = GetPassword.WeakPassword();
                        break;
                    case '2':
                        _password = GetPassword.StrongPassword();
                        break;
                    case '3':
                        exit = 0;
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }

                if (string.IsNullOrEmpty(_password.ToInsecureString()) && exit != 0)
                {
                    Console.WriteLine("\nError creating password.\n\n");

                    Console.WriteLine("Press enter to close");
                    Console.ReadKey();
                }
                else if (exit != 0)
                {
                    Console.WriteLine($"\nYour password is: {_password.ToInsecureString()}");

                    Console.WriteLine("Press enter to continue");
                    Console.ReadKey();

                    Console.Clear();
                }
            }
        }
    }
}
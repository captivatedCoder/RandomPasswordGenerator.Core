using System;
using System.Security;

namespace PasswordGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var exit = 1;

            while(exit == 1)
            {
                var password = new SecureString();

                Console.WriteLine("What type of password do you need?");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Weak password");
                Console.WriteLine("2. Strong password");
                Console.WriteLine("3. Generate passphrase");
                Console.WriteLine("4. Exit");
                Console.WriteLine("----------------------------------");


                var input = Console.ReadKey(true).KeyChar;

                switch(input)
                {
                    case '1':
                        password = GetPassword.WeakPassword();
                        break;
                    case '2':
                        password = GetPassword.StrongPassword();
                        break;
                    case '3':
                        password = GetPassphrase.MakePassphrase();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }

                if(string.IsNullOrEmpty(password.ToInsecureString()) && exit != 0)
                {
                    Console.WriteLine("\nError creating password.\n\n");

                    Console.WriteLine("Press enter to close");
                    Console.ReadKey();
                }
                else if(exit != 0)
                {
                    Console.WriteLine($"\nYour password is: {password.ToInsecureString()}");

                    Console.WriteLine("Press enter to continue");
                    Console.ReadKey();

                    Console.Clear();
                }
            }
        }

        
    }
}
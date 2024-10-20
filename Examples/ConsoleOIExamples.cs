using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    internal class ConsoleOIExamples
    {
        public static void RunExamples()
        {
            // Basic output
            Console.WriteLine("Hello, World!");
            Console.Write("This is on the same line. ");
            Console.WriteLine("This is on a new line.");

            // Formatting output
            int age = 30;
            string name = "John";
            Console.WriteLine("My name is {0} and I am {1} years old.", name, age);
            Console.WriteLine($"My name is {name} and I am {age} years old.");

            // Reading input
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine($"Hello, {userName}!");

            // Reading and parsing numeric input
            Console.Write("Enter your age: ");
            if (int.TryParse(Console.ReadLine(), out int userAge))
            {
                Console.WriteLine($"In 10 years, you will be {userAge + 10} years old.");
            }
            else
            {
                Console.WriteLine("Invalid age entered.");
            }

            // Reading a single key
            Console.WriteLine("Press any key to continue...");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine($"You pressed: {keyInfo.KeyChar}");

            // Changing console colors
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("This text is green.");
            Console.ResetColor();

            // Formatting numbers and dates
            double price = 1234.5678;
            DateTime now = DateTime.Now;
            Console.WriteLine($"The price is {price:C2}");
            Console.WriteLine($"Today's date is {now:d}");
            Console.WriteLine($"The time is {now:T}");

            // Creating a simple menu
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Option 1");
                Console.WriteLine("2. Option 2");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("You chose Option 1");
                        break;
                    case "2":
                        Console.WriteLine("You chose Option 2");
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}

namespace Examples
{
    internal class Basics
    {
        static void Main(string[] args)
        {
            ///DATA TYPES and VARIABLES
            ///
            // Value types
            int age = 30;
            double salary = 50000.50;
            bool isEmployed = true;
            char grade = 'A';

            // Reference types
            string name = "John Doe";
            int[] numbers = { 1, 2, 3, 4, 5 };

            // Type inference
            var count = 10; // inferred as int
            var message = "Hello"; // inferred as string



            ///OPERATORS
            ///
            // Arithmetic operators
            int a = 10, b = 3;
            Console.WriteLine($"Addition: {a + b}");
            Console.WriteLine($"Subtraction: {a - b}");
            Console.WriteLine($"Multiplication: {a * b}");
            Console.WriteLine($"Division: {a / b}");
            Console.WriteLine($"Modulus: {a % b}");

            // Comparison operators
            Console.WriteLine($"Equal: {a == b}");
            Console.WriteLine($"Not equal: {a != b}");
            Console.WriteLine($"Greater than: {a > b}");
            Console.WriteLine($"Less than or equal: {a <= b}");

            // Logical operators
            bool x = true, y = false;
            Console.WriteLine($"AND: {x && y}");
            Console.WriteLine($"OR: {x || y}");
            Console.WriteLine($"NOT: {!x}");

            // Assignment operators
            int c = 5;
            c += 3; // equivalent to c = c + 3
            Console.WriteLine($"c after +=: {c}");

            ///
            ///CONTROL STRUCTURES
            ///
            // if-else statement
            int number = 10;
            if (number > 0)
            {
                Console.WriteLine("Positive number");
            }
            else if (number < 0)
            {
                Console.WriteLine("Negative number");
            }
            else
            {
                Console.WriteLine("Zero");
            }

            // switch statement
            string day = "Monday";
            switch (day)
            {
                case "Monday":
                case "Tuesday":
                case "Wednesday":
                case "Thursday":
                case "Friday":
                    Console.WriteLine("Weekday");
                    break;
                case "Saturday":
                case "Sunday":
                    Console.WriteLine("Weekend");
                    break;
                default:
                    Console.WriteLine("Invalid day");
                    break;
            }

            // for loop
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Iteration {i}");
            }

            // while loop
            int counter = 0;
            while (counter < 3)
            {
                Console.WriteLine($"Counter: {counter}");
                counter++;
            }

            // do-while loop
            int j = 0;
            do
            {
                Console.WriteLine($"j is {j}");
                j++;
            } while (j < 3);

            // foreach loop
            string[] fruits = { "apple", "banana", "orange" };
            foreach (string fruit in fruits)
            {
                Console.WriteLine(fruit);
            }

            ///
            /// METHODS and FUNCTIONS
            ///

            AlgorithmsAndDataStructures.RunExamples();
        }
    }
}

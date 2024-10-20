using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class UserManager
    {
        public void CreateUser(string username) { /* ... */ }
        public void DeleteUser(string username) { /* ... */ }
    }

    class EmailService
    {
        public void SendEmail(string to, string subject, string body) { /* ... */ }
    }

    // Open-Closed Principle
    abstract class Shape
    {
        public abstract double CalculateArea();
    }

    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override double CalculateArea() => Width * Height;
    }

    class Circle : Shape
    {
        public double Radius { get; set; }
        public override double CalculateArea() => Math.PI * Radius * Radius;
    }

    // Liskov Substitution Principle
    class Bird
    {
        public virtual void Fly() { Console.WriteLine("The bird is flying"); }
    }

    class Sparrow : Bird { }

    class Ostrich : Bird
    {
        public override void Fly() { throw new NotSupportedException("Ostriches can't fly"); }
    }

    // Interface Segregation Principle
    interface IWorker
    {
        void Work();
    }

    interface IEater
    {
        void Eat();
    }

    class Human : IWorker, IEater
    {
        public void Work() { Console.WriteLine("Human is working"); }
        public void Eat() { Console.WriteLine("Human is eating"); }
    }

    class Robot : IWorker
    {
        public void Work() { Console.WriteLine("Robot is working"); }
    }

    // Dependency Inversion Principle
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message) { Console.WriteLine(message); }
    }

    class FileLogger : ILogger
    {
        public void Log(string message) { /* Log to file */ }
    }

    class UserService
    {
        private readonly ILogger _logger;

        public UserService(ILogger logger)
        {
            _logger = logger;
        }

        public void RegisterUser(string username)
        {
            // Register user logic
            _logger.Log($"User {username} registered");
        }
    }

    // Example of using these principles
    internal class PatternsAndBestPractices
    {
        /*static void Main()
        {
            // Using Open-Closed Principle
            List<Shape> shapes = new List<Shape>
        {
            new Rectangle { Width = 5, Height = 3 },
            new Circle { Radius = 2 }
        };

            foreach (var shape in shapes)
            {
                Console.WriteLine($"Area: {shape.CalculateArea()}");
            }

            // Using Dependency Inversion Principle
            ILogger consoleLogger = new ConsoleLogger();
            UserService userService = new UserService(consoleLogger);
            userService.RegisterUser("john_doe");

            // Exception handling
            try
            {
                int result = Divide(10, 0);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("This always executes");
            }
        }

        // Example of a method with proper exception handling
        static int Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return a / b;
        }*/
    }

    // Example of proper naming conventions and code organization
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public class OrderProcessor
    {
        private readonly ILogger _logger;

        internal OrderProcessor(ILogger logger)
        {
            _logger = logger;
        }

        public void ProcessOrder(Order order)
        {
            try
            {
                // Process order logic
                _logger.Log($"Order {order.Id} processed successfully");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error processing order {order.Id}: {ex.Message}");
                throw;
            }
        }
    }

    public class Order
    {
        public int Id { get; set; }
        // Other order properties
    }
}

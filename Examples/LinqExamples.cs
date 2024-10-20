using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    internal class LinqExamples
    {
        public static void RunExamples()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Query syntax
            var evenNumbersQuery = from num in numbers
                                   where num % 2 == 0
                                   select num;

            Console.WriteLine("Even numbers (Query syntax):");
            foreach (var num in evenNumbersQuery)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Method syntax
            var evenNumbersMethod = numbers.Where(num => num % 2 == 0);

            Console.WriteLine("Even numbers (Method syntax):");
            foreach (var num in evenNumbersMethod)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Other LINQ methods
            var sortedNumbers = numbers.OrderByDescending(num => num);
            var sum = numbers.Sum();
            var average = numbers.Average();

            Console.WriteLine($"Sum: {sum}, Average: {average}");

            // Working with complex objects
            List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25 },
            new Person { Name = "Bob", Age = 30 },
            new Person { Name = "Charlie", Age = 35 },
            new Person { Name = "David", Age = 28 }
        };

            var namesOfPeopleOver30 = people.Where(p => p.Age > 30)
                                            .Select(p => p.Name);

            Console.WriteLine("Names of people over 30:");
            foreach (var name in namesOfPeopleOver30)
            {
                Console.WriteLine(name);
            }

            // Grouping
            var groupedByAge = people.GroupBy(p => p.Age / 10 * 10);

            foreach (var group in groupedByAge)
            {
                Console.WriteLine($"Age group: {group.Key}s");
                foreach (var person in group)
                {
                    Console.WriteLine($"  {person.Name} - {person.Age}");
                }
            }

            // Join operation
            List<Department> departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" }
        };

            List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "John", DepartmentId = 1 },
            new Employee { Name = "Jane", DepartmentId = 2 },
            new Employee { Name = "Bob", DepartmentId = 2 }
        };

            var employeeDepartments = employees.Join(departments,
                e => e.DepartmentId,
                d => d.Id,
                (e, d) => new { EmployeeName = e.Name, DepartmentName = d.Name });

            Console.WriteLine("Employees and their departments:");
            foreach (var ed in employeeDepartments)
            {
                Console.WriteLine($"{ed.EmployeeName} - {ed.DepartmentName}");
            }
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class Department
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class Employee
        {
            public string Name { get; set; }
            public int DepartmentId { get; set; }
        }
    }
}

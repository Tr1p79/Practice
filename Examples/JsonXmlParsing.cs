using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Examples
{
    internal class JsonXmlParsing
    {
        public static void JsonExample()
        {
            // Create an object to serialize
            var person = new Person
            {
                Name = "John Doe",
                Age = 30,
                Email = "john@example.com"
            };

            // Serialize to JSON
            string jsonString = JsonSerializer.Serialize(person);
            Console.WriteLine("Serialized JSON:");
            Console.WriteLine(jsonString);

            // Deserialize from JSON
            Person deserializedPerson = JsonSerializer.Deserialize<Person>(jsonString);
            Console.WriteLine("\nDeserialized Person:");
            Console.WriteLine($"Name: {deserializedPerson.Name}");
            Console.WriteLine($"Age: {deserializedPerson.Age}");
            Console.WriteLine($"Email: {deserializedPerson.Email}");
        }

        public static void XmlExample()
        {
            // Create an XML document
            XDocument doc = new XDocument(
                new XElement("People",
                    new XElement("Person",
                        new XElement("Name", "Jane Smith"),
                        new XElement("Age", 28),
                        new XElement("Email", "jane@example.com")
                    ),
                    new XElement("Person",
                        new XElement("Name", "Bob Johnson"),
                        new XElement("Age", 35),
                        new XElement("Email", "bob@example.com")
                    )
                )
            );

            // Save XML to string
            string xmlString = doc.ToString();
            Console.WriteLine("XML Document:");
            Console.WriteLine(xmlString);

            // Parse XML and extract data
            XDocument parsedDoc = XDocument.Parse(xmlString);
            var people = parsedDoc.Descendants("Person").Select(p => new Person
            {
                Name = p.Element("Name").Value,
                Age = int.Parse(p.Element("Age").Value),
                Email = p.Element("Email").Value
            });

            Console.WriteLine("\nParsed People:");
            foreach (var person in people)
            {
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Email: {person.Email}");
            }
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}

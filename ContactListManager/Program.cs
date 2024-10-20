using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ContactListManager
{
    class Contact
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    internal class Program
    {
        static List<Contact> contactList = new List<Contact>();

        static void Main(string[] args)
        {
            while(true) {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        ViewAllContacts();
                        break;
                    case "3":
                        SearchContact();
                        break;
                    case "4":
                        DeleteContact();
                        break;
                    case "5":
                        Console.WriteLine("Closing the app.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }    
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nContact List Manager");
            Console.WriteLine("1. Add new contact");
            Console.WriteLine("2. View all contacts");
            Console.WriteLine("3. Search for contact (by name)");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Exit program");
            Console.Write("Select an option: ");
        }

        static void AddContact()
        {
            string name = GetValidInput("Enter name: ", ValidateName);
            string phone = GetValidInput("Enter phone: ", ValidatePhone);
            string email = GetValidInput("Enter email: ", ValidateEmail);

            contactList.Add(new Contact { Name = name, Phone = phone, Email = email });
            Console.WriteLine("Contact added succesfully.");
        }

        static string GetValidInput(string prompt, Func<string, bool> validationFunc)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!validationFunc(input));

            return input;
        }

        static bool ValidateName(string name) 
        { 
            if(string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please try again.");
                return false;
            }
            return true;
        }
        static bool ValidatePhone(string phone) 
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Phone cannot be empty. Please try again.");
                return false;
            }
            if (!Regex.IsMatch(phone, @"^\+?[0-9]{10,14}$"))
            {
                Console.WriteLine("Invalid phone number format. Please enter 10-14 digits, optionally starting with '+'.");
                return false;
            }
            return true;
        }
        static bool ValidateEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty. Please try again.");
                return false;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Invalid email format. Please try again.");
                return false;
            }
            return true;
        }

        static void ViewAllContacts()
        {
            if (contactList.Count == 0) 
            {
                Console.WriteLine("There are no contacts.");
                return;
            }

            foreach (Contact contact in contactList) 
            {
                Console.WriteLine($"Name: {contact.Name}, Phone: {contact.Phone}, Email: {contact.Email}");
            }
        }

        static void SearchContact()
        {
            Console.Write("Enter name to search: ");
            string searchName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

            var foundContacts = contactList.Where(c => c.Name.ToLower().Contains(searchName.ToLower())).ToList();

            if (foundContacts.Count == 0) 
            {
                Console.WriteLine("No contacts found."); 
            }
            else
            {
                foreach (Contact contact in foundContacts) 
                { 
                    Console.WriteLine($"Name: {contact.Name}, Phone: {contact.Phone}, Email: {contact.Email}");
                }
            }
        }

        static void DeleteContact()
        {
            Console.Write("Enter the name of contact to delete: ");
            string nameToDelete = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nameToDelete))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            var contactToDelete = contactList.FirstOrDefault(c => c.Name.ToLower() == nameToDelete.ToLower());

            if (contactToDelete != null) 
            { 
                contactList.Remove(contactToDelete);
                Console.WriteLine("Contact removed succesfully.");
            }
            else 
            {
                Console.WriteLine("Contact not found.");
            }
        }
    }
}

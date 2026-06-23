using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_Book
{
    public class ContactService
    {
        private List<Contact> contacts = new List<Contact>();
        private readonly DataService _dataService;
        public ContactService(DataService dataService)
        {
            _dataService = dataService;
            contacts = _dataService.LoadContacts();
        }
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome To The Contact Book, Menu");
            Console.WriteLine("1. Add Contacts");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Search a Contact");
            Console.WriteLine("4. Delete a Contact");
            Console.WriteLine("0. Save and Exit");

            string choice = Console.ReadLine()?? "";

            switch (choice)
            {
                case "1": AddContacts(); return;
                case "2": ViewContacts(); return;
                case "3": Search(); return;
                case "4":Delete(); return;
                case "0":
                    _dataService.SaveData(contacts);
                    Environment.Exit(0); return;

                default:
                    break;
            }
        }
        public void AddContacts()
        {
            Console.Clear();
            Contact contact = new Contact();
            Console.WriteLine("Enter Name:\n");
            contact.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Enter a Number:\n");
            contact.Number = Console.ReadLine()?? "";
            Console.Clear();
            contacts.Add(contact);
            Console.WriteLine($"Contact: {contact.Name} Added\n\n\nPress any Key to Continue...");
            Console.ReadKey();
        }

        public void ViewContacts()
        {
            if (contacts.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("The Contact list is Empty");
                Console.WriteLine(" You should add them first\n\n" +
                                  "Press any Key to Continue...");
                Console.WriteLine("=============================");

                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                int n = 1;
                foreach (var item in contacts)
                {
                    Console.WriteLine("===========================");
                    Console.WriteLine($"{n}.\nName: {item.Name}");
                    Console.WriteLine($"Number:{item.Number}\n\n");
                    n++;
                }
                Console.WriteLine("Press Any Key to go back...");
                Console.ReadKey();
            }
        }
        
        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Enter a Contact Name");
            string input = Console.ReadLine() ?? "";
            Contact? Found = contacts.Find(x => x.Name.ToLower().Contains(input.ToLower()));
            if(Found == null)
            {
                Console.WriteLine("Contact Not Found");
                Console.WriteLine("Press any Key to Continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"\nName: {Found.Name}");
                Console.WriteLine($"Number:{Found.Number}\n\n");

                Console.WriteLine("Press any Key to Continue");
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            int n = 1;
            foreach (var item in contacts)
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"{n}.\nName: {item.Name}");
                Console.WriteLine($"Number:{item.Number}\n\n");
                n++;
            }

            Console.WriteLine("Choose a Contact to Delete");
            int.TryParse(Console.ReadLine(), out n);
            
            try
            {
                
                contacts.RemoveAt(n - 1);
            }

            catch
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                Console.WriteLine("Press Any Key To Continue");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine($"Contact Removed");
            Console.WriteLine("Press any Key to Continue....");
            Console.ReadKey();
        }

    }
}

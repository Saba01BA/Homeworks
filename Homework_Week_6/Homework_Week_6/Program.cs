using System.Globalization;
using System.Runtime.InteropServices;

namespace Homework_Week_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            #region Task 1

            Console.WriteLine("Hi, Please enter the size of an Array :)");

            int size = Convert.ToInt32(Console.ReadLine());

            int[] nums = new int[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Please enter the Array Member on index {i}");
                nums[i] = Convert.ToInt32(Console.ReadLine());

            }

            List<int> oddNums = new List<int>();
            List<int> evenNums = new List<int>();

            for (int i = 0; i < size; i++)
            {
                if (nums[i] % 2 == 0)
                {
                    evenNums.Add(nums[i]);
                }
                else
                {
                    oddNums.Add(nums[i]);
                }
            }

            Console.WriteLine($"Even Numbers: {String.Join(" ",evenNums)}\n\n Odd Numbers: {string.Join(" ",oddNums)}");

            #endregion
            

            #region Task 2

            Dictionary<string, string> myContactList = new Dictionary<string, string>();
            Console.WriteLine("Welcome to your Contacts.");
            var userChoice = "";
            var contactName = "";
            var contactNumber = "";
            while (userChoice != "quit")
            {
                Console.WriteLine("Choose and Action (List, Add, Remove, Update, Quit)");
                userChoice = Console.ReadLine().Trim().ToLower();

                switch(userChoice)
                {
                    case "list":
                        foreach (var item in myContactList)
                        {
                            Console.WriteLine($"Name:{item.Key}\nNumber:{item.Value}\n\n");
                        }
                        break;
                        
                    case "add":
                        Console.WriteLine("Enter Name");
                        contactName = Console.ReadLine();
                        Console.WriteLine("Enter Number");
                        contactNumber = Console.ReadLine();
                        myContactList.Add(contactName, contactNumber);

                        break;
                    
                    case "remove":
                        Console.WriteLine("Enter Name to remove from the Contact List");
                        contactName = Console.ReadLine();
                        myContactList.Remove(contactName);


                        break;
                    
                    case ("update"):
                        Console.WriteLine("Enter Name");
                        contactName = Console.ReadLine();
                        Console.WriteLine("Enter Number to update it");
                        myContactList[contactName] = Console.ReadLine();


                        break;

                    case ("quit"):

                        userChoice = "quit";

                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;

                }
            }


            #endregion

            

            #region Task 3


            Console.WriteLine("Hi, Please enter the size of an Array :)");

            int size1 = Convert.ToInt32(Console.ReadLine()); //size already declared!

            int[] nums1 = new int[size1];

            for (int i = 0; i < size1; i++)
            {
                Console.WriteLine($"Please enter the Array Member on index {i}");
                nums1[i] = Convert.ToInt32(Console.ReadLine());

            }

            var group = nums1.GroupBy(x => x);

            foreach (var item in group)
            {
                Console.WriteLine($"{item.Key} apperars {item.Count()} times sum {item.Sum()}");
            }

            
            #endregion
            
            #region Task 4

            List<int> studentScore = new List<int> { 30, 45, 41, 76, 21, 99, 80, 76, 96, 40 };
            Console.WriteLine("How many do you want to see?");
            int n = Convert.ToInt32(Console.ReadLine());

            var result = studentScore.OrderByDescending(x=>x).Take(n); //its a COLLECTION

            foreach (var item in result)
            {
                Console.WriteLine(item); //not item.Value... because its just ints there. if there were string then thats another story.
            }
            

            #endregion

        }
    }
}

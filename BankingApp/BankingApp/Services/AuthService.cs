using BankingApp.Models;

namespace BankingApp.Services
{
    public class AuthService
    {
        private readonly DataService _dataService;
        private readonly LoggerService _logger;

        public AuthService(DataService dataService, LoggerService logger)
        {
            _dataService = dataService;
            _logger = logger;
        }


        public BankAccount? Authenticate()
        {
            BankAccount account = _dataService.LoadAccount();

            Console.Clear();
            Console.WriteLine("WELCOME TO ATM");
            Console.WriteLine();
            Console.Write("Enter Card Number: ");
            string cardNumber = Console.ReadLine() ?? "";

            Console.Write("Enter CVC: ");
            string cvc = Console.ReadLine() ?? "";

            Console.Write("Enter Expiration Date (MM/YY):");
            string expDate = Console.ReadLine() ?? "";

            bool cardValid = account.CardDetails.CardNumber == cardNumber &&
                             account.CardDetails.Cvc == cvc &&
                             account.CardDetails.ExpirationDate == expDate;

            if (!cardValid)
            {
                Console.WriteLine();
                Console.WriteLine("Please Provide Correct Data");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                return null;
            }

         
            Console.Clear();
            Console.WriteLine("ENTER YOUR PIN");
            Console.WriteLine();
            Console.Write("Enter PIN:");
            string pin = Console.ReadLine() ?? "";

            if (account.PinCode != pin)
            {
                Console.WriteLine();
                Console.WriteLine("Please Provide Correct Pin");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                return null;
            }

            _logger.LogInfo($"User {account.FirstName} {account.LastName} logged in successfully.");
             return account;
        }
    }
}
using BankingApp.Models;

namespace BankingApp.Services
{
    public class BankService
    {
        private readonly DataService _dataService;
        private readonly LoggerService _logger;
        private const double GEL_TO_USD = 0.37;
        private const double GEL_TO_EUR = 0.34;

        public BankService(DataService dataService, LoggerService logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public void ShowMenu(BankAccount account)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hello, {account.FirstName} {account.LastName}!");
                Console.WriteLine("\n1. Check Balance");
                Console.WriteLine("2. Withdraw Amount");
                Console.WriteLine("3. Last 5 Transactions");
                Console.WriteLine("4. Deposit Amount");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Currency Conversion");
                Console.WriteLine("0. Exit");
                Console.Write("\n  Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": CheckBalance(account); return;
                    case "2": Withdraw(account); return;
                    case "3": LastTransactions(account); return;
                    case "4": Deposit(account); return;
                    case "5": ChangePin(account); return;
                    case "6": CurrencyConversion(account); return;
                    case "0":
                        Console.WriteLine("\nGoodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n Invalid option, try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CheckBalance(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("YOUR BALANCE");
            Console.WriteLine($"GEL: {account.Balance.Gel}");
            Console.WriteLine($"USD: {account.Balance.Usd}");
            Console.WriteLine($"EUR: {account.Balance.Eur}");

            SaveTransaction(account, "BalanceInquiry", 0, 0, 0);
            _logger.LogInfo($"User checked balance. GEL: {account.Balance.Gel}");

            Console.WriteLine("\n  Press any key to return...");
            Console.ReadKey();
        }

        private void Withdraw(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("WITHDRAW");
            Console.Write($"\n  Current Balance: {account.Balance.Gel} GEL");
            Console.Write("\n  Enter amount to withdraw (GEL): ");

            string input = Console.ReadLine() ?? "";

            if (!double.TryParse(input, out double amount) || amount <= 0)
            {
                Console.WriteLine("\nInvalid amount.");
                Console.ReadKey();
                return;
            }

            if (amount > account.Balance.Gel)
            {
                _logger.LogWarning("Withdraw failed - insufficient funds.");
                Console.WriteLine("\nInsufficient funds.");
                Console.ReadKey();
                return;
            }

            account.Balance.Gel -= amount;
            SaveTransaction(account, "GetAmount", amount, 0, 0);
            _logger.LogInfo($"User withdrew {amount} GEL. New balance: {account.Balance.Gel} GEL");

            Console.WriteLine($"\nWithdrawn: {amount} GEL");
            Console.WriteLine($"New Balance: {account.Balance.Gel} GEL");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void LastTransactions(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("LAST 5 TRANSACTIONS");

            var last5 = account.TransactionHistory
                .OrderByDescending(t => t.Timestamp)
                .Take(5)
                .ToList();

            if (last5.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
            }
            else
            {
                foreach (var t in last5)
                {
                    Console.WriteLine($"{t.Type} | GEL:{t.AmountGEL}");
                    Console.WriteLine($"{t.Timestamp:yyyy-MM-dd HH:mm}");
                }
            }

            Console.WriteLine("\n  Press any key to return...");
            Console.ReadKey();
        }

        private void Deposit(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("DEPOSIT");
            Console.Write("\nEnter amount to deposit (GEL): ");

            string input = Console.ReadLine() ?? "";

            if (!double.TryParse(input, out double amount) || amount <= 0)
            {
                _logger.LogError ($"Tried to deposit Invalid Amount");

                Console.WriteLine("\nInvalid amount.");
                Console.ReadKey();
                return;
            }

            account.Balance.Gel += amount;
            SaveTransaction(account, "FillAmount", amount, 0, 0);
            _logger.LogInfo($"User deposited {amount} GEL. New balance: {account.Balance.Gel} GEL");

            Console.WriteLine($"\nDeposited: {amount} GEL");
            Console.WriteLine($"New Balance: {account.Balance.Gel} GEL");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ChangePin(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("CHANGE PIN");
            Console.Write("\nEnter current PIN: ");
            string currentPin = Console.ReadLine() ?? "";

            if (currentPin != account.PinCode)
            {
                Console.WriteLine("\nIncorrect current PIN.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter new PIN: ");
            string newPin = Console.ReadLine() ?? "";

            Console.Write("Confirm new PIN: ");
            string confirmPin = Console.ReadLine() ?? "";

            if (newPin != confirmPin)
            {
                Console.WriteLine("\nPINs do not match.");
                Console.ReadKey();
                return;
            }

            if (newPin.Length < 4)
            {
                Console.WriteLine("\nPIN must be at least 4 digits.");
                Console.ReadKey();
                return;
            }

            account.PinCode = newPin;
            SaveTransaction(account, "ChangePin", 0, 0, 0);
            _logger.LogInfo("User changed PIN successfully.");

            Console.WriteLine("\nPIN changed successfully!");
            Console.WriteLine("\n  Press any key to return...");
            Console.ReadKey();
        }

        
        private void CurrencyConversion(BankAccount account)
        {
            Console.Clear();
            Console.WriteLine("CURRENCY CONVERSION");
            Console.WriteLine($"GEL: {account.Balance.Gel} | USD: {account.Balance.Usd} | EUR: {account.Balance.Eur}");
            Console.WriteLine("1. GEL to USD");
            Console.WriteLine("2. GEL to EUR");
            Console.WriteLine("3. USD to GEL");
            Console.WriteLine("4. USD to EUR");
            Console.WriteLine("5. EUR to GEL");
            Console.WriteLine("6. EUR to USD");
            Console.Write("\nChoose: ");

            string choice = Console.ReadLine() ?? "";
            string currency = choice == "1" || choice == "2" ? "GEL" :
                  choice == "3" || choice == "4" ? "USD" : "EUR";
            Console.Write($"Enter amount in {currency}: ");
            string input = Console.ReadLine() ?? "";

            if (!double.TryParse(input, out double amount) || amount <= 0)
            {
                Console.WriteLine("\nInvalid amount.");
                Console.ReadKey();
                return;
            }

           

            if (choice == "1")
            {
                if (amount > account.Balance.Gel)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }

                double converted = Math.Round(amount * GEL_TO_USD, 2);
                account.Balance.Gel -= amount;
                account.Balance.Usd += converted;
                SaveTransaction(account, "CurrencyConversion", amount, converted, 0);
                _logger.LogInfo($"User converted {amount} GEL. Choice: {choice}");
                Console.WriteLine($"\n{amount} GEL to {converted} USD");
            }
            else if (choice == "2")
            {
                if (amount > account.Balance.Gel)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }

                double converted = Math.Round(amount * GEL_TO_EUR, 2);
                account.Balance.Gel -= amount;
                account.Balance.Eur += converted;
                SaveTransaction(account, "CurrencyConversion", amount, 0, converted);
                _logger.LogInfo($"User converted {amount} GEL. Choice: {choice}");
                Console.WriteLine($"\n{amount} GEL to {converted} EUR");
            }
            else if (choice == "3")
            {
                if (amount > account.Balance.Usd)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }
                double converted = Math.Round(amount / GEL_TO_USD, 2);
                account.Balance.Usd -= amount;
                account.Balance.Gel += converted;
                SaveTransaction(account, "CurrencyConversion", converted, amount, 0);
                _logger.LogInfo($"User converted {amount} USD to GEL.");
                Console.WriteLine($"\n{amount} USD to {converted} GEL");
            }
            else if (choice == "4")
            {
                if (amount > account.Balance.Usd)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }
                double converted = Math.Round(amount * (GEL_TO_EUR / GEL_TO_USD), 2);
                account.Balance.Usd -= amount;
                account.Balance.Eur += converted;
                SaveTransaction(account, "CurrencyConversion", 0, amount, converted);
                _logger.LogInfo($"User converted {amount} USD to EUR.");
                Console.WriteLine($"\n{amount} USD to {converted} EUR");
            }
            else if (choice == "5")
            {
                if (amount > account.Balance.Eur)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }
                double converted = Math.Round(amount / GEL_TO_EUR, 2);
                account.Balance.Eur -= amount;
                account.Balance.Gel += converted;
                SaveTransaction(account, "CurrencyConversion", converted, 0, amount);
                _logger.LogInfo($"User converted {amount} EUR to GEL.");
                Console.WriteLine($"\n{amount} EUR to {converted} GEL");
            }
            else if (choice == "6")
            {
                if (amount > account.Balance.Eur)
                {
                    Console.WriteLine("\n Insufficient funds.");
                    _logger.LogWarning("Conversion Failed - insufficient funds.");
                    Console.ReadKey();
                    return;
                }
                double converted = Math.Round(amount * (GEL_TO_USD / GEL_TO_EUR), 2);
                account.Balance.Eur -= amount;
                account.Balance.Usd += converted;
                SaveTransaction(account, "CurrencyConversion", 0, converted, amount);
                _logger.LogInfo($"User converted {amount} EUR to USD.");
                Console.WriteLine($"\n{amount} EUR to {converted} USD");
            }
            else
            {
                Console.WriteLine("\nInvalid choice.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        
        private void SaveTransaction(BankAccount account, string type, double gel, double usd, double eur)
        {
            account.TransactionHistory.Add(new Transaction
            {
                Type = type,
                AmountGEL = gel,
                AmountUSD = usd,
                AmountEUR = eur,
                Timestamp = DateTime.UtcNow
            });

            _dataService.SaveAccount(account);
        }
    }
}
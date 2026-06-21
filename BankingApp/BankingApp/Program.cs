using BankingApp.Models;
using BankingApp.Services;

LoggerService logger = new LoggerService();
DataService dataService = new DataService(logger);
AuthService authService = new AuthService(dataService, logger);
BankService bankService = new BankService(dataService, logger);

while (true)
{
    BankAccount? account = authService.Authenticate();

    if (account == null)
        continue;

    bankService.ShowMenu(account);
}
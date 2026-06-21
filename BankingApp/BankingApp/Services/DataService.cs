using System.Text.Json;
using BankingApp.Models;

namespace BankingApp.Services
{
    public class DataService
    {
        private readonly LoggerService _logger;
      
        private readonly string _filePath = "data.json";

        public DataService(LoggerService logger)
        {
            _logger = logger;
        }

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public BankAccount LoadAccount()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                BankAccount? account = JsonSerializer.Deserialize<BankAccount>(json, _options);

                if (account == null)
                {
                    _logger.LogError("Failed to load Account Data");
                    throw new Exception("Failed to load account data.");
                }

                return account;
            }
            catch (FileNotFoundException)
            {
                _logger.LogError("data.json file not found!");
                Console.WriteLine("ERROR: data.json file not found!");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR loading data: {ex.Message}");
                Console.WriteLine($"ERROR loading data: {ex.Message}");
                throw;
            }
        }

        public void SaveAccount(BankAccount account)
        {
            try
            {
                string json = JsonSerializer.Serialize(account, _options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR loading data: {ex.Message}");
                Console.WriteLine($"ERROR saving data: {ex.Message}");
                throw;
            }
        }
    }
}
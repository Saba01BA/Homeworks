using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Contact_Book
{

    public class DataService
    {
        private readonly string _filePath = "data.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public List<Contact> LoadContacts()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                List<Contact>? contacts= JsonSerializer.Deserialize<List<Contact>>(json, _options);
                if (contacts == null)
                    throw new Exception("Failed to Load an Account");

                return contacts;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data was not Found");
                return new List<Contact>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Contact>();
            }
        }
        public void SaveData(List<Contact> contacts)
        {
            try
            {
                string json = JsonSerializer.Serialize(contacts, _options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks:{ex.Message}");
            }
        }
    }
}

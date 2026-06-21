using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace ToDo
{
    
    public class DataService
    {
        private readonly string _filePath = "data.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public List<Task> LoadTasks()
        {
            try
            {
            string json = File.ReadAllText(_filePath);
            List<Task>? tasks = JsonSerializer.Deserialize<List<Task>>(json, _options);
            if (tasks == null)
                throw new Exception("Failed to Load an Account");

                return tasks;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data was not Found");
                return new List<Task>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Task>();
            }
        }
        public void SaveData(List<Task> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, _options);
                File.WriteAllText(_filePath, json);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error saving tasks:{ex.Message}");
            }
        }
    }
}

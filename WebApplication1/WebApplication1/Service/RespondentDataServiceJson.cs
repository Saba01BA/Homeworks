using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class RespondentDataServiceJson : IRespondentDataService
    {
        public List<Person> Load()
        {

            if (File.Exists("Respondents.json"))
            {
                string json = File.ReadAllText("Respondents.json");
                var list = JsonSerializer.Deserialize<List<Person>>(json);
                if (list == null)
                    return new List<Person>();
                return list;
            }
            return new List<Person>();
        }

        public void Save(Person person)
        {
            var list = Load();
            list.Add(person);
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText("Respondents.json", json);
        }

        public void SaveAll(List<Person> people)
        {
            string json = JsonSerializer.Serialize(people);
            File.WriteAllText("Respondents.json", json);
        }
    }
}

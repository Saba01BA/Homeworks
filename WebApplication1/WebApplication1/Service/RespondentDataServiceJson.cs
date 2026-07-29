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

                if (string.IsNullOrWhiteSpace(json))
                    return new List<Person>();

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

        public Person? GetById(int id)
        {
            return Load().FirstOrDefault(person => person.Id == id);
        }

        public bool Update(int id, Person updatedPerson)
        {
            var people = Load();
            var index = people.FindIndex(person => person.Id == id);
            if (index < 0)
                return false;

            updatedPerson.Id = id;
            people[index] = updatedPerson;
            File.WriteAllText("Respondents.json", JsonSerializer.Serialize(people));
            return true;
        }

        public bool Delete(int id)
        {
            var people = Load();
            var person = people.FirstOrDefault(person => person.Id == id);
            if (person is null)
                return false;

            people.Remove(person);
            File.WriteAllText("Respondents.json", JsonSerializer.Serialize(people));
            return true;
        }
    }
}

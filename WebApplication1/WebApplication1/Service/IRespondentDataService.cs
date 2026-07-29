using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IRespondentDataService
    {
        void Save(Person person);
        List<Person> Load();
        Person? GetById(int id);
        bool Update(int id, Person updatedPerson);
        bool Delete(int id);
    }
}

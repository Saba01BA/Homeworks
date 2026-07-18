using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IRespondentDataService
    {
        void Save(Person person);
        List<Person> Load();
        void SaveAll(List<Person> people);
    }
}

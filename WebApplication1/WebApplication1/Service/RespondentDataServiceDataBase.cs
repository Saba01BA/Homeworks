using RespondentDataTracker.Context;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class RespondentDataServiceDatabase : IRespondentDataService
    {
        private readonly PersonContext _context;

        public RespondentDataServiceDatabase(PersonContext context)
        {
            _context = context;
        }

        public List<Person> Load()
        {
            return _context.Persons.ToList();
        }

        public Person? GetById(int id)
        {
            return _context.Persons.Find(id);
        }

        public void Save(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public bool Update(int id, Person updatedPerson)
        {
            var person = _context.Persons.Find(id);
            if (person is null)
                return false;

            person.CreateDate = updatedPerson.CreateDate;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.JobPosition = updatedPerson.JobPosition;
            person.WorkExperience = updatedPerson.WorkExperience;
            person.Salary = updatedPerson.Salary;
            person.PersonAdress.City = updatedPerson.PersonAdress.City;
            person.PersonAdress.Country = updatedPerson.PersonAdress.Country;
            person.PersonAdress.HomeNumber = updatedPerson.PersonAdress.HomeNumber;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var person = _context.Persons.Find(id);
            if (person is null)
                return false;

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return true;
        }
    }
}

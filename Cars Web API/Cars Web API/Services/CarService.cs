using Cars_Web_API.Models;

namespace Cars_Web_API.Services
{
    public class CarService
    {
        private List<Car> _cars = new()
        {
            new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2020, Mileage = 45000 },
            new Car { Id = 2, Make = "BMW",    Model = "X5",    Year = 2021, Mileage = 30000 },
            new Car { Id = 3, Make = "Tesla",  Model = "Model 3", Year = 2022, Mileage = 20000 },
        };

        public List<Car> GetAll() => _cars;
        public Car? GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);
        public void Add(Car car) => _cars.Add(car);
        public bool Delete(int id)
        {
            var car = GetById(id);
            if (car == null) return false;
            _cars.Remove(car);
            return true;
        }
    }
}
using Cars_Web_API.Models;
using Cars_Web_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService = new();

        // GET api/cars
        [HttpGet]
        public ActionResult<List<Car>> GetAll()
        {
            return Ok(_carService.GetAll());
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public ActionResult<Car> GetById(int id)
        {
            var car = _carService.GetById(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        // POST api/cars
        [HttpPost]
        public ActionResult Add(Car car)
        {
            _carService.Add(car);
            return Ok("Car added successfully");
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_carService.Delete(id)) return NotFound();
            return Ok("Car deleted");
        }
    }
}
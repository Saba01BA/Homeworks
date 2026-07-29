using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespondentController : ControllerBase
    {
        private readonly IRespondentDataService _respondentDataService;
        public RespondentController(IRespondentDataService respondentDataService)
        {
            _respondentDataService = respondentDataService;
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(person);
            }

            _respondentDataService.Save(person);
            return Ok(_respondentDataService.Load());
        }
      

        [HttpGet("{id}")]
        public IActionResult ViewRespondents(int id)
        {
            var person = _respondentDataService.GetById(id);
            if (person is null)
                return NotFound($"No respondent with ID {id}");

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteByID(int id)
        {

            if (!_respondentDataService.Delete(id))
                return NotFound($"No respondent with ID {id}");

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody]Person updatedPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_respondentDataService.Update(id, updatedPerson))
                return NotFound($"No respondent with ID {id}");

            return Ok(_respondentDataService.GetById(id));

        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]string? city, [FromQuery]double? minSalary, [FromQuery]double? maxSalary, [FromQuery]double? minWorkExperience)
        {
            var list = _respondentDataService.Load();
            if (!string.IsNullOrEmpty(city))
                list = list.Where(p => p.PersonAdress.City.Contains(city, StringComparison.OrdinalIgnoreCase)).ToList();
            if (minSalary.HasValue)
                list = list.Where(p => p.Salary >= minSalary).ToList();
            if (maxSalary.HasValue)
                list = list.Where(p => p.Salary <= maxSalary).ToList();
            if (minWorkExperience.HasValue)
                list = list.Where(p => p.WorkExperience >= minWorkExperience).ToList();

            return Ok(list);
                
        }

     

    }
}

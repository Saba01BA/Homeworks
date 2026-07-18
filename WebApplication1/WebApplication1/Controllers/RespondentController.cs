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
      

        [HttpGet("{id}")] //get by ID (should return a person on the entered Index)
        public IActionResult ViewRespondents(int id)
        {
            var list = _respondentDataService.Load();
            if (id < 0 || id >= list.Count)
                return NotFound($"No respondent at index {id}");
            return Ok(list[id]);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteByID(int id)
        {

            var list = _respondentDataService.Load();
            if (id < 0 || id >= list.Count)
                return NotFound($"No respondent at index {id}");


            list.RemoveAt(id);
            _respondentDataService.SaveAll(list);
            return Ok(list);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody]Person updatedPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var list=_respondentDataService.Load();
            if (id<0 || id>= list.Count)
                return NotFound($"No Respond at Index {id}");
            list[id] = updatedPerson;
            _respondentDataService.SaveAll(list);
            return Ok(list);

        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]string? city, [FromQuery]double? minSalary, [FromQuery]double? maxSalary, [FromQuery]double? minWorkExperience)
        {
            var list = _respondentDataService.Load();
            if (!string.IsNullOrEmpty(city))
                list = list.Where(p => p.PersonAdress.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
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

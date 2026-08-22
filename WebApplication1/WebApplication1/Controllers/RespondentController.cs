using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RespondentDataTracker.Context;
using RespondentDataTracker.Models;
using RespondentDataTracker.Models.Dto;
using RespondentDataTracker.Service;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespondentController : ControllerBase
    {
        private readonly IRespondentDataService _respondentDataService;
        private readonly PersonContext _personContext;
        private readonly TokenService _tokenService;
        public RespondentController(IRespondentDataService respondentDataService, PersonContext personContext, TokenService tokenService)
        {
            _respondentDataService = respondentDataService;
            _personContext = personContext;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_personContext.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email Already in Use");
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };
            _personContext.Users.Add(user);
            _personContext.SaveChanges();
            return Ok("User Registered Successfully");
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _personContext.Users.
                FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid Credentials");
            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }

        [Authorize(Roles ="Admin")]
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

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ViewRespondents(int id)
        {
            var person = _respondentDataService.GetById(id);
            if (person is null)
                return NotFound($"No respondent with ID {id}");

            return Ok(person);
        }


        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteByID(int id)
        {

            if (!_respondentDataService.Delete(id))
                return NotFound($"No respondent with ID {id}");

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody]Person updatedPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_respondentDataService.Update(id, updatedPerson))
                return NotFound($"No respondent with ID {id}");

            return Ok(_respondentDataService.GetById(id));

        }
        [Authorize]
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

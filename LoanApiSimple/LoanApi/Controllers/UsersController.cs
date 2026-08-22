using LoanApi.Models.Dto;
using LoanApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInformation()
        {
            var userId = GetUserId();
            var user = await _accountService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id, user.FirstName, user.LastName, user.UserName,
                user.Age, user.Email, user.MonthlyIncome,
                user.IsBlocked, user.BlockedUntil, user.Role
            });
        }

        [Authorize(Roles = "Accountant")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _accountService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(new
            {
                user.Id, user.FirstName, user.LastName, user.UserName,
                user.Age, user.Email, user.MonthlyIncome,
                user.IsBlocked, user.BlockedUntil, user.Role
            });
        }

        [Authorize(Roles = "Accountant")]
        [HttpPut("{id}/block")]
        public async Task<IActionResult> BlockUser(int id, BlockUserDto dto)
        {
            if (!await _accountService.BlockUserAsync(id, dto.NumberOfDays))
                return NotFound();
            return Ok("User was blocked successfully.");
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}

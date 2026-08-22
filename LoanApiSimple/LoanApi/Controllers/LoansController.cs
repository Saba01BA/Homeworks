using LoanApi.Models.Dto;
using LoanApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyLoans()
        {
            return Ok(await _loanService.GetUserLoansAsync(GetUserId()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetUserLoanAsync(id, GetUserId());
            if (loan == null)
                return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanDto dto)
        {
            return Ok(await _loanService.CreateAsync(GetUserId(), dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanDto dto)
        {
            if (!await _loanService.UpdateAsync(id, GetUserId(), dto))
                return NotFound();
            return Ok(await _loanService.GetUserLoanAsync(id, GetUserId()));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _loanService.DeleteAsync(id, GetUserId()))
                return NotFound();
            return NoContent();
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}

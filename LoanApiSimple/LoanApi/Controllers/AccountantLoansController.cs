using LoanApi.Models.Dto;
using LoanApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("api/accountant/loans")]
    [Authorize(Roles = "Accountant")]
    public class AccountantLoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public AccountantLoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _loanService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
                return NotFound();
            return Ok(loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanDto dto)
        {
            if (!await _loanService.AccountantUpdateAsync(id, dto))
                return NotFound();
            return Ok(await _loanService.GetByIdAsync(id));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, UpdateLoanStatusDto dto)
        {
            if (!await _loanService.ChangeStatusAsync(id, dto.Status))
                return NotFound();
            return Ok(await _loanService.GetByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _loanService.AccountantDeleteAsync(id))
                return NotFound();
            return NoContent();
        }
    }
}

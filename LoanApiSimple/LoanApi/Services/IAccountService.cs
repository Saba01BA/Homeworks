using LoanApi.Models;
using LoanApi.Models.Dto;

namespace LoanApi.Services
{
    public interface IAccountService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<User?> GetByIdAsync(int id);
        Task<bool> BlockUserAsync(int userId, int numberOfDays);
    }
}

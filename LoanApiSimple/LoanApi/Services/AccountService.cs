using LoanApi.Data;
using LoanApi.Models;
using LoanApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AccountService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(user => user.UserName == dto.UserName))
                throw new InvalidOperationException("Username is already in use.");

            if (await _context.Users.AnyAsync(user => user.Email == dto.Email))
                throw new InvalidOperationException("Email is already in use.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Age = dto.Age,
                Email = dto.Email,
                MonthlyIncome = dto.MonthlyIncome,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreateResponse(user);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == dto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return CreateResponse(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> BlockUserAsync(int userId, int numberOfDays)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.IsBlocked = true;
            user.BlockedUntil = DateTime.UtcNow.AddDays(numberOfDays);
            await _context.SaveChangesAsync();
            return true;
        }

        private AuthResponseDto CreateResponse(User user)
        {
            return new AuthResponseDto
            {
                Token = _tokenService.CreateToken(user),
                UserId = user.Id,
                UserName = user.UserName,
                Role = user.Role
            };
        }
    }
}

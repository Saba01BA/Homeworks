using LoanApi.Data;
using LoanApi.Models;
using LoanApi.Models.Dto;
using LoanApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace LoanApi.Tests
{
    public class ServiceTests
    {
        [Fact]
        public async Task CreateLoan_StatusIsPending()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);

            var loan = await service.CreateAsync(user.Id, CreateDto());

            Assert.Equal(LoanStatus.Pending, loan.Status);
        }

        [Fact]
        public async Task BlockedUser_CannotCreateLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            user.IsBlocked = true;
            user.BlockedUntil = DateTime.UtcNow.AddDays(5);
            await context.SaveChangesAsync();
            var service = new LoanService(context);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateAsync(user.Id, CreateDto()));
        }

        [Fact]
        public async Task User_CannotUpdateApprovedLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            await service.ChangeStatusAsync(loan.Id, LoanStatus.Approved);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.UpdateAsync(loan.Id, user.Id, new UpdateLoanDto
                {
                    LoanType = LoanType.AutoLoan,
                    Amount = 5000,
                    Currency = Currency.GEL,
                    PeriodInMonths = 12
                }));
        }

        [Fact]
        public async Task ExpiredBlock_UserCanCreateLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            user.IsBlocked = true;
            user.BlockedUntil = DateTime.UtcNow.AddDays(-1);
            await context.SaveChangesAsync();
            var service = new LoanService(context);

            var loan = await service.CreateAsync(user.Id, CreateDto());

            Assert.NotNull(loan);
            Assert.False(user.IsBlocked);
            Assert.Null(user.BlockedUntil);
        }

        [Fact]
        public async Task User_CanUpdatePendingLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());

            var result = await service.UpdateAsync(
                loan.Id, user.Id, UpdateDto());

            Assert.True(result);
            Assert.Equal(LoanType.AutoLoan, loan.LoanType);
            Assert.Equal(5000, loan.Amount);
            Assert.Equal(12, loan.PeriodInMonths);
        }

        [Fact]
        public async Task User_CanDeletePendingLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());

            var result = await service.DeleteAsync(loan.Id, user.Id);

            Assert.True(result);
            Assert.Null(await context.Loans.FindAsync(loan.Id));
        }

        [Fact]
        public async Task User_CannotDeleteApprovedLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            await service.ChangeStatusAsync(loan.Id, LoanStatus.Approved);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.DeleteAsync(loan.Id, user.Id));
        }

        [Fact]
        public async Task User_CannotViewAnotherUsersLoan()
        {
            var context = CreateContext();
            var firstUser = await AddUserAsync(context, "one");
            var secondUser = await AddUserAsync(context, "two");
            var service = new LoanService(context);
            var loan = await service.CreateAsync(firstUser.Id, CreateDto());

            var result = await service.GetUserLoanAsync(
                loan.Id, secondUser.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task User_CannotUpdateAnotherUsersLoan()
        {
            var context = CreateContext();
            var firstUser = await AddUserAsync(context, "one");
            var secondUser = await AddUserAsync(context, "two");
            var service = new LoanService(context);
            var loan = await service.CreateAsync(firstUser.Id, CreateDto());

            var result = await service.UpdateAsync(
                loan.Id, secondUser.Id, UpdateDto());

            Assert.False(result);
        }

        [Fact]
        public async Task Accountant_CanChangeLoanStatus()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());

            var result = await service.ChangeStatusAsync(
                loan.Id, LoanStatus.Approved);

            Assert.True(result);
            Assert.Equal(LoanStatus.Approved, loan.Status);
        }

        [Fact]
        public async Task Accountant_CanUpdateApprovedLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            await service.ChangeStatusAsync(loan.Id, LoanStatus.Approved);

            var result = await service.AccountantUpdateAsync(
                loan.Id, UpdateDto());

            Assert.True(result);
            Assert.Equal(LoanType.AutoLoan, loan.LoanType);
            Assert.Equal(5000, loan.Amount);
        }

        [Fact]
        public async Task Accountant_CanDeleteApprovedLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            await service.ChangeStatusAsync(loan.Id, LoanStatus.Approved);

            var result = await service.AccountantDeleteAsync(loan.Id);

            Assert.True(result);
            Assert.Null(await context.Loans.FindAsync(loan.Id));
        }

        [Fact]
        public async Task Register_SavesUserWithHashedPasswordAndUserRole()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);

            var response = await service.RegisterAsync(RegisterDto());
            var user = await context.Users.SingleAsync();

            Assert.Equal("User", user.Role);
            Assert.NotEqual("password123", user.PasswordHash);
            Assert.True(BCrypt.Net.BCrypt.Verify("password123", user.PasswordHash));
            Assert.False(string.IsNullOrWhiteSpace(response.Token));
        }

        [Fact]
        public async Task Register_DuplicateUsernameThrowsException()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);
            await service.RegisterAsync(RegisterDto());
            var duplicate = RegisterDto();
            duplicate.Email = "different@example.com";

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.RegisterAsync(duplicate));
        }

        [Fact]
        public async Task Register_DuplicateEmailThrowsException()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);
            await service.RegisterAsync(RegisterDto());
            var duplicate = RegisterDto();
            duplicate.UserName = "differentuser";

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.RegisterAsync(duplicate));
        }

        [Fact]
        public async Task Login_CorrectPasswordReturnsToken()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);
            await service.RegisterAsync(RegisterDto());

            var response = await service.LoginAsync(new LoginDto
            {
                UserName = "testuser",
                Password = "password123"
            });

            Assert.NotNull(response);
            Assert.False(string.IsNullOrWhiteSpace(response.Token));
        }

        [Fact]
        public async Task Login_WrongPasswordReturnsNull()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);
            await service.RegisterAsync(RegisterDto());

            var response = await service.LoginAsync(new LoginDto
            {
                UserName = "testuser",
                Password = "wrong-password"
            });

            Assert.Null(response);
        }

        [Fact]
        public async Task GetById_ExistingUserReturnsUser()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = CreateAccountService(context);

            var result = await service.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task BlockUser_ExistingUserSetsBlockInformation()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = CreateAccountService(context);

            var result = await service.BlockUserAsync(user.Id, 5);

            Assert.True(result);
            Assert.True(user.IsBlocked);
            Assert.NotNull(user.BlockedUntil);
            Assert.True(user.BlockedUntil > DateTime.UtcNow);
        }

        [Fact]
        public async Task BlockUser_MissingUserReturnsFalse()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);

            var result = await service.BlockUserAsync(999, 5);

            Assert.False(result);
        }

        [Fact]
        public async Task Token_ContainsUserIdUsernameAndRoleClaims()
        {
            var context = CreateContext();
            var service = CreateAccountService(context);
            var response = await service.RegisterAsync(RegisterDto());

            var token = new JwtSecurityTokenHandler()
                .ReadJwtToken(response.Token);

            Assert.Contains(token.Claims, claim =>
                claim.Type == ClaimTypes.NameIdentifier && claim.Value == response.UserId.ToString());
            Assert.Contains(token.Claims, claim =>
                claim.Type == ClaimTypes.Name && claim.Value == "testuser");
            Assert.Contains(token.Claims, claim =>
                claim.Type == ClaimTypes.Role && claim.Value == "User");
        }

        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private async Task<User> AddUserAsync(
            AppDbContext context, string name = "test")
        {
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                UserName = $"{name}user",
                Age = 25,
                Email = $"{name}@example.com",
                MonthlyIncome = 2000,
                PasswordHash = "hash"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        private CreateLoanDto CreateDto()
        {
            return new CreateLoanDto
            {
                LoanType = LoanType.FastLoan,
                Amount = 1000,
                Currency = Currency.GEL,
                PeriodInMonths = 6
            };
        }

        private UpdateLoanDto UpdateDto()
        {
            return new UpdateLoanDto
            {
                LoanType = LoanType.AutoLoan,
                Amount = 5000,
                Currency = Currency.GEL,
                PeriodInMonths = 12
            };
        }

        private AccountService CreateAccountService(AppDbContext context)
        {
            return new AccountService(context, new TokenService(Configuration()));
        }

        private IConfiguration Configuration()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "test-secret-key-that-is-longer-than-32-characters",
                    ["Jwt:Issuer"] = "TestIssuer",
                    ["Jwt:Audience"] = "TestAudience"
                })
                .Build();
        }

        private RegisterDto RegisterDto()
        {
            return new RegisterDto
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "testuser",
                Age = 25,
                Email = "test@example.com",
                MonthlyIncome = 2000,
                Password = "password123"
            };
        }
    }
}

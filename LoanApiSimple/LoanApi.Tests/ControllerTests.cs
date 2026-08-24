using LoanApi.Controllers;
using LoanApi.Data;
using LoanApi.Models;
using LoanApi.Models.Dto;
using LoanApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace LoanApi.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task AccountController_RegisterReturnsOk()
        {
            var context = CreateContext();
            var controller = new AccountController(CreateAccountService(context));

            var result = await controller.Register(RegisterDto());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AccountController_InvalidLoginReturnsUnauthorized()
        {
            var context = CreateContext();
            var controller = new AccountController(CreateAccountService(context));

            var result = await controller.Login(new LoginDto
            {
                UserName = "missing",
                Password = "wrong"
            });

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task UsersController_GetMyInformationReturnsOk()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var controller = new UsersController(CreateAccountService(context));
            SetLoggedInUser(controller, user.Id, "User");

            var result = await controller.GetMyInformation();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UsersController_MissingUserReturnsNotFound()
        {
            var context = CreateContext();
            var controller = new UsersController(CreateAccountService(context));
            SetLoggedInUser(controller, 999, "User");

            var result = await controller.GetMyInformation();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UsersController_BlockExistingUserReturnsOk()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var controller = new UsersController(CreateAccountService(context));

            var result = await controller.BlockUser(
                user.Id, new BlockUserDto { NumberOfDays = 5 });

            Assert.IsType<OkObjectResult>(result);
            Assert.True(user.IsBlocked);
        }

        [Fact]
        public async Task LoansController_CreateReturnsOkAndUsesLoggedInUser()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var controller = new LoansController(new LoanService(context));
            SetLoggedInUser(controller, user.Id, "User");

            var result = await controller.Create(CreateDto());
            var savedLoan = await context.Loans.SingleAsync();

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user.Id, savedLoan.UserId);
        }

        [Fact]
        public async Task LoansController_GetMissingLoanReturnsNotFound()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var controller = new LoansController(new LoanService(context));
            SetLoggedInUser(controller, user.Id, "User");

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task LoansController_UpdateMissingLoanReturnsNotFound()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var controller = new LoansController(new LoanService(context));
            SetLoggedInUser(controller, user.Id, "User");

            var result = await controller.Update(999, UpdateDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task LoansController_DeletePendingLoanReturnsNoContent()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            var controller = new LoansController(service);
            SetLoggedInUser(controller, user.Id, "User");

            var result = await controller.Delete(loan.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task AccountantController_GetAllReturnsOk()
        {
            var context = CreateContext();
            var controller = new AccountantLoansController(new LoanService(context));

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AccountantController_ChangeStatusReturnsApprovedLoan()
        {
            var context = CreateContext();
            var user = await AddUserAsync(context);
            var service = new LoanService(context);
            var loan = await service.CreateAsync(user.Id, CreateDto());
            var controller = new AccountantLoansController(service);

            var result = await controller.ChangeStatus(
                loan.Id, new UpdateLoanStatusDto { Status = LoanStatus.Approved });

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(LoanStatus.Approved, loan.Status);
        }

        [Fact]
        public async Task AccountantController_DeleteMissingLoanReturnsNotFound()
        {
            var context = CreateContext();
            var controller = new AccountantLoansController(new LoanService(context));

            var result = await controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void LoansController_RequiresUserRole()
        {
            var attribute = typeof(LoansController)
                .GetCustomAttribute<AuthorizeAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("User", attribute.Roles);
        }

        [Fact]
        public void AccountantController_RequiresAccountantRole()
        {
            var attribute = typeof(AccountantLoansController)
                .GetCustomAttribute<AuthorizeAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("Accountant", attribute.Roles);
        }

        [Fact]
        public void BlockUserEndpoint_RequiresAccountantRole()
        {
            var method = typeof(UsersController).GetMethod("BlockUser");
            var attribute = method?.GetCustomAttribute<AuthorizeAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("Accountant", attribute.Roles);
        }

        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private async Task<User> AddUserAsync(AppDbContext context)
        {
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "testuser",
                Age = 25,
                Email = "test@example.com",
                MonthlyIncome = 2000,
                PasswordHash = "hash"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        private AccountService CreateAccountService(AppDbContext context)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "test-secret-key-that-is-longer-than-32-characters",
                    ["Jwt:Issuer"] = "TestIssuer",
                    ["Jwt:Audience"] = "TestAudience"
                })
                .Build();
            return new AccountService(context, new TokenService(configuration));
        }

        private void SetLoggedInUser(ControllerBase controller, int userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"))
                }
            };
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
    }
}

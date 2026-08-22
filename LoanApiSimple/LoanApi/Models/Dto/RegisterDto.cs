using System.ComponentModel.DataAnnotations;

namespace LoanApi.Models.Dto
{
    public class RegisterDto
    {
        [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(50)] public string UserName { get; set; } = string.Empty;
        [Range(18, 100)] public int Age { get; set; }
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Range(0, 1000000)] public decimal MonthlyIncome { get; set; }
        [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    }
}

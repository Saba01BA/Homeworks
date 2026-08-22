using System.ComponentModel.DataAnnotations;

namespace LoanApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Range(18, 100)]
        public int Age { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, 1000000)]
        public decimal MonthlyIncome { get; set; }

        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockedUntil { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
        public List<Loan> Loans { get; set; } = new();
    }
}

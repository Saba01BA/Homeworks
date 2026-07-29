using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.ModelValidations;

namespace WebApplication1.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [DateTimeValidation(ErrorMessage ="Date can't be in Future")]
        public DateTime CreateDate { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="First Name can not be over 50 Symbols")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Last Name can not be over 50 Symbols")]

        public string LastName { get; set; } = string.Empty;
        [Required]
        public string JobPosition { get; set; } = string.Empty;
        [Required]
        public double WorkExperience { get; set; }
        [Required]
        [Range(0.0,10000.0,ErrorMessage ="Salary can't be over 10000.0")]
        public Double Salary { get; set; }

        public Adress PersonAdress { get; set; } = new();
    }
}

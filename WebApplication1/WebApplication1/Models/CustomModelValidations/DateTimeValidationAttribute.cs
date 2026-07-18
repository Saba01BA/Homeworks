using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ModelValidations
{
    public class DateTimeValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime datetime)
            {
                if(datetime.Date > DateTime.Today)
                {
                    return new ValidationResult(errorMessage: "Date Can not be in Future");
                }
            }
            return ValidationResult.Success;
        }
    }
}

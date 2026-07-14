using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Models
{
    public class ValidAppointmentTimeAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? timeString = value as string;
            if (!TimeSpan.TryParse(timeString, out TimeSpan result))
                return new ValidationResult("Invalid Input");
            var minTimeSpan = new TimeSpan(10, 0, 0);
            var maxTimeSpan = new TimeSpan(19, 0, 0);
            if (result >= minTimeSpan && result <= maxTimeSpan)
                {
                return ValidationResult.Success;
                }
            else 
            {
                return new ValidationResult("Invalid Timespan");
            }
        }
    }
}

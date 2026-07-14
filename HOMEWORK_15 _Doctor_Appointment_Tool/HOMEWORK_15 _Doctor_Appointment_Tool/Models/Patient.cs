using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Models
{
    public class Patient
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}

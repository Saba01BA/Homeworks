using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Models
{
    public class Doctor
    {
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}

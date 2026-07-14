using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Models
{
    public class Appointment
    {
        public Patient Patient { get; set; } = new Patient();
        public Doctor Doctor { get; set; } = new Doctor();
        [ValidAppointmentTimeAttribute]
        public string Time { get; set; } = string.Empty;
    }
}

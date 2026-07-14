using HOMEWORK_15__Doctor_Appointment_Tool.Models;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Controllers
{
    public interface IAppointmentService
    {
        void Save(Appointment appointment);
        List<Appointment> Load();
    }
}

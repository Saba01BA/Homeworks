using HOMEWORK_15__Doctor_Appointment_Tool.Models;
using System.Text.Json;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Controllers
{
    public class AppointmentServiceJson : IAppointmentService
    {
        public List<Appointment> Load()
        {
            if (File.Exists("appointments.json"))
            {
                string json = File.ReadAllText("appointments.json");
                var list = JsonSerializer.Deserialize<List<Appointment>>(json);
                if (list == null)
                {
                    return new List<Appointment>();
                }    
                return list;
                
            }
            else
            {
                return new List<Appointment>();
            }

        }

        public void Save(Appointment appointment)
        {
            var list = Load();
            list.Add(appointment);
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText("appointments.json", json);
            
        }
    }
}

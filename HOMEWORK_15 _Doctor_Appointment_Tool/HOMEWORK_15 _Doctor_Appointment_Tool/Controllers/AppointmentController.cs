using HOMEWORK_15__Doctor_Appointment_Tool.Models;
using Microsoft.AspNetCore.Mvc;

namespace HOMEWORK_15__Doctor_Appointment_Tool.Controllers
{
    public class AppointmentController:Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(Appointment appointment)
        {
            if (!ModelState.IsValid) 
            {
               return View(appointment);
                
            }

            _appointmentService.Save(appointment);
            return RedirectToAction("ViewRecords");

        }

        [HttpGet]
        public IActionResult ViewRecords()
        {
            var list =  _appointmentService.Load();
            return View(list);
        }
    }
}

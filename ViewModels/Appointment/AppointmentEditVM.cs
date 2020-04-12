using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.Appointment
{
    public class AppointmentEditVM
    {
        public int AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentDay { get; set; }
        [Required]
        public DateTime AppointmentStart { get; set; }
        [Required]
        public DateTime AppointmentEnd { get; set; }

        [Required]
        public string Title { get; set; }
        public string ProcedureDescription { get; set; } 

        public List<AppointmentProcedure> AppointmentProcedures { get; set; }
    }
}

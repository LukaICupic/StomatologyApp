using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.Appointment
{
    public class AppointmentCreateVM
    {
        public AppointmentCreateVM()
        {
            //AppointmentProcedures = new List<AppointmentProcedure>();
        }
 
        [Required(ErrorMessage = "The starting time of the appointment must be specified")]
        public DateTime AppointmentStart { get; set; }
        [Required(ErrorMessage = "The ending time of the appointment must be specified")]
        public DateTime AppointmentEnd { get; set; }

        [Required(ErrorMessage ="Specify the title")]
        public string Title { get; set; }
        public string ProcedureDescription { get; set; }

        //public Models.AppointmentProcedure AppointmentProcedures { get; set; }

    }
}

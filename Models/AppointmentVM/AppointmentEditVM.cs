using StomatologyApp.Interfaces;
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
        public AppointmentEditVM()
        {
            PickedDentalProcedures = new List<Models.DentalProcedure>();

            UnPickedDentalProcedures = new List<Models.DentalProcedure>();
        }

        [Required]
        public DateTime AppointmentStart { get; set; }
        [Required]
        public DateTime AppointmentEnd { get; set; }

        [Required]
        public string Title { get; set; }
        public string ProcedureDescription { get; set; }

        public List<Models.DentalProcedure> PickedDentalProcedures { get; set; }

        public List<Models.DentalProcedure> UnPickedDentalProcedures { get; set; }

    }
}

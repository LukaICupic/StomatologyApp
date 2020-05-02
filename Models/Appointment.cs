using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class Appointment
    {
        public Appointment()
        {
            AppointmentProcedures = new List<AppointmentProcedure>();
        }

        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentStart { get; set; }
        [Required]
        public DateTime AppointmentEnd   { get; set; }

        [Required]
        public string Title { get; set; }
        public string ProcedureDescription { get; set; } 

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int WorkDaysId { get; set; }
        public WorkDays WorkDays { get; set; }

        public List<AppointmentProcedure> AppointmentProcedures { get; set; }

    }
}

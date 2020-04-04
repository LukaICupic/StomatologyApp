using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd   { get; set; }
        public bool AppointmentCanceled { get; set; }
        public string AppointmentProcedureDescription { get; set; } // možda će trebati nešto veće od stringa

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int WorkDaysId { get; set; }
        public WorkDays WorkDays { get; set; }

        public List<AppointmentProcedure> AppointmentProcedures { get; set; }

    }
}

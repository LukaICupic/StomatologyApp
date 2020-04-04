using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class WorkDays
    {
        [Key]
        public int WorkDaysId { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}

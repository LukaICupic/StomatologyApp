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

        [Required(ErrorMessage = "The starting hours and day of the working week must be specified")]
        public DateTime WorkStart { get; set; }
        [Required(ErrorMessage = "The ending hours and day of the working week must be specified")]
        public DateTime WorkEnd { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}

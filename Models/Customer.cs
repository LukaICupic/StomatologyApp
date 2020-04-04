using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
 
        public string Address { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        //public bool CameToAppointment { get; set; }


        public List<Appointment> Appointments { get; set; }
        public List<CustomerProcedure> CustomerProcedures { get; set; }


    }
}

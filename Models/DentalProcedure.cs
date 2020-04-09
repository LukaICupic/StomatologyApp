using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class DentalProcedure
    {
        [Key]
        public int DentalProcedureId { get; set; }
        [Required(ErrorMessage = "The name of the procedure must be specified")]
        public string ProcedureName { get; set; }
        [Required(ErrorMessage = "The price of the procedure must be specified")]
        public decimal ProcedurePrice { get; set; }

        public List<CustomerProcedure> CustomerProcedures { get; set; }
        public List<AppointmentProcedure> AppointmentProcedures { get; set; }

    }
}

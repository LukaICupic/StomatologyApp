using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class DentalProcedure
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DentalProcedureId { get; set; }
        [Required(ErrorMessage = "The name of the procedure must be specified")]
        public string ProcedureName { get; set; }
        [Required(ErrorMessage = "The price of the procedure must be specified")]
        public decimal ProcedurePrice { get; set; }
        public bool isEnabled { get; set; }

        public List<CustomerProcedure> CustomerProcedures { get; set; }
        public List<AppointmentProcedure> AppointmentProcedures { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.DentalProcedure
{
    public class DentalProcedureEditVM
    {
        public int DentalProcedureId { get; set; }
        [Required(ErrorMessage = " The name of the procedure must be specified")]
        public string ProcedureName { get; set; }
        [Required(ErrorMessage = "The price of the procedure must be specified")]
        public decimal ProcedurePrice { get; set; }
    }
}

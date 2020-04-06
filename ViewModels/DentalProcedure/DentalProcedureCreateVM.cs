using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.DentalProcedure
{
    public class DentalProcedureCreateVM
    {
        [Required]
        public string ProcedureName { get; set; }
        [Required]
        public decimal ProcedurePrice { get; set; }
    }
}

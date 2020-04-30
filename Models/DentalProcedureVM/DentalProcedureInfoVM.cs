using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models.DentalProcedureVM
{
    public class DentalProcedureInfoVM
    {    
        public int DentalProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public bool isSelected { get; set; }
    }
}

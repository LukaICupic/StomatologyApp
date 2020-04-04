using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class CustomerProcedure
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public int DentalProcedureId { get; set; }
        public DentalProcedure DentalProcedure { get; set; }
    }
}

using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public interface IDentalProcedure
    {
        DentalProcedure GetProcedure(int Id);

        IEnumerable<DentalProcedure> GetProcedures();

        DentalProcedure AddProcedure(DentalProcedure customer);

        DentalProcedure UpdateProcedure(DentalProcedure customer);

        DentalProcedure DeleteProcedure(int Id);
    }
}

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

        DentalProcedure AddProcedure(DentalProcedure procedure);

        DentalProcedure UpdateProcedure(DentalProcedure procedure);

        DentalProcedure DeleteProcedure(int Id);
    }
}

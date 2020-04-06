using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public class SQLDentalProcedureRepo : IDentalProcedure
    {
        private readonly AppDbContext context;

        public SQLDentalProcedureRepo(AppDbContext context)
        {
            this.context = context;
        }

        public DentalProcedure GetProcedure (int Id)
        {
            return context.DentalProcedures.Find(Id);
        }

        public IEnumerable<DentalProcedure> GetProcedures ()
        {
            return context.DentalProcedures;
        }

        public DentalProcedure AddProcedure(DentalProcedure procedure)
        {
            context.Add(procedure);
            context.SaveChanges();

            return procedure;
        }

       public DentalProcedure UpdateProcedure(DentalProcedure procedure)
        {
            var _procedure = context.DentalProcedures.Attach(procedure);
            _procedure.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return procedure;
        }

       public DentalProcedure DeleteProcedure(int Id)
        {
            var _procedure = context.DentalProcedures.Find(Id);

            if(_procedure != null) { 
                context.Remove(_procedure);
                context.SaveChanges();
                }

            return _procedure;
        }

    }
}

using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public class WorkDaysRepository : IWorkDayRepository
    {
        private readonly AppDbContext context;
        public WorkDaysRepository (AppDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<WorkDays> GetAllWorkWeeks()
        {
            return context.WorkDays;
        }

        public WorkDays GetWorkWeek (int Id)
        {
            var workWeek = context.WorkDays.Find(Id);

            return workWeek;

        }

        public WorkDays AddWorkWeek(WorkDays workDays)
        {
            context.WorkDays.Add(workDays);
            context.SaveChanges();
            return workDays;
        }

        public WorkDays UpdateWorkWeek (WorkDays workDays)
        {
            var workWeek = context.WorkDays.Attach(workDays);
            workWeek.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return workDays;
        }

        public WorkDays DeleteWorkWeek (int Id)
        {
            var workWeek = context.WorkDays.Find(Id);

            if (workWeek != null)
            {
                context.Remove(workWeek);
                context.SaveChanges();
            }

            return workWeek;
        }
    }
}

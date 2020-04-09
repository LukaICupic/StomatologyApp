using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public interface IWorkDay
    {
         IEnumerable<WorkDays> GetAllWorkWeeks();

         WorkDays GetWorkWeek(int Id);

         WorkDays AddWorkWeek(WorkDays workDays);

         WorkDays UpdateWorkWeek(WorkDays workDays);

        WorkDays DeleteWorkWeek(int Id);
         
    }
}

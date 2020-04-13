using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
   public interface IAppointment
    {
        IEnumerable<Appointment> GetAppointments();

        Appointment GetAppointment(int Id);

        Appointment CreateAppointment(Appointment appointment);

        Appointment UpdateAppointment(Appointment appointment);

        void AppointmentCanceled (Appointment appointment);

        Appointment DeleteAppointment(int Id);
    }
}

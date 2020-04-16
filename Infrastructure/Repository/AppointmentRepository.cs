using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext context;

        public AppointmentRepository (AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return context.Appointments;
        }

        public Appointment GetAppointment (int Id)
        {
            var appointment = context.Appointments.Find(Id);
            return appointment;
        }

        public Appointment CreateAppointment(Appointment app)
        {
            context.Appointments.Add(app);
            context.SaveChanges();
            return app;
        }

        public Appointment UpdateAppointment (Appointment app)
        {

            var appointment = context.Appointments.Attach(app);
            appointment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return app;
        }

        public void AppointmentCanceled(Appointment appointment)
        {
            var appCanceled = context.AppointmentProcedures.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);
            appCanceled.ProcedureAppointmentCanceled = true;
            context.SaveChanges();

            
        }

        public Appointment DeleteAppointment (int Id)
        {
            var app = context.Appointments.Find(Id);

            if(app != null)
            {
                context.Appointments.Remove(app);
                context.SaveChanges();
            }

            return app;
        }
    }
}

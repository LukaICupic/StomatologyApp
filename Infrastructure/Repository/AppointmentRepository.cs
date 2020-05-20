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
            return context.Appointments.OrderBy(a => a.AppointmentStart).ToList();
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
            var appProc = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);

            appProc.AppointmentCanceled = true;
            context.SaveChanges();

            
        }

        public void DeleteAppProc(int Id)
        {
            var appProc = context.AppointmentProcedures.Where(a => a.AppointmentId == Id).ToList();
            
                foreach (var app in appProc)
                {
                   context.Remove(app);
                }

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

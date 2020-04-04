using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<WorkDays> WorkDays { get; set; }

        public DbSet<DentalProcedure> DentalProcedures { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<DentalProcedure>()
                .Property(p => p.ProcedurePrice)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Appointment>()
               .HasOne(c => c.Customer)
               .WithMany(a => a.Appointments);

            modelBuilder.Entity<Appointment>()
               .HasOne(w => w.WorkDays)
               .WithMany(a => a.Appointments);



            modelBuilder.Entity<CustomerProcedure>()
                .HasKey(cp => new { cp.CustomerId, cp.DentalProcedureId });

            modelBuilder.Entity<CustomerProcedure>()
                .HasOne(c => c.Customer)
                .WithMany(cp => cp.CustomerProcedures)
                .HasForeignKey(cu => cu.CustomerId);

            modelBuilder.Entity<CustomerProcedure>()
               .HasOne(d => d.DentalProcedure)
               .WithMany(cp => cp.CustomerProcedures)
               .HasForeignKey(dp => dp.DentalProcedureId);



            modelBuilder.Entity<AppointmentProcedure>()
                .HasKey(ap => new { ap.AppointmentId, ap.DentalProcedureId });

            modelBuilder.Entity<AppointmentProcedure>()
                .HasOne(a => a.Appointment)
                .WithMany(cp => cp.AppointmentProcedures)
                .HasForeignKey(ap => ap.AppointmentId);

            modelBuilder.Entity<AppointmentProcedure>()
                .HasOne(d => d.DentalProcedure)
                .WithMany(cp => cp.AppointmentProcedures)
                .HasForeignKey(dp => dp.DentalProcedureId);

            modelBuilder.Entity<Customer>()
                .HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "Luka",
                    Address = "Miramarska 22",
                    TelephoneNumber = "0993132245"
                },
                 new Customer
                 {
                     CustomerId = 2,
                     Name = "Ana",
                     Address = "Miramarska 23",
                     TelephoneNumber = "0993132246"
                 },
                  new Customer
                  {
                      CustomerId = 3,
                      Name = "Vilihrast",
                      Address = "Miramarska 24",
                      TelephoneNumber = "0993131245"
                  });


        }
    }
}

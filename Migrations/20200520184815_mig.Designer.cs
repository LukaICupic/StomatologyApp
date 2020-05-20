﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StomatologyApp.Models;

namespace StomatologyApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200520184815_mig")]
    partial class mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StomatologyApp.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AppointmentCanceled");

                    b.Property<DateTime>("AppointmentEnd");

                    b.Property<DateTime>("AppointmentStart");

                    b.Property<int>("CustomerId");

                    b.Property<string>("ProcedureDescription");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("WorkDaysId");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("WorkDaysId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("StomatologyApp.Models.AppointmentProcedure", b =>
                {
                    b.Property<int>("AppointmentId");

                    b.Property<int>("DentalProcedureId");

                    b.HasKey("AppointmentId", "DentalProcedureId");

                    b.HasIndex("DentalProcedureId");

                    b.ToTable("AppointmentProcedures");
                });

            modelBuilder.Entity("StomatologyApp.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("TelephoneNumber")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "Miramarska 22",
                            Name = "Luka",
                            TelephoneNumber = "0993132245"
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "Miramarska 23",
                            Name = "Ana",
                            TelephoneNumber = "0993132246"
                        },
                        new
                        {
                            CustomerId = 3,
                            Address = "Miramarska 24",
                            Name = "Vilihrast",
                            TelephoneNumber = "0993131245"
                        });
                });

            modelBuilder.Entity("StomatologyApp.Models.CustomerProcedure", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("DentalProcedureId");

                    b.HasKey("CustomerId", "DentalProcedureId");

                    b.HasIndex("DentalProcedureId");

                    b.ToTable("CustomerProcedures");
                });

            modelBuilder.Entity("StomatologyApp.Models.DentalProcedure", b =>
                {
                    b.Property<int>("DentalProcedureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProcedureName")
                        .IsRequired();

                    b.Property<decimal>("ProcedurePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isEnabled");

                    b.HasKey("DentalProcedureId");

                    b.ToTable("DentalProcedures");
                });

            modelBuilder.Entity("StomatologyApp.Models.WorkDays", b =>
                {
                    b.Property<int>("WorkDaysId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("WorkWeekEnd");

                    b.Property<DateTime>("WorkWeekStart");

                    b.HasKey("WorkDaysId");

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("StomatologyApp.Models.Appointment", b =>
                {
                    b.HasOne("StomatologyApp.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StomatologyApp.Models.WorkDays", "WorkDays")
                        .WithMany("Appointments")
                        .HasForeignKey("WorkDaysId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StomatologyApp.Models.AppointmentProcedure", b =>
                {
                    b.HasOne("StomatologyApp.Models.Appointment", "Appointment")
                        .WithMany("AppointmentProcedures")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StomatologyApp.Models.DentalProcedure", "DentalProcedure")
                        .WithMany("AppointmentProcedures")
                        .HasForeignKey("DentalProcedureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StomatologyApp.Models.CustomerProcedure", b =>
                {
                    b.HasOne("StomatologyApp.Models.Customer", "Customer")
                        .WithMany("CustomerProcedures")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StomatologyApp.Models.DentalProcedure", "DentalProcedure")
                        .WithMany("CustomerProcedures")
                        .HasForeignKey("DentalProcedureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

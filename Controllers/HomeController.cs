﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.Models.AppointmentVM;
using StomatologyApp.ViewModels;
using StomatologyApp.ViewModels.Appointment;

namespace StomatologyApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAppointmentRepository _appointment;
        private readonly ICustomerRepository _customer;
        private readonly IWorkDayRepository _workDay;
        private readonly IDentalProcedureRepository _dentalProcedure;
        private readonly AppDbContext _context;

        public HomeController (IAppointmentRepository appointment, ICustomerRepository customer, IWorkDayRepository workDayRepository,
            IDentalProcedureRepository dentalProcedure, AppDbContext context)
        {
            _appointment = appointment;
            _customer = customer;
            _workDay = workDayRepository;
            _dentalProcedure = dentalProcedure;
            _context = context;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var model = _appointment.GetAppointments();

            return View(model);
        }

      

        [HttpGet]
        public ViewResult GetAppointment (int Id)
        {
            var model = _appointment.GetAppointment(Id);

            if (model == null)
            {
                ViewBag.ErrorMessage = "The appointment does not exist";
                return View("NotFound");
            }
            var customer = _customer.GetCustomer(model.CustomerId);

            if (model == null)
            {
                ViewBag.ErrorMessage = "The scheduled appointment can not be found";
                return View("NotFound");
            }

            AppointmentSeeVM appointment = new AppointmentSeeVM
            {
                Customer = customer,
                CustomerId = model.CustomerId,
                CustomerName = model.Customer.Name,
                AppointmentId = model.AppointmentId,
                AppointmentStart = model.AppointmentStart,
                AppointmentEnd = model.AppointmentEnd,
                Title = model.Title,
                ProcedureDescription = model.ProcedureDescription,
                AppointmentProcedures = model.AppointmentProcedures
            };
            return View(appointment);
        }

        [HttpGet]
        public IActionResult AddAppointment(int Id)
        {

            ViewBag.customerId = Id;

            if (Id == 0)
            {
                return View("NotFound");

            }

            AppointmentCreateVM model = new AppointmentCreateVM
            {
                DentalProcedures = _dentalProcedure.GetProcedures().ToList()
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult AddAppointment(AppointmentCreateVM model, int Id)
        {   

            if (ModelState.IsValid) 
            {
                

                if (Id != 0)
                {
                    if( model.AppointmentStart.Date != model.AppointmentEnd.Date)
                    {
                       
                         ViewBag.ErrorMessage = ("The dates must match for the appointment to be created");
                        ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                        return View();
                    }

                   if (model.AppointmentStart >= model.AppointmentEnd)
                    {
                        ViewBag.ErrorMessage = ("The starting time of the appointment was set the same as the ending time");
                        ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                        return View();
                    }

                    var workkWeek = _workDay.GetAllWorkWeeks()
                        .FirstOrDefault(w => w.WorkWeekStart <= model.AppointmentStart && w.WorkWeekEnd >= model.AppointmentEnd);


                    if (workkWeek == null || model.AppointmentEnd.Hour > workkWeek.WorkWeekEnd.Hour || model.AppointmentStart.Hour < workkWeek.WorkWeekStart.Hour)
                    {
                        ViewBag.ErrorMessage = ("The appointment can not be created for the time of that working week was either overstepped," +
                            " not defined or the appointment was set too early. Please check if you have created the working week.");
                        ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                        return View();
                    }

                    var customer = _customer.GetCustomer(Id);
                    var workWeek = _workDay.GetWorkWeek(workkWeek.WorkDaysId);

                    Appointment appointment = new Appointment
                    {
                        AppointmentStart = model.AppointmentStart,
                        AppointmentEnd = model.AppointmentEnd,
                        Title = model.Title,
                        ProcedureDescription = model.ProcedureDescription,
                        CustomerId = Id,
                        Customer = customer,
                        WorkDaysId =  workkWeek.WorkDaysId,
                        WorkDays = workWeek
                        };

                    foreach (var proc in model.DentalProcedures)
                    {

                        if (proc.isEnabled)
                        {

                            appointment.AppointmentProcedures.Add(new AppointmentProcedure { 
                                Appointment = appointment,
                                DentalProcedure = _context.DentalProcedures.FirstOrDefault(d => d.DentalProcedureId == proc.DentalProcedureId),
                                ProcedureAppointmentCanceled = false
                            });
                        }
                    }



                    _appointment.CreateAppointment(appointment);
                    return RedirectToAction("Index");
                        //, new { Id = appointment.AppointmentId });
                }

                else
                {
                    ViewBag.ErrorMessage = "User could not be found, so the appointment could not be created";
                    return View("NotFound");
                }
            }

                return View();
        }

        


        [HttpGet]
        public ViewResult EditAppointment(int? Id)
        {
            var appointment = _appointment.GetAppointment(Id.Value);

            if (appointment == null)
            {
                ViewBag.Title = "The appointment does not exist.";
                return View("NotFound");
            }

            AppointmentEditVM model = new AppointmentEditVM
            {
                AppointmentStart = appointment.AppointmentStart,
                AppointmentEnd = appointment.AppointmentEnd,
                Title = appointment.Title,
                ProcedureDescription = appointment.ProcedureDescription,
                AppointmentProcedures = appointment.AppointmentProcedures,
                Customer = appointment.Customer //samo jedan pristiže

            };

            return View(model);

         }


        [HttpPost]
        public IActionResult AppointmentCanceled(Appointment model)
        {
            if (ModelState.IsValid)
            {
                var appointment = _appointment.GetAppointment(model.AppointmentId);

                if(appointment == null)
                {
                    ViewBag.Title = "The appointment does not exist.";
                    return View("NotFound");
                }

                _appointment.AppointmentCanceled(appointment);

                //_appointment.DeleteAppointment(appointment.AppointmentId);

                //ne obrisati već postaviti kao obrisano s "ProcedureCanceled" boolom u AppointmentProcedures
                //specifično ih prikazati i označiti u GetAppointments
                return View("GetAppointments");
            }

            return View(model);

            //napraviti ili da je dani appointment u "disabled" ili da se obrište potpuno.
            //(Pitanje je onda hoće li ostati u tavlici AppointmentProceduress radi izvještaja). 
        }
    }
}
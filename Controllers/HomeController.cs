using System;
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
        private readonly IWorkDayRepository _workDayRepository;
        private readonly IDentalProcedureRepository _dentalProcedure;

        public HomeController (IAppointmentRepository appointment, ICustomerRepository customer, IWorkDayRepository workDayRepository,
            IDentalProcedureRepository dentalProcedure)
        {
            _appointment = appointment;
            _customer = customer;
            _workDayRepository = workDayRepository;
            _dentalProcedure = dentalProcedure;
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
                Customer = model.Customer,
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
            AppointmentCreateVM model = new AppointmentCreateVM();
            return View(model);


            //unos DentalProcedura radi biranja postupaka koristeći AppointmentProcedures
            //(već zakomentirana sekcija u AddAppointment.cshtml)


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
                         return View();
                    }

                   if (model.AppointmentStart >= model.AppointmentEnd)
                    {
                        ViewBag.ErrorMessage = ("The starting time of the appointment was set the same as the ending time");
                        return View();
                    }

                    var workkWeek = _workDayRepository.GetAllWorkWeeks()
                        .FirstOrDefault(w => w.WorkWeekStart <= model.AppointmentStart && w.WorkWeekEnd >= model.AppointmentEnd);


                    if (workkWeek == null)
                    {
                        ViewBag.ErrorMessage = ("The appointment can not be created for the time of that working week was either overstepped," +
                            " not defined or the appointment was set too early. Please check if you have created the working week");
                        return View();
                    }


                    Appointment appointment = new Appointment
                    {
                        AppointmentStart = model.AppointmentStart,
                        AppointmentEnd = model.AppointmentEnd,
                        Title = model.Title,
                        ProcedureDescription = model.ProcedureDescription,
                        CustomerId = Id,
                        WorkDaysId =  workkWeek.WorkDaysId                   
                        };

                    _appointment.CreateAppointment(appointment);
                    return RedirectToAction("GetAppointment", new { Id = appointment.AppointmentId });
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
        public IActionResult AppointmentCanceled(AppointmentEditVM model)
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
                _appointment.DeleteAppointment(appointment.AppointmentId);
            }

            return View(model);

            //napraviti ili da je dani appointment u "disabled" ili da se obrište potpuno.
            //(Pitanje je onda hoće li ostati u tavlici AppointmentProceduress radi izvještaja). 
        }
    }
}
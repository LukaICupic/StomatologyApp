using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.ViewModels;
using StomatologyApp.ViewModels.Appointment;

namespace StomatologyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppointmentRepository _appointment;
        private readonly ICustomerRepository _customer;

        public HomeController (IAppointmentRepository appointment, ICustomerRepository customer)
        {
            _appointment = appointment;
            _customer = customer;
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
                ViewBag.ErrorMessage = "The scheduled appointment can not be found";
                return View("NotFound");
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AddAppointment()
        {
            AppointmentCreateVM model = new AppointmentCreateVM();

            var customer = _customer.GetCustomers();

            model.Customer = new List<Customer>(customer);

            return View(model);

            //napraviti "search za ovo u sklopu viewa"
            //jquery autocomplete
        }


        //[HttpPost]
        //public IActionResult AddAppointment(AppointmentCreateVM model)
        //{

        //}

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
                AppointmentDay = appointment.AppointmentDay,
                AppointmentStart = appointment.AppointmentStart,
                AppointmentEnd = appointment.AppointmentEnd,
                Title = appointment.Title,
                ProcedureDescription = appointment.ProcedureDescription,
                AppointmentProcedures = appointment.AppointmentProcedures,
                Customer = appointment.Customer //samo jedan pristiže

            };

            return View(model);

         }

        //[HttpPost]
        //public ViewResult EditAppointment(AppointmentEditVM model)
        //{ }


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
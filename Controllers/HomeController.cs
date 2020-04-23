using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                AppointmentDay = model.AppointmentDay,
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

            //ujedno poslati WorkWeek radi provjere (da li se appointment
            //poklapa s radnim vremenom)

            //popraviti AppointmentDay, AppointmentStart, AppointmentEnd 
            //odnosno ograničiti pr isamo na datum, druga da direktno povezati na vrijeme tog datuma
            //(automatski preuzimaju odabrani datum). Odnosno da već tako i POHRANJUJE (WorkDayController).

            //unos DentalProcedura radi biranja postupaka koristeći AppointmentProcedures
            //(već zakomentirana sekcija u AddAppointment.cshtml)

            //ujedno popraviti generalne probleme (ViewBag prikaz nakon dohvaćanja NotFound viewa).

            


        }

        [HttpPost]
        public IActionResult AddAppointment(AppointmentCreateVM model, int Id)
        {

            //vratiti (ModelState.valid) jer trenutačno puca zato što vrijeme nije dobro namješteno

            //Workdays Id dolazi iz WorkDays objekta koji će se dohvatiti

            if (Id != 0)
                {

                    Appointment appointment = new Appointment
                    {
                        AppointmentDay = model.AppointmentDay,
                        AppointmentStart = model.AppointmentStart,
                        AppointmentEnd = model.AppointmentEnd,
                        Title = model.Title,
                        ProcedureDescription = model.ProcedureDescription,
                        CustomerId = Id,
                        WorkDaysId = 23 //hardkodirano
                    };

                    _appointment.CreateAppointment(appointment);
                    return RedirectToAction("GetAppointment", new { Id = appointment.AppointmentId });
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
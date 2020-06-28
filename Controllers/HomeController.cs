using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController (IAppointmentRepository appointment, ICustomerRepository customer, IWorkDayRepository workDayRepository,
            IDentalProcedureRepository dentalProcedure, AppDbContext context, ILogger<HomeController> logger)
        {
            _appointment = appointment;
            _customer = customer;
            _workDay = workDayRepository;
            _dentalProcedure = dentalProcedure;
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult FinishedAppointmentsReport()
        {

            var appointmentProcedure = _context.AppointmentProcedures.Where(a => a.Appointment.AppointmentCanceled == false).ToList();

            using (MemoryStream stream = new MemoryStream())
            {

                StreamWriter streamWriter = new StreamWriter(stream);


                foreach (var appProc in appointmentProcedure)
                {
                    var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == appProc.AppointmentId);
                    var procedure = _context.DentalProcedures.FirstOrDefault(p => p.DentalProcedureId == appProc.DentalProcedureId);


                    streamWriter.Write("Title:" + " " + appointment.Title + "\r\n" +
                        "Appointment start:" + " " + appointment.AppointmentStart + "\r\n" +
                        "Appointment end:" + " " + appointment.AppointmentEnd + "\r\n" +
                        "Procedure:" + " " + procedure.ProcedureName + "\r\n" + "\r\n");

                }

                streamWriter.Flush();
                streamWriter.Close();
                return File(stream.ToArray(), "text/plain", "Done_treatments.txt");

            }

        }

        [HttpGet]
        public IActionResult CanceledAppointmentsReport()
        {

            var appointmentProcedure = _context.AppointmentProcedures.Where(a => a.Appointment.AppointmentCanceled == true).ToList();

            using (MemoryStream stream = new MemoryStream())
            {

                StreamWriter streamWriter = new StreamWriter(stream);


                foreach (var appProc in appointmentProcedure)
                {
                    var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == appProc.AppointmentId);
                    var procedure = _context.DentalProcedures.FirstOrDefault(p => p.DentalProcedureId == appProc.DentalProcedureId);


                    streamWriter.Write("Title:" + " " + appointment.Title + "\r\n" +
                        "Appointment start:" + " " + appointment.AppointmentStart + "\r\n" +
                        "Appointment end:" + " " + appointment.AppointmentEnd + "\r\n" +
                        "Procedure:" + " " + procedure.ProcedureName + "\r\n" + "\r\n");

                }

                streamWriter.Flush();
                streamWriter.Close();
                return File(stream.ToArray(), "text/plain", "Canceled_treatments.txt");
            }

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

            ViewBag.customereId = Id;

            if (Id == 0)
            {
                return View("NotFound");

            }

            AppointmentCreateVM model = new AppointmentCreateVM
            {
                DentalProcedures = _dentalProcedure.GetProcedures().ToList()
            };

            _logger.LogInformation("A new appointment is being created with the title: " + model.Title + "\r\n" +
                                    "and the starting time at: " + model.AppointmentStart.Date);

            return View(model);

        }

        [HttpPost]
        public IActionResult AddAppointment(AppointmentCreateVM model, int Id)
        {   

            if (ModelState.IsValid) 
            {
                try
                {

                    if (Id != 0)
                    {
                        if (model.AppointmentStart.Date != model.AppointmentEnd.Date)
                        {

                            ViewBag.ErrorMessage = ("The dates must match for the appointment to be created");
                            ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                            _logger.LogError("The dates must match for the appointment to be created");

                            return View(model);
                        }

                        if (model.AppointmentStart >= model.AppointmentEnd)
                        {
                            ViewBag.ErrorMessage = ("The starting time of the appointment was either set the same as the ending time or it was overstepped");
                            ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                            _logger.LogError("The starting time of the appointment was either set the same as the ending time or it was overstepped");
                            return View(model);
                        }

                        var workkWeek = _workDay.GetAllWorkWeeks()
                            .FirstOrDefault(w => w.WorkWeekStart <= model.AppointmentStart && w.WorkWeekEnd >= model.AppointmentEnd);


                        if (workkWeek == null || model.AppointmentEnd.Hour > workkWeek.WorkWeekEnd.Hour || model.AppointmentStart.Hour < workkWeek.WorkWeekStart.Hour)
                        {
                            ViewBag.ErrorMessage = ("The appointment can not be created for the time of that working week was either overstepped," +
                                " not defined or the appointment was set too early. Please check if you have created the working week.");
                            ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                            _logger.LogError("The appointment can not be created for the time of that working week was either overstepped," +
                                " not defined or the appointment was set too early.");
                            return View(model);
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
                            WorkDaysId = workkWeek.WorkDaysId,
                            WorkDays = workWeek
                        };



                        foreach (var proc in model.DentalProcedures)
                        {

                            if (proc.isEnabled)
                            {

                                appointment.AppointmentProcedures.Add(new AppointmentProcedure
                                {
                                    Appointment = appointment,
                                    DentalProcedure = _context.DentalProcedures.FirstOrDefault(d => d.DentalProcedureId == proc.DentalProcedureId),
                                });
                            }
                        }

                        if (appointment.AppointmentProcedures.Count == 0)
                        {
                            ViewBag.ErrorMessage = ("You need to pick at least one procedure if you wish to save the appointment.");
                            return View(model);
                        }


                        _appointment.CreateAppointment(appointment);
                        _logger.LogWarning($"Appoitnemnt {appointment.Title} was successfully created");
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        ViewBag.ErrorMessage = "User could not be found, so the appointment could not be created";
                        _logger.LogError($"Appointment could not be created because the user could not be found");
                        return View("NotFound");
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogError($"Exception message: {ex.Message}" + "\r\n" + "\r\n" +
                        $"Inner exception: {ex.InnerException}");
                    return View(model);
                }
            }

                return View(model);
        }

        


        [HttpGet]
        public ViewResult EditAppointment(int? Id) 
        {
            ViewBag.appointmentId = Id;
            AppointmentEditVM model = new AppointmentEditVM();

            var appointment = _appointment.GetAppointment(Id.Value); //ne sprema Customera niti WorkDays objekt. Vjer do SQLa

            if (appointment == null)
            {
                ViewBag.Title = "The appointment does not exist.";
                return View("NotFound");
            }

            var appointmentProcedures = _context.AppointmentProcedures
                .Where(ap => ap.AppointmentId == appointment.AppointmentId).ToList();

            var allDentalProcedures = _dentalProcedure.GetProcedures().ToList();


            foreach (var appProcedure in appointmentProcedures)
            {
                foreach (var allProcedures in allDentalProcedures)
                {
                  
                    if (appProcedure.DentalProcedureId == allProcedures.DentalProcedureId)
                    {
                            allProcedures.isEnabled = true;
                            model.PickedDentalProcedures.Add(allProcedures);
                            break;

                    }

                }

            }

            foreach(var appProc in allDentalProcedures)
            {
                if (!model.PickedDentalProcedures.Contains(appProc))
                {
                    appProc.isEnabled = false;
                    model.UnPickedDentalProcedures.Add(appProc);
                }

            }     


            model.AppointmentStart = appointment.AppointmentStart;
            model.AppointmentEnd = appointment.AppointmentEnd;
            model.Title = appointment.Title;
            model.ProcedureDescription = appointment.ProcedureDescription;
            
        

            return View(model);

         }


        [HttpPost]
        public IActionResult EditAppointment(AppointmentEditVM model, int Id)
        {
            if (ModelState.IsValid)
            {
                var appointment = _appointment.GetAppointment(Id);

                if (appointment == null)
                {
                    ViewBag.Title = "The appointment does not exist.";
                    return View("NotFound");
                }

                if (model.AppointmentStart.Date != model.AppointmentEnd.Date)
                {

                    ViewBag.ErrorMessage = ("The dates must match for the appointment to be created");
                    ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                    return View(model);
                }

                if (model.AppointmentStart >= model.AppointmentEnd)
                {
                    ViewBag.ErrorMessage = ("The starting time of the appointment was either set the same as the ending time or it was overstepped");
                    ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                    return View(model);
                }

                var workkWeek = _workDay.GetAllWorkWeeks()
                    .FirstOrDefault(w => w.WorkWeekStart <= model.AppointmentStart && w.WorkWeekEnd >= model.AppointmentEnd);


                if (workkWeek == null || model.AppointmentEnd.Hour > workkWeek.WorkWeekEnd.Hour || model.AppointmentStart.Hour < workkWeek.WorkWeekStart.Hour)
                {
                    ViewBag.ErrorMessage = ("The appointment can not be created for the time of that working week was either overstepped," +
                        " not defined or the appointment was set too early. Please check if you have created the working week.");
                    ViewBag.Message = ("You have inserted the following dates and time: " + model.AppointmentStart + " - " + model.AppointmentEnd);
                    return View(model);
                }


                appointment.AppointmentStart = model.AppointmentStart;
                appointment.AppointmentEnd = model.AppointmentEnd;
                appointment.Title = model.Title;
                appointment.ProcedureDescription = model.ProcedureDescription;
                appointment.Customer = appointment.Customer;
                appointment.CustomerId = appointment.CustomerId;
                appointment.WorkDays = workkWeek;
                appointment.WorkDaysId = workkWeek.WorkDaysId;

                _appointment.DeleteAppProc(appointment.AppointmentId);


                foreach (var proc in model.PickedDentalProcedures)
                {

                    if (proc.isEnabled)
                    {

                        appointment.AppointmentProcedures.Add(new AppointmentProcedure
                        {
                            Appointment = appointment,
                            DentalProcedure = _context.DentalProcedures.FirstOrDefault(d => d.DentalProcedureId == proc.DentalProcedureId),
                        });
                    }

                }

                foreach (var _proc in model.UnPickedDentalProcedures)
                {

                    if (_proc.isEnabled)
                    {

                        appointment.AppointmentProcedures.Add(new AppointmentProcedure
                        {
                            Appointment = appointment,
                            DentalProcedure = _context.DentalProcedures.FirstOrDefault(d => d.DentalProcedureId == _proc.DentalProcedureId),
                        });
                    }

                }

                if (appointment.AppointmentProcedures.Count == 0)
                {
                    ViewBag.ErrorMessage = "At least one procedure must be picked!";
                    _logger.LogWarning($"Not a single procedure has been picked in the {model.Title} appointment");
                    return View(model);

                }

                _appointment.UpdateAppointment(appointment);
                return RedirectToAction("Index");

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AppointmentCanceled (int Id)
        {
            if (ModelState.IsValid)
            {
                var appointment = _appointment.GetAppointment(Id);

                if(appointment == null)
                {
                    ViewBag.Title = "The appointment does not exist.";
                    return View("NotFound");
                }

                _appointment.AppointmentCanceled(appointment);

                //_appointment.DeleteAppointment(appointment.AppointmentId);

                //ne obrisati već postaviti kao obrisano s "ProcedureCanceled" boolom u AppointmentProcedures
                //specifično ih prikazati i označiti u GetAppointments
                return RedirectToAction("Index");
            }

            return View();

            //napraviti ili da je dani appointment u "disabled" ili da se obrište potpuno.
            //(Pitanje je onda hoće li ostati u tavlici AppointmentProceduress radi izvještaja). 
        }
    }
}
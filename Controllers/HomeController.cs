using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.ViewModels.Appointment;

namespace StomatologyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppointment Iappointment;

        public HomeController (IAppointment appointment)
        {
            Iappointment = appointment;
        }

        public ViewResult Index()
        {
            var model = Iappointment.GetAppointments();

            return View(model);
        }

        public ViewResult GetAppointment (int Id)
        {
            var model = Iappointment.GetAppointment(Id);

            if (model == null)
            {
                ViewBag.ErrorMessage = "The scheduled appointment can not be found";
                return View("NotFound");
            }

            return View(model);
        }
       


        //[HttpPost]
        //public IActionResult AppointmentCanceled(Appointment model)
        //{
        //    dohvatiti dani appointment
        //    svrstati podatke u AppointmentEditVM
        //    pristupiti modelu dohvaćenom, preko njega ProcedureAppointmentCanceled polju u AppointmentProcedure i staviti ga "true"
        //    spremiti podatke i vratiti listu appointmenta
        //}
    }
}
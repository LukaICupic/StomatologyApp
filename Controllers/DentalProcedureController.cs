using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.ViewModels.DentalProcedure;

namespace StomatologyApp.Controllers
{
    public class DentalProcedureController : Controller
    {
        private readonly IDentalProcedure dentalProcedure;

        public DentalProcedureController(IDentalProcedure dentalProcedure)
        {
            this.dentalProcedure = dentalProcedure;
        }

        [HttpGet]
        public ViewResult GetProcedures()
        {
            var procedures = dentalProcedure.GetProcedures();

            return View(procedures);
        }

        [HttpGet]
        public ViewResult GetProcedure (int Id)
        {
            var procedure = dentalProcedure.GetProcedure(Id);
            return View(procedure);
        }

        [HttpGet]
        public ViewResult EditProcedure (int? Id)
        {
            var procedure = dentalProcedure.GetProcedure(Id.Value);

            if(procedure == null)
            {
                ViewBag.ErrorMessage = "The dental procedure does not exist";
                return View("NotFound");
            }

            DentalProcedureEditVM _procedure = new DentalProcedureEditVM
            {
                DentalProcedureId = procedure.DentalProcedureId,
                ProcedureName = procedure.ProcedureName,
                ProcedurePrice = procedure.ProcedurePrice
            };

            return View(_procedure);
        }

        [HttpPost]
        public IActionResult EditProcedure(DentalProcedureEditVM model)
        {
            if (ModelState.IsValid)
            {
                var procedure = dentalProcedure.GetProcedure(model.DentalProcedureId);

                if (procedure == null)
                {
                    //ViewBag.ErrorMessage = "The procedure does not exist";
                    return View("NotFound");

                }

                procedure.ProcedureName = model.ProcedureName;
                procedure.ProcedurePrice = model.ProcedurePrice;

                dentalProcedure.UpdateProcedure(procedure);

                return RedirectToAction("GetProcedures");

            }

            return View(model);
        }

        [HttpGet]
        public ViewResult CreateProcedure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProcedure(DentalProcedureCreateVM model)
        {


            if (ModelState.IsValid)
            {
                DentalProcedure procedure = new DentalProcedure
                {
                    ProcedureName = model.ProcedureName,
                    ProcedurePrice = model.ProcedurePrice
                };

                dentalProcedure.AddProcedure(procedure);
                return RedirectToAction("GetProcedures");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProcedure (int Id)
        {
            var procedure = dentalProcedure.GetProcedure(Id);

            if(procedure == null)
            {
                ViewBag.ErrorMessage = "The procedure does not exist";
                return View("NotFound");

            }

            dentalProcedure.DeleteProcedure(Id);
            return RedirectToAction("GetProcedures");

        }

    }
}
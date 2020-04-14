using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.ViewModels;
using StomatologyApp.ViewModels.WorkDays;

namespace StomatologyApp.Controllers
{
    public class WorkDayController : Controller
    {
        private readonly IWorkDay workDay;

        public WorkDayController (IWorkDay workDay)
        {
            this.workDay = workDay;
        }

        [HttpGet]
        public ViewResult GetAllWorkWeeks()
        {
            var workWeek = workDay.GetAllWorkWeeks();

            return View(workWeek);
        }

        [HttpGet]
        public ViewResult GetWorkWeek(int Id)
        {
            var workWeek = workDay.GetWorkWeek(Id);

            return View(workWeek);
        }

        [HttpGet]
        public ViewResult EditWorkWeek(int? Id)
        {
            var workWeek = workDay.GetWorkWeek(Id.Value);

            if (workWeek == null)
            {
                ViewBag.ErrorMessage = "The work week is not created";
                return View("NotFound");
            }

            WorkWeekEditVM _workWeek = new WorkWeekEditVM
            {
                WorkDaysId = workWeek.WorkDaysId,
                WorkWeekStart = workWeek.WorkWeekStart,
                WorkWeekEnd = workWeek.WorkWeekEnd,
                WorkStart = workWeek.WorkStart,
                WorkEnd = workWeek.WorkEnd
            };

            return View(_workWeek);
        }

        [HttpPost]
        public IActionResult EditWorkWeek(WorkWeekEditVM model)
        {
            if (ModelState.IsValid)
            {
                var workWeek = workDay.GetWorkWeek(model.WorkDaysId);

                if (workWeek == null)
                {
                    return View("NotFound");

                }

                workWeek.WorkWeekStart = model.WorkWeekStart;
                workWeek.WorkWeekEnd = model.WorkWeekEnd;
                workWeek.WorkStart = model.WorkStart;
                workWeek.WorkEnd = model.WorkEnd;

                workDay.UpdateWorkWeek(workWeek);

                return RedirectToAction("GetAllWorkWeeks");

            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AddWorkWeek()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWorkWeek(WorkWeekCreateVM model)
        {


            if (ModelState.IsValid)
            {
                WorkDays workWeek = new WorkDays
                {
                    WorkWeekStart = model.WorkWeekStart,
                    WorkWeekEnd = model.WorkWeekEnd,
                    WorkStart = model.WorkStart,
                    WorkEnd = model.WorkEnd
                };

                workDay.AddWorkWeek(workWeek);
                return RedirectToAction("GetAllWorkWeeks");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteWorkWeek(int Id)
        {
            var workWeek = workDay.GetWorkWeek(Id);

            if (workWeek == null)
            {
                ViewBag.ErrorMessage = "The work week is not created";
                return View("NotFound");

            }

            workDay.DeleteWorkWeek(Id);
            return RedirectToAction("GetAllWorkWeeks");

        }
    }
}
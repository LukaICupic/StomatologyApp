using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;
using StomatologyApp.ViewModels;
using StomatologyApp.ViewModels.WorkDays;

namespace StomatologyApp.Controllers
{
    [Authorize]
    public class WorkDayController : Controller
    {
        private readonly IWorkDayRepository _workDay;

        public WorkDayController (IWorkDayRepository workDay)
        {
            _workDay = workDay;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult GetAllWorkWeeks()
        {
            var workWeek = _workDay.GetAllWorkWeeks();

            return View(workWeek);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult GetWorkWeek(int Id)
        {
            var workWeek = _workDay.GetWorkWeek(Id);

            return View(workWeek);
        }

        [HttpGet]
        public ViewResult EditWorkWeek(int? Id)
        {
            var workWeek = _workDay.GetWorkWeek(Id.Value);

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
            };

            return View(_workWeek);
        }

        [HttpPost]
        public IActionResult EditWorkWeek(WorkWeekEditVM model)
        {
            if (ModelState.IsValid)
            {
                var workWeek = _workDay.GetWorkWeek(model.WorkDaysId);

                if (workWeek == null)
                {
                    return View("NotFound");

                }

                workWeek.WorkWeekStart = model.WorkWeekStart;
                workWeek.WorkWeekEnd = model.WorkWeekEnd;

                _workDay.UpdateWorkWeek(workWeek);

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
                };

                _workDay.AddWorkWeek(workWeek);
                return RedirectToAction("GetAllWorkWeeks");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteWorkWeek(int Id)
        {
            var workWeek = _workDay.GetWorkWeek(Id);

            if (workWeek == null)
            {
                ViewBag.ErrorMessage = "The work week is not created";
                return View("NotFound");

            }

            _workDay.DeleteWorkWeek(Id);
            return RedirectToAction("GetAllWorkWeeks");

        }
    }
}
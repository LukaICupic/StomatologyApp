using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLog.Targets;
using StomatologyApp.Models;
using StomatologyApp.ViewModels.Customer;

namespace StomatologyApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public CustomerController(ICustomerRepository customerRepository, IHostingEnvironment hostingEnvironment)
        {
            _customerRepository = customerRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();

            return View(customers);
        }

        [HttpGet] 
        public IActionResult GetCustomer(int Id)
        {
            var customer = _customerRepository.GetCustomer(Id);

            if(customer == null)
            {
                ViewBag.ErrorMessage = "The customer does not exist";
                return View("NotFound");
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult SearchForCustomer (string searchName)
        {
            var customers = _customerRepository.SearchForCustomer(searchName);

            if (customers == null)
            {
                ViewBag.ErrorMessage = "The customer you were looking for does not exist";
                return View("NotFound");
            }

            return View("FoundCustomers", customers);            
        }


        [HttpGet]
        public ViewResult EditCustomer (int? Id)
        {
            var customer = _customerRepository.GetCustomer(Id.Value);

            if (customer == null)
            {
                ViewBag.ErrorMessage = "The user does not exist any longer";
                return View("NotFound");
            }

            CustomerEditVM _customer = new CustomerEditVM()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Address = customer.Address,
                TelephoneNumber = customer.TelephoneNumber
            };

            return View(_customer);
        }

        [HttpPost]
        public IActionResult EditCustomer (CustomerEditVM model)
        {
            if (ModelState.IsValid)
            {

                var customer = _customerRepository.GetCustomer(model.CustomerId);

                if (customer == null)
                {
                    ViewBag.ErrorMessage = "The customer does not exist.";
                    return View("NotFound");
                }

                customer.Name = model.Name;
                customer.TelephoneNumber = model.TelephoneNumber;
                customer.Address = model.Address;

                _customerRepository.UpdateCustomer(customer);
                return RedirectToAction("GetCustomers");

            }

            return View(model);
           
        }

        [HttpGet]
        public ViewResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer (CustomerCreateVM customer)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if(customer.Photo != null)
                {
                    string folder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.Photo.FileName;
                    string pathFile = Path.Combine(folder, uniqueFileName);

                    customer.Photo.CopyTo(new FileStream(pathFile, FileMode.Create));
                }

                Customer _customer = new Customer
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    TelephoneNumber = customer.TelephoneNumber,
                    PhotoPath = uniqueFileName
                };

                _customerRepository.AddCustomer(_customer);
                return RedirectToAction("GetCustomers");
            }

            return View(customer); 
        }


        [HttpPost]
        public  IActionResult DeleteCustomer(int Id)
        {
            var _customer = _customerRepository.GetCustomer(Id);

            if(_customer == null) 
            {
                ViewBag.ErrorMessage = "The customer does not exist.";
                return View("NotFound");
            }

            var customer = _customerRepository.DeleteCustomer(Id);
            return RedirectToAction("GetCustomers");

            
        }

    }
}
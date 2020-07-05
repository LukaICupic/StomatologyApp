using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Models;
using StomatologyApp.ViewModels.Customer;

namespace StomatologyApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
                Customer _customer = new Customer
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    TelephoneNumber = customer.TelephoneNumber,
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatologyApp.Models;
using StomatologyApp.ViewModels.Customer;

namespace StomatologyApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public ViewResult GetCustomers()
        {
            var customers = customerRepository.GetCustomers();

            return View(customers);
        }

        [HttpGet]
        public ViewResult EditCustomer (int? Id)
        {
            var customer = customerRepository.GetCustomer(Id.Value);

            if (customer == null)
            {
                ViewBag.ErrorMessage = "The user does not exist any longer";
                return View("NotFound");
            }

            CustomerEditVM _customer = new CustomerEditVM()
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
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

                var customer = customerRepository.GetCustomer(model.CustomerId);

                if (customer == null)
                {
                    ViewBag.ErrorMessage = "The user does not exist.";
                    return View("NotFound");
                }

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Address = model.Address;
                customer.TelephoneNumber = model.TelephoneNumber;

                customerRepository.UpdateCustomer(customer);
                return RedirectToAction("GetCustomers");

            }

            return View(model);
           
        }

    }
}
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
        private readonly ICustomer customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public ViewResult GetCustomers()
        {
            var customers = customerRepository.GetCustomers();

            return View(customers);
        }

        [HttpGet] 
        public IActionResult GetCustomer(int Id)
        {
            var customer = customerRepository.GetCustomer(Id);
            return View(customer);
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

                var customer = customerRepository.GetCustomer(model.CustomerId);

                if (customer == null)
                {
                    ViewBag.ErrorMessage = "The customer does not exist.";
                    return View("NotFound");
                }

                customer.Name = model.Name;
                customer.TelephoneNumber = model.TelephoneNumber;
                customer.Address = model.Address;

                customerRepository.UpdateCustomer(customer);
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
               
                //if (customer.Address == null)
                //{
                //    customer.Name = customer.Name;
                //    customer.Address = "The address was not specified";
                //    customer.TelephoneNumber = customer.TelephoneNumber;
                //};

                Customer _customer = new Customer
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    TelephoneNumber = customer.TelephoneNumber,
                }; 
                //idealno bi bilo kad se kod ne bi ponavljao
                //provjeriti (async?)

                customerRepository.AddCustomer(_customer);
                return RedirectToAction("GetCustomers");
            }

            return View(customer); 
        }


        [HttpPost]
        public  IActionResult DeleteCustomer(int Id)
        {
            var _customer = customerRepository.GetCustomer(Id);

            if(_customer == null) 
            {
                ViewBag.ErrorMessage = "The customer does not exist.";
                return View("NotFound");
            }

            var customer = customerRepository.DeleteCustomer(Id);
            return RedirectToAction("GetCustomers");

            
        }

    }
}
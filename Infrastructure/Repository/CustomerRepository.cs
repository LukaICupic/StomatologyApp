﻿using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Customer GetCustomer(int Id)
        {
            return context.Customers.Find(Id);
        }


        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers;
        }

        public IEnumerable<Customer> SearchForCustomer (string name)
        {
            var customer = context.Customers.Where(c => c.Name.Contains(name)).ToList();
            return customer;
        }
        public Customer AddCustomer(Customer customer)
        {
            context.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {

            var _customer = context.Customers.Attach(customer);
            _customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return customer;
        }

        public Customer DeleteCustomer(int Id)
        {
            var model = context.Customers.Find(Id);

            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }

            return model;
        }
    }
}

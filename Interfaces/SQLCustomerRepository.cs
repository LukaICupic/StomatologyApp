using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Interfaces
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public SQLCustomerRepository(AppDbContext context)
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

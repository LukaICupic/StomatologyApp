using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int Id);

        IEnumerable<Customer> GetCustomers();

        IEnumerable<Customer> SearchForCustomer(string name);

        Customer AddCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);

        Customer DeleteCustomer(int Id);
    }
}

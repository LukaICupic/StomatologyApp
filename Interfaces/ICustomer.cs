using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp
{
    public interface ICustomer
    {
        Customer GetCustomer(int Id);

        IEnumerable<Customer> GetCustomers();

        Customer AddCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);

        Customer DeleteCustomer(int Id);
    }
}

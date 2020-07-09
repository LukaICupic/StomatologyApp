using Microsoft.AspNetCore.Http;
using StomatologyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.Customer
{
    public class CustomerCreateVM
    {     
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Telephone number is a required field")]
        public string TelephoneNumber { get; set; }
        public IFormFile Photo { get; set; }
    }
}

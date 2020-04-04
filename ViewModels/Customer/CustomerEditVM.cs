﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.ViewModels.Customer
{
    public class CustomerEditVM
    {
       
        
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Address { get; set; } 
        [Required]
        public string TelephoneNumber { get; set; }


        
    }
}

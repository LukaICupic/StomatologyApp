﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StomatologyApp.Models
{
    public class AppointmentProcedure
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int DentalProcedureId { get; set; }
        public DentalProcedure DentalProcedure { get; set; }
    }
}

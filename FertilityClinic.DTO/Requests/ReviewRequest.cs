﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class ReviewRequest
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}

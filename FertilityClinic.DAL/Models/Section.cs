﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public ICollection<Floor> Floors { get; set; }
    }
}

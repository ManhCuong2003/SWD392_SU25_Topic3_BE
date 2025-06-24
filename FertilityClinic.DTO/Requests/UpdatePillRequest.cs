using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdatePillRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? UnitPrice { get; set; }
        public string? Unit { get; set; } // e.g., mg, ml, tablets

        public bool? RequiresPrescription { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

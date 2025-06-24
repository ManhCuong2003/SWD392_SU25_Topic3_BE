using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class PillRequest
    {
        public string? NameAndContent { get; set; }
        public string? Unit { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        
    }
}

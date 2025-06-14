using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdateLabTestScheduleRequest
    {
        
        public DateTime? TestDate { get; set; }
        public string? TestType { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        
    }
}

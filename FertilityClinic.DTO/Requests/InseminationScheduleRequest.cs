using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class InseminationScheduleRequest
    {
        
        public DateTime? InseminationDate { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class InseminationResultRequest
    {
        
        public string? ResultDetails { get; set; }
        public string? Notes { get; set; }
        public DateTime ResultDate { get; set; } = DateTime.UtcNow;
        
    }
}

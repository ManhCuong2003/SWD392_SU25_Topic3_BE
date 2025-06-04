using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class TreatmentMethodRequest
    {
        public string? MethodName { get; set; }
        public string? MethodCode { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? TechnicalRequirements { get; set; }
        public int? AverageDuration { get; set; } // in days
    }
}

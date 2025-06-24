using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PrescriptionResponse
    {
        public string? MethodName { get; set; }
        public string? TrackingMode { get; set; }
        
        public PrescriptionDetailsResponse? PrescriptionDetail { get; set; }
    }
}

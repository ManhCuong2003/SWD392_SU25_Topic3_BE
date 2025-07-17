using System;
using System.Collections.Generic;

namespace FertilityClinic.DTO.Responses
{
    public class PrescriptionResponse
    {
        public int PrescriptionId { get; set; }
        //public string? Status { get; set; }
        
        public string? MethodName { get; set; }
        public string? Monitoring { get; set; }

        public List<PrescriptionItemResponse>? Medications { get; set; }
    }
}

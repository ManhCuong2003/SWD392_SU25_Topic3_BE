using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class TreatmentProcessResponse
    {
        public int TreatmentProcessId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TreatmentMethodId { get; set; }
        public string TreatmentMethodName { get; set; }
        public string ProcessName { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}

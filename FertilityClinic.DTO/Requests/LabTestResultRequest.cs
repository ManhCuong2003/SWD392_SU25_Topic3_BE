using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class LabTestResultRequest
    {
        public string? Name { get; set; }
        public string? Result { get; set; }
        public string? Normal { get; set; }
        public string? Unit { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool Bold { get; set; } = false;
    }
}

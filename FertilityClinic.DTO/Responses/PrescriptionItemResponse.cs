using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PrescriptionItemResponse
    {
        public int? MedicationId { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public int? Quantity { get; set; }
        public string? Usage { get; set; }
    }
}

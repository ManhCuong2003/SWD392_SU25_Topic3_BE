using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PrescriptionByUserResponse
    {
        public int PrescriptionId { get; set; }
        //public string? Status { get; set; }
        //public int? MedicationId { get; set; }
        public int? TreatmentMethodId { get; set; }
        public string? Monitoring { get; set; }

        public List<PrescriptionItemResponse>? Medications { get; set; }
    }
}

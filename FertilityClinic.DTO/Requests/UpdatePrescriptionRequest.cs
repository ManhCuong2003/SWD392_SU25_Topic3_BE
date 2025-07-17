using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdatePrescriptionRequest
    {
        public int? TreatmentMethodId { get; set; }
        public string? Monitoring { get; set; }

        public List<PrescriptionMedicationRequest>? Medications { get; set; }
    }

    public class PrescriptionMedicationRequest
    {
        public int PillId { get; set; }
        public string Dosage { get; set; } = null!;
        public int? Quantity { get; set; }
        public string Unit { get; set; } = null!;
    }

}

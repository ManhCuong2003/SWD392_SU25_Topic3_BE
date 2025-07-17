using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    using System.Collections.Generic;

    namespace FertilityClinic.DTO.Requests
    {
        public class CreatePrescriptionRequest
        {
            
            public int TreatmentMethodId { get; set; }

            //public string? Status { get; set; }
            public string? Monitoring { get; set; }

            public List<CreatePrescriptionItemDto>? Medications { get; set; }
        }

        public class CreatePrescriptionItemDto
        {
            public int PillId { get; set; }
            public string? Unit { get; set; }
            public string? Dosage { get; set; }
            public int? Quantity { get; set; }
        }
    }

}

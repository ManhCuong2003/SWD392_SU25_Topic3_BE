using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdatePrescriptionRequest
    {
        public string Status { get; set; }
        public DateTime PrescribedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<UpdatePrescriptionItemDto> Items { get; set; }
    }

    public class UpdatePrescriptionItemDto
    {
        public int? ItemId { get; set; } // Có thể null nếu là thuốc mới
        public int PillId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }

}

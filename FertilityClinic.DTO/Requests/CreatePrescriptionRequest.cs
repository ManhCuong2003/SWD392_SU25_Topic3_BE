using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class CreatePrescriptionRequest
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }

        public List<CreatePrescriptionItemDto> Items { get; set; }
    }
    public class CreatePrescriptionItemDto
    {
        public int PillId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }
}

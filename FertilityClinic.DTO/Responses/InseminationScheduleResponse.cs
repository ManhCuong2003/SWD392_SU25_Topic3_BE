using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class InseminationScheduleResponse
    {
        public int InseminationScheduleId { get; set; }
        public int TreatmentProcessId { get; set; }
        public int DoctorId { get; set; }
        public DateTime InseminationDate { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

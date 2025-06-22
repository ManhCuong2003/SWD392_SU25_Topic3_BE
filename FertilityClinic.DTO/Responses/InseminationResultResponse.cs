using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class InseminationResultResponse
    {
        public int InseminationResultId { get; set; }
        public int InseminationScheduleId { get; set; }
        public int DoctorId { get; set; }
        public string? ResultDetails { get; set; }
        public string? Notes { get; set; }
        public DateTime ResultDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
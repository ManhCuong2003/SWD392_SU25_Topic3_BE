using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class GetAllPatientsResponse
    {
        public int? PatientId { get; set; }
        public string? FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? treatmentStatus { get; set; }
        public string? Diagnose { get; set; }
        public DateTime? lastVisit { get; set; }
        public string? doctorInCharge { get; set; } = null;
    }
}

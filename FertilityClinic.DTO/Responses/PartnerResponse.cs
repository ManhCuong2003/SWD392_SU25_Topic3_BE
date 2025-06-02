using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PartnerResponse
    {
        public string FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
        public string HealthInsuranceId { get; set; }
    }
}

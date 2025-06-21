using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PatientResponse
    {
        public string PatientCode { get; set; } // VD: BN001
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Diagnosis { get; set; }
        public string Status { get; set; }
        public DateTime? LastCheckupDate { get; set; }
        public string DoctorName { get; set; }
    }
}

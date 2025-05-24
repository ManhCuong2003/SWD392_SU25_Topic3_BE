using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class DoctorResponse
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Avatar { get; set; }
        public string Specialization { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public string Certifications { get; set; } = null!;
        public int ExperienceYear { get; set; }
        public string Bio { get; set; } = null!;
        public List<string> Education { get; set; } = new();
    }
}

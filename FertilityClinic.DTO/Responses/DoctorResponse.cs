using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class DoctorResponse
    {
        public int DotorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public string Avatar { get; set; }
        public List<string> Experience { get; set; } = new List<string>();
        public int ExperienceYears { get; set; }
        public string Bio { get; set; }
        public List<string> Education { get; set; } = new List<string>();
        public bool Status { get; set; }

    }
}

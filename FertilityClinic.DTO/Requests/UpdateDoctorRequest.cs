using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdateDoctorRequest
    {
        public string? Avatar { get; set; } = null!;
        public string? Specialization { get; set; } = null!;
        public string? Degree { get; set; } = null!;
        public List<string>? Experience { get; set; } = new();
        public int? ExperienceYear { get; set; }
        public string? Bio { get; set; } = null!;
        public List<string>? Education { get; set; } = new();
        public bool? Status { get; set; } = true;
    }
}

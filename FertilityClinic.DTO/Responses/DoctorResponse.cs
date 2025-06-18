using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class DoctorResponse
    {
        
        public string DoctorName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Specialization { get; set; } = null!;
        public string Degree { get; set; } = null!;
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class GetAllUsersResponse
    {
        public int UserId { get; set; }

        public string? FullName { get; set; }

        public DateOnly? DateDateOfBirth { get; set; }
        public string? Gender { get; set; }
        //public string? DoctorName { get; set; }

        //public DateOnly? AppointmentDate { get; set; }


    }
}

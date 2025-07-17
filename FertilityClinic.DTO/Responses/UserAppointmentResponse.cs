using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class UserAppointmentResponse : UserResponse
    {
        public new int AppointmentId { get; set; }
        public new DateOnly? AppointmentDate { get; set; }
        public new TimeOnly? AppointmentTime { get; set; }
    }
}

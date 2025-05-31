using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class AppointmentRequest
    {
        public int UserId { get; set; }
        public int TreatmentMethodId { get; set; }
        public string PartnerName { get; set; }
        public DateOnly PartnerDOB { get; set; }
        public int DoctorId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        [Column(TypeName = "time")]
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

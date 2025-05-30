using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public string PartnerName { get; set; }
        public DateOnly PartnerDOB { get; set; }
        public int DoctorId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

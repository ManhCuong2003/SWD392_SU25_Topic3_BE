using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class AppointmentHistory
    {
        public int AppointmentHistoryId { get; set; }
        public int UserId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateOnly? PatientDOB {  get; set; }
        public string PatientGender { get; set; }
        public string PhoneNumber { get; set; }
        public string PartnerName { get; set; }
        public DateOnly? PartnerDOB { get; set; }
        public string PartnerGender { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
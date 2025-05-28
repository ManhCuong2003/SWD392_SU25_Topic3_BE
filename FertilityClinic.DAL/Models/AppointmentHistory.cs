using System;
using System.Collections.Generic;
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
        public DateTime PatientDOB {  get; set; }
        public string PartnerPatientName { get; set; }
        public DateTime? PatientDOBDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Section { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}

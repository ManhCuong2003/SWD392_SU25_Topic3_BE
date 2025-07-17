using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Avatar { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public List<string> Experience { get; set; } = new List<string>();
        public int ExperienceYears { get; set; }
        public string Bio { get; set; }
        public List<string> Education { get; set; } = new List<string>();
        public bool Status { get; set; } = true; // true for active, false for inactive
        public virtual User User { get; set; }
        
        public ICollection<TreatmentProcess> TreatmentProcesses { get; set; }
        public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
        //public ICollection<LabTestSchedule> LabTestSchedules { get; set; }
        //public ICollection<LabTestResult> LabTestResults { get; set; }
        public ICollection<InseminationSchedule> InseminationSchedules { get; set; }
        public ICollection<InseminationResult> InseminationResults { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

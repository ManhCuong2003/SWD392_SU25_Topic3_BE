using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class InjectionSchedule
    {
        public int InjectionScheduleId { get; set; }
        public int TreatmentProcessId { get; set; }
        public int DoctorId { get; set; }
        public DateTime InjectionDate { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual TreatmentProcess TreatmentProcess { get; set; }
        public Doctor Doctor { get; set; }
    }
}

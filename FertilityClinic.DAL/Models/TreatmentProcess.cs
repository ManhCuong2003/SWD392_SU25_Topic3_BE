using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class TreatmentProcess
    {
        public int TreatmentProcessId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int TreatmentMethodId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public virtual Patient Patient { get; set; }
        public  virtual Doctor Doctor { get; set; }
        public virtual TreatmentMethod TreatmentMethod { get; set; }

        public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
        public ICollection<LabTestSchedule> LabTestSchedules { get; set; }
        public ICollection<InseminationSchedule> InseminationSchedules { get; set; }
    }
}

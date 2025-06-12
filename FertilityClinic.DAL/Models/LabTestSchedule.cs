using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class LabTestSchedule
    {
        public int LabTestScheduleId { get; set; }
        public int TreatmentProcessId { get; set; }
        public int DoctorId { get; set; }
        public DateTime TestDate { get; set; }
        public string? TestType { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public virtual TreatmentProcess? TreatmentProcess { get; set; }
        public  virtual Doctor? Doctor { get; set; }
    }
}

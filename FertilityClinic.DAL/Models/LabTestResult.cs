using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class LabTestResult
    {
        public int LabTestResultId { get; set; }
        public int LabTestScheduleId { get; set; }
        public int DoctorId { get; set; }
        public string? ResultDetails { get; set; }
        public string? Notes { get; set; }
        public DateTime ResultDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



        public  virtual LabTestSchedule? LabTestSchedule { get; set; }
        public  virtual Doctor? Doctor { get; set; }
    }
}

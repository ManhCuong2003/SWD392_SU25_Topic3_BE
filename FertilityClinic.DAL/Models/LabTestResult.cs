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
        //public int LabTestScheduleId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Result { get; set; }
        public string? Normal { get; set; }
        public string? Unit { get; set; }
        public bool? Bold { get; set; } = false;
        public DateTime? Date { get; set; }
        
        //public  virtual LabTestSchedule? LabTestSchedule { get; set; }
        public  virtual User? User { get; set; }
    }
}

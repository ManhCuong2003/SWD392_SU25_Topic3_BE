using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
   public class TreatmentProcess
        {
            public int TreatmentProcessId { get; set; }
    
            [Required]
            public int UserId { get; set; }
            public User User { get; set; }
    
            [Required]
            public int TreatmentMethodId { get; set; }
            public TreatmentMethod TreatmentMethod { get; set; }
    
            [Required]
            [MaxLength(50)]
            public string Status { get; set; } // InProgress, Completed, Cancelled
    
            public string Notes { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
            public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
            public ICollection<InseminationSchedule> InseminationSchedules { get; set; }
            public ICollection<LabTestSchedule> LabTestSchedules { get; set; }
        }
}

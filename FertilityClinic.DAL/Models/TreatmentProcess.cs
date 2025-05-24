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
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    
    [Required]
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    
    [Required]
    public int TreatmentMethodId { get; set; }
    public TreatmentMethod TreatmentMethod { get; set; }
    
    public int? MarriageCertificateId { get; set; }
    public MarriageCertificate MarriageCertificate { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } // InProgress, Completed, Cancelled
    
    public string Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
    public ICollection<InseminationSchedule> InseminationSchedules { get; set; }
    public ICollection<LabTestSchedule> LabTestSchedules { get; set; }
}
}

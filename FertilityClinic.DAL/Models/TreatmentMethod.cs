using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class TreatmentMethod
{
    public int TreatmentMethodId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string MethodName { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string MethodCode { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    public bool? IsActive { get; set; } = true;
    
    public string? TechnicalRequirements { get; set; }
    
    public int? AverageDuration { get; set; } // in days
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        private float? Price { get; set; } // Price in VND

        public ICollection<TreatmentProcess>? TreatmentProcesses { get; set; }
}
}

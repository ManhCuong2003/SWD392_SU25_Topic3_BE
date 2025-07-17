using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilityClinic.DAL.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        
        

        [Required] 
        public int TreatmentMethodId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? Monitoring { get; set; }
        public DateTime PrescribedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
        
        public virtual TreatmentMethod? TreatmentMethod { get; set; }

        public virtual User? User { get; set; }
        
    }
}
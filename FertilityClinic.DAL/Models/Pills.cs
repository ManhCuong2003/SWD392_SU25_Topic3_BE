using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilityClinic.DAL.Models
{
    public class Pills
    {
        [Key]
        public int PillId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string? NameAndContent { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        public int? Quantity { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public decimal? UnitPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<PrescriptionDetails> PrescriptionDetails { get; set; } = new List<PrescriptionDetails>();
    }
}

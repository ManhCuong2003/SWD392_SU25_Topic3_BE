using FertilityClinic.DAL.Models;
using System.ComponentModel.DataAnnotations;

public class PrescriptionItem
{
    [Key]
    public int PrescriptionItemId { get; set; }

    [Required]
    public int PrescriptionId { get; set; }

    [Required]
    public int PillId { get; set; }

    public string? Unit { get; set; } // e.g., mg, ml, tablets
    [StringLength(100)]
    public string? Dosage { get; set; }
    public int Quantity { get; set; }

    

    // Navigation
    public virtual Prescription? Prescription { get; set; }
    public virtual Pills? Pill { get; set; }
}

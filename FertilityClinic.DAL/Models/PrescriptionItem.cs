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

    [Required]
    [StringLength(100)]
    public string Dosage { get; set; }

    [Required]
    [StringLength(100)]
    public string Frequency { get; set; }

    [StringLength(100)]
    public string Duration { get; set; }

    public int Quantity { get; set; }

    [StringLength(500)]
    public string Instructions { get; set; }

    // Navigation
    public virtual Prescription Prescription { get; set; }
    public virtual Pills Pill { get; set; }
}

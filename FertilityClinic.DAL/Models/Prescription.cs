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
        public int PillId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosage { get; set; } // e.g., "2 tablets"

        [Required]
        [StringLength(100)]
        public string Frequency { get; set; } // e.g., "twice daily"

        [StringLength(100)]
        public string Duration { get; set; } // e.g., "7 days"

        public int Quantity { get; set; }

        [StringLength(500)]
        public string Instructions { get; set; }

        public DateTime PrescribedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Status { get; set; } // Active, Completed, Cancelled

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Pills Pill { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilityClinic.DAL.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // Pending, Completed, Failed, Refunded

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
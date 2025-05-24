using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class MarriageCertificate
    {
        public int CertificateId { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        [MaxLength(50)]
        public string CertificateNumber { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string IssuedBy { get; set; }

        [Required]
        [MaxLength(100)]
        public string SpouseName { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpouseIdNumber { get; set; }

        [MaxLength(255)]
        public string DocumentUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string VerificationStatus { get; set; } // Verified, Pending, Rejected

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TreatmentProcess> TreatmentProcesses { get; set; }
    }
}

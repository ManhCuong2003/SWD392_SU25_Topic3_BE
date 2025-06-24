using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilityClinic.DAL.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public int TreatmentMethodId { get; set; }
        public string? TrackingMode { get; set; }
        public DateTime PrescribedDate { get; set; } = DateTime.Now;
        public virtual User? User { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Appointment? Appointment { get; set; }
        public virtual TreatmentMethod? TreatmentMethod { get; set; }
        public virtual ICollection<PrescriptionDetails> PrescriptionDetails { get; set; } = new List<PrescriptionDetails>();
    }

}
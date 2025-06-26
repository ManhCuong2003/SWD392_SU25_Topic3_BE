using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PrescriptionResponse
    {
        public int PrescriptionId { get; set; }

        public int PillId { get; set; }

        public int DoctorId { get; set; }

        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        public string Dosage { get; set; }

        public string Frequency { get; set; }

        public string Duration { get; set; } 

        public int Quantity { get; set; }

        public string Instructions { get; set; }

        public DateTime PrescribedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

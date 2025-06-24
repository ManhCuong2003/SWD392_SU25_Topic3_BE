using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class PrescriptionDetails
    {
        [Key]
        public int PrescriptionDetailId { get; set; }

        public int PrescriptionId { get; set; }
        public int PillId { get; set; }

        public int? Quantity { get; set; }
        public string? Dosage { get; set; }         // "Uống 4 viên"
        public string? Instructions { get; set; }   // "Chia 2 lần sau ăn"

        public virtual Prescription? Prescription { get; set; }
        public virtual Pills? Pill { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        public long OrderCode { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; } // Pending, PAID, CANCELLED
        public DateTime CreatedAt { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public int UserId { get; set; }
        public string? FullName { get; set; }
        
        public string? Phone { get; set; }
        public string? NationalId { get; set; }
        public string? Gender { get; set; }
        public string? HealthInsuranceId { get; set; }
        public DateOnly? HealthInsuranceExpirationDate { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public virtual User? User { get; set; }
    }
}

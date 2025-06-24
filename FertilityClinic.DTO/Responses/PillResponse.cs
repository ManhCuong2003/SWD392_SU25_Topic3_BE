using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class PillResponse
    {
        public int PillId { get; set; }
        public string? NameAndContent { get; set; }
        public string? Unit { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        // Navigation properties can be added if needed
        // public virtual ICollection<PrescriptionResponse> Prescriptions { get; set; } = new List<PrescriptionResponse>();
    }
}

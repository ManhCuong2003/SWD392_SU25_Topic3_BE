using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class TreatmentMethod
    {
        public int TreatmentMethodId { get; set; }
        public string MethodName { get; set; }
        public string MethodCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<TreatmentProcess> TreatmentProcesses { get; set; }
    }
}

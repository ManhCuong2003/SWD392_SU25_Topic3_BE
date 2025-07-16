using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdateTreatmentProcessRequest
    {
        
        public string? ProcessName { get; set; } // Nullable to allow partial updates
        public string? Notes { get; set; } // Nullable to allow partial updates
        public string? TrackingMode { get; set; } // Nullable to allow partial updates

    }
}

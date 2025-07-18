using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class LabTestResultUpdateRequest
    {
        //public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Result { get; set; }
        public string? Normal { get; set; }
        public string? Unit { get; set; }
        public bool? Bold { get; set; }
        public DateTime? Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class LabTestResultResponse
    {
        public int LabTestResultId { get; set; }
        //public int LabTestScheduleId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Result { get; set; }
        public string? Normal { get; set; }
        public string? Unit { get; set; }
        public bool Bold { get; set; } = false;
        public DateTime Date { get; set; }
    }
}
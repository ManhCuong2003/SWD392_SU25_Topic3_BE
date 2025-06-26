using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Responses
{
    public class ReviewResponse
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

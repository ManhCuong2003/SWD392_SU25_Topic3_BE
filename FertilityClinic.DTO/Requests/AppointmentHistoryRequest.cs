using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class AppointmentHistoryRequest
    {
        public int UserId { get; set; }
        public string PatientName { get; set; }
        public DateOnly? PatientDOB { get; set; }
        public string PhoneNumber { get; set; }
        public string PartnerName { get; set; }
        public DateOnly PartnerDOB { get; set; }
        public string DoctorName { get; set; }
        [DataType(DataType.Date)]  // Chỉ định kiểu Date (ngày-tháng-năm)
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        [Column(TypeName = "time")]
        public TimeOnly AppointmentTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

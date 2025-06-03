using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class UpdateAppointmentRequest
    {
        public int? UserId { get; set; }
        public int? TreatmentMethodId { get; set; }
        public string? PartnerName { get; set; }
        public DateOnly? PartnerDOB { get; set; }
        public int? DoctorId { get; set; }
        [DataType(DataType.Date)]  // Chỉ định kiểu Date (ngày-tháng-năm)
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AppointmentDate { get; set; }

        [Column(TypeName = "time")]
        public TimeOnly? AppointmentTime { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class AppointmentRequest
    {
        [DataType(DataType.Date)]  // Chỉ định kiểu Date (ngày-tháng-năm)
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

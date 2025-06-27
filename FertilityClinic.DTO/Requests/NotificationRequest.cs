using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}

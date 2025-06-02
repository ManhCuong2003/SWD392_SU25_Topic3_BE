using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int PartnerId { get; set; }
        public string HealthInsuranceId { get; set; }
        public DateOnly? HealthInsuranceExpirationDate { get; set; }
        public bool IsMarried { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<AppointmentHistory> GetAppointmentHistories {  get; set; }
        public virtual Doctor Doctor { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}

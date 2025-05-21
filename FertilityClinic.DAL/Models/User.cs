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
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}

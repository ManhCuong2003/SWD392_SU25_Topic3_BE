using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class RegisterRequest
    {
        [DefaultValue("Full name")]
        public string? FullName { get; set; }
        
        [DefaultValue("example@email.com")]
        public string? Email { get; set; }
        [DefaultValue("Your_password")]
        public string? Password { get; set; }
        public string? Phone {  get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}

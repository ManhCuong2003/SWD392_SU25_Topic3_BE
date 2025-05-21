using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Requests
{
    public class LoginRequest
    {
        [Required]
        [DefaultValue("example@email.com")]
        public string Email { get; set; }
        [Required]
        [DefaultValue("Your_password")]
        public string Password { get; set; }
    }
}

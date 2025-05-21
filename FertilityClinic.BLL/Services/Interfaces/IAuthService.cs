using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest dto);
        Task<RegisterResponse?> RegisterAsync(RegisterRequest dto);
    }
}

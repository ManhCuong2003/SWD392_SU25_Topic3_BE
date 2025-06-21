using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateUserAsync(UpdateUserRequest dto);
        Task<bool> SoftDeleteUserAsync(int userId);
        Task<List<GetAllUsersResponse>> GetAllUsersAsync();
        Task<UserResponse> GetByIdAsync(int id);
        Task<bool> HardDeleteUserAsync(int id);
        Task<UserResponse> GetByEmailAsync(string email);
        //Task<List<GetAllPatientsResponse>> GetAllPatientAsync();
    }
}

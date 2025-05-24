using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IDoctorService
    {   
        Task<List<DoctorResponse>> GetAllDoctorsAsync();
        Task<DoctorResponse?> GetDoctorByIdAsync(int id);
        Task<DoctorResponse> CreateDoctorAsync(DoctorRequest request, int userId);
        Task<DoctorResponse> UpdateDoctorAsync(int id, UpdateDoctorRequest request);
        Task<bool> DeleteDoctorAsync(int id);
    }
}

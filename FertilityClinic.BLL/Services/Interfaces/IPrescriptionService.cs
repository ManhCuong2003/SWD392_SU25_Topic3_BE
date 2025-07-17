using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Requests.FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<List<PrescriptionResponse>> GetAllPrescriptionsAsync();
        Task<PrescriptionResponse?> GetPrescriptionByIdAsync(int id);
        Task<PrescriptionResponse> CreatePrescriptionAsync(CreatePrescriptionRequest request, int UserId);
        Task<PrescriptionResponse> UpdatePrescriptionAsync(int id,UpdatePrescriptionRequest request);
        Task<bool> DeletePrescriptionAsync(int id);
        Task<List<PrescriptionByUserResponse>> GetPrescriptionsByUserIdAsync(int userId);

    }
}

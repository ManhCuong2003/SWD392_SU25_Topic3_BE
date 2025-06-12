using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface ITreatmentProcessService
    {
        Task<List<TreatmentProcessResponse>> GetAllTreatmentProcessesAsync();
        Task<TreatmentProcessResponse?> GetTreatmentProcessByIdAsync(int id);
        Task<TreatmentProcessResponse> CreateTreatmentProcessAsync(TreatmentProcessRequest request, int userId, int treatmentMethodId, int doctorId);
        Task<TreatmentProcessResponse> UpdateTreatmentProcessAsync(int id, UpdateTreatmentProcessRequest request);
        Task<bool> DeleteTreatmentProcessAsync(int id);
    }
}

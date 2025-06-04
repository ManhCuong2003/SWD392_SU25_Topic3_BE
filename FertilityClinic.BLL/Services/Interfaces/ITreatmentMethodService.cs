using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface ITreatmentMethodService
    {
        Task<List<TreatmentMethodResponse>> GetAllTreatmentMethodsAsync();
        Task<TreatmentMethodResponse?> GetTreatmentMethodByIdAsync(int id);
        Task<TreatmentMethodResponse> CreateTreatmentMethodAsync(TreatmentMethodRequest request);
        Task<TreatmentMethodResponse> UpdateTreatmentMethodAsync(int id, TreatmentMethodRequest request);
        Task<bool> DeleteTreatmentMethodAsync(int id);
    }
}

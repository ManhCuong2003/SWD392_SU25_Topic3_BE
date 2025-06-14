using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IInseminationScheduleService
    {
        Task<InseminationScheduleResponse> CreateInseminationScheduleAsync(InseminationScheduleRequest request, int doctorId, int treatmentMethodId);
        Task<bool> DeleteInseminationScheduleAsync(int id);
        Task<List<InseminationScheduleResponse>> GetAllInseminationSchedulesAsync();
        Task<InseminationScheduleResponse> GetInseminationScheduleByIdAsync(int inseminationScheduleId);
        Task<InseminationScheduleResponse> UpdateInseminationScheduleAsync(int id, InseminationScheduleRequest request);
    }
}

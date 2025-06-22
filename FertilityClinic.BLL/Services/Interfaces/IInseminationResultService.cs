using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IInseminationResultService
    {
        Task<List<InseminationResultResponse>> GetAllInseminationResultsAsync();
        Task<InseminationResultResponse?> GetInseminationResultByIdAsync(int id);
        Task<InseminationResultResponse> CreateInseminationResultAsync(InseminationResultRequest request, int inseminationScheduleId, int doctorId);
        //Task<InseminationResultResponse> UpdateInseminationResultAsync(int id, InseminationResultRequest request);
        Task<bool> DeleteInseminationResultAsync(int id);
    }
}

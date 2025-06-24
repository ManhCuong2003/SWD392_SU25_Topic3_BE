using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPillService
    {
        Task <List<PillResponse>> GetAllPillsAsync();
        Task<PillResponse?> GetPillByIdAsync(int pillid);
        Task<PillResponse> CreatePillAsync(PillRequest request);
        Task<PillResponse> UpdatePillAsync(int pillid, PillRequest request);
        Task<bool> DeletePillAsync(int pillid);
    }
}

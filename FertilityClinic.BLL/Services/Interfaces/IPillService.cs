using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPillService
    {
        Task<List<PillResponse>> GetAllPillsAsync();
        Task<PillResponse> GetPillByIdAsync(int pillId);
        Task<List<PillResponse>> GetPillsByNameAsync(string name);
        Task<PillResponse> AddPillAsync(PillRequest pill);
        Task<PillResponse> UpdatePillAsync(UpdatePillRequest pill, int id);
        Task<bool> DeletePillAsync(int pillId);
    }
}

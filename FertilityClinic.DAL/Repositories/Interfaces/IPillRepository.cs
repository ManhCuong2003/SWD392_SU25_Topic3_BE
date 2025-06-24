using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IPillRepository
    {
        Task<List<Pills>> GetAllPillsAsync();
        Task<Pills> GetPillByIdAsync(int pillId);
        Task<List<Pills>> GetPillsByNameAsync(string name);
        Task<Pills> AddPillAsync(Pills pill);
        Task<Pills> UpdatePillAsync(Pills pill);
        Task<bool> DeletePillAsync(int pillId);
    }
}

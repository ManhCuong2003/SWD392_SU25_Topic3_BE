using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IPillRepository : IGenericRepository<Pills>
    {
        Task<bool> CreatePillAsync(Pills pill);
        Task<IEnumerable<Pills>> GetAllPillsAsync();
        Task<Pills> GetPillByIdAsync(int pillId);
        Task<bool> UpdatePillAsync(Pills pill);
        Task<bool> DeletePillAsync(int pillId);
    }
}

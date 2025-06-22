using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IInseminationResultRepository : IGenericRepository<InseminationResult>
    {
        Task <IEnumerable<InseminationResult>> GetAllInseminationResultsAsync();
        Task<InseminationResult?> GetInseminationResultByIdAsync(int id);
        Task<bool> CreateInseminationResultAsync(InseminationResult inseminationResult);
        Task<InseminationResult> UpdateInseminationResultAsync(InseminationResult inseminationResult);
        Task<bool> DeleteInseminationResultAsync(int id);
    }
}

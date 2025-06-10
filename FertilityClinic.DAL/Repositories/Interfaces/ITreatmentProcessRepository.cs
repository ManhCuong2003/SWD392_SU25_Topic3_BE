using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface ITreatmentProcessRepository: IGenericRepository<TreatmentProcess>
    {
        Task<IEnumerable<TreatmentProcess>> GetAllTreatmentProcessesAsync();
        Task<TreatmentProcess?> GetTreatmentProcessByIdAsync(int id);
        Task<TreatmentProcess?> GetTreatmentProcessByNameAsync(string name);
        Task <bool> CreateTreatmentProcessAsync(TreatmentProcess treatmentProcess);
        Task <TreatmentProcess> UpdateTreatmentProcessAsync(TreatmentProcess treatmentProcess);
        Task<bool> DeleteTreatmentProcessAsync(int id);
        
    }
}

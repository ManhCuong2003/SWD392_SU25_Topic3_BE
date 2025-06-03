using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface ITreatmentMethodRepository : IGenericRepository<TreatmentMethod>
    {
        public Task<IEnumerable<TreatmentMethod>> GetAllTreatmentMethodsAsync();
        public Task<TreatmentMethod?> GetTreatmentMethodByIdAsync(int id);
        public Task<bool> CreateTreatmentMethodAsync(TreatmentMethod treatmentMethod);
        public Task<TreatmentMethod> UpdateTreatmentMethodAsync(TreatmentMethod treatmentMethod);
        public Task<bool> DeleteTreatmentMethodAsync(int id);
    }
}

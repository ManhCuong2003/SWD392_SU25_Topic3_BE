using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class TreatmentMethodRepository : GenericRepository<TreatmentMethod>, ITreatmentMethodRepository
    {
        private readonly FertilityClinicDbContext _context;
        public TreatmentMethodRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateTreatmentMethodAsync(TreatmentMethod treatmentMethod)
        {
            await AddAsync(treatmentMethod);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTreatmentMethodAsync(int id)
        {
            var treatmentMethod = await GetTreatmentMethodByIdAsync(id);
            if (treatmentMethod == null)
            {
                return false;
            }
            Remove(treatmentMethod);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TreatmentMethod>> GetAllTreatmentMethodsAsync()
        {
            return await _context.TreatmentMethods
                .Include(tm => tm.TreatmentProcesses)
                .ToListAsync();
        }

        public Task<TreatmentMethod?> GetTreatmentMethodByIdAsync(int id)
        {
            return _context.TreatmentMethods
                .Include(tm => tm.TreatmentProcesses)
                .FirstOrDefaultAsync(tm => tm.TreatmentMethodId == id);
        }

        public async Task<TreatmentMethod> UpdateTreatmentMethodAsync(TreatmentMethod treatmentMethod)
        {
            Update(treatmentMethod);
            await _context.SaveChangesAsync();
            return await GetTreatmentMethodByIdAsync(treatmentMethod.TreatmentMethodId) ?? treatmentMethod;
        }
    }
}

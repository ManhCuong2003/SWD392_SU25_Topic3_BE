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
    public class TreatmentProcessRepository : GenericRepository<TreatmentProcess>, ITreatmentProcessRepository
    {
        private readonly FertilityClinicDbContext _context;
        public TreatmentProcessRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> CreateTreatmentProcessAsync(TreatmentProcess treatmentProcess)
        {
            await AddAsync(treatmentProcess);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTreatmentProcessAsync(int id)
        {
            var treatmentProcess = _context.TreatmentProcesses.Find(id);
            if (treatmentProcess == null)
            {
                return false;
            }
            Remove(treatmentProcess);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TreatmentProcess>> GetAllTreatmentProcessesAsync()
        {
            return await _context.TreatmentProcesses
                .Include(tp => tp.TreatmentMethod)
                .Include(tp => tp.User)
                .ToListAsync();
        }

        public async Task<TreatmentProcess?> GetTreatmentProcessByIdAsync(int id)
        {
            return await _context.TreatmentProcesses
                .Include(tp => tp.TreatmentMethod)
                .Include(tp => tp.User)
                .FirstOrDefaultAsync(tp => tp.TreatmentProcessId == id);
        }

        public async Task<TreatmentProcess?> GetTreatmentProcessByNameAsync(string name)
        {
            return await _context.TreatmentProcesses
                .Include(tp => tp.TreatmentMethod)
                .Include(tp => tp.User)
                .FirstOrDefaultAsync(tp => tp.ProcessName.ToLower().Contains(name.ToLower()));
        }

        public async Task<TreatmentProcess> UpdateTreatmentProcessAsync(TreatmentProcess treatmentProcess)
        {
            Update(treatmentProcess);
            await _context.SaveChangesAsync();
            return await GetTreatmentProcessByIdAsync(treatmentProcess.TreatmentProcessId) ?? treatmentProcess;
        }
    }
}

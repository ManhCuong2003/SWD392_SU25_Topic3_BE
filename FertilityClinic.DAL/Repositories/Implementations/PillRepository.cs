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
    public class PillRepository : GenericRepository<Pills>, IPillRepository
    {
        private readonly FertilityClinicDbContext _context;
        public PillRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatePillAsync(Pills pill)
        {
            await _context.Pills.AddAsync(pill);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePillAsync(int pillId)
        {
            var pill = await _context.Pills.FindAsync(pillId);
            if (pill == null)
            {
                return false;
            }
            Remove(pill);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Pills>> GetAllPillsAsync()
        {
            return await _context.Pills
                .Include(p => p.PrescriptionDetails)
                .ToListAsync();
        }

        public async Task<Pills> GetPillByIdAsync(int pillId)
        {
            return await _context.Pills
                .Include(p => p.PrescriptionDetails)
                .FirstOrDefaultAsync(p => p.PillId == pillId);
        }

        public async Task<bool> UpdatePillAsync(Pills pill)
        {
            Update(pill);
            await _context.SaveChangesAsync();
            return await GetPillByIdAsync(pill.PillId) != null;

        }
    }
}

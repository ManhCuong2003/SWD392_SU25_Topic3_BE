using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FertilityClinic.DTO.Constants.APIEndPoints;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class PillRepository :GenericRepository<Pills>, IPillRepository
    {
        private readonly FertilityClinicDbContext _context;
        public PillRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Pills> AddPillAsync(Pills pill)
        {
           await _context.Pills.AddAsync(pill);
            return pill;
        }

        public async Task<bool> DeletePillAsync(int pillId)
        {
            var pill = await _context.Pills.FirstOrDefaultAsync(p => p.PillId == pillId);
            if (pill == null)
            {
                return false; // Pill not found
            }
            _context.Pills.Remove(pill);
            return await _context.SaveChangesAsync() > 0; // Returns true if the delete was successful
        }

        public async Task<List<Pills>> GetAllPillsAsync()
        {
            return await _context.Pills.ToListAsync();
        }

        public async Task<Pills> GetPillByIdAsync(int pillId)
        {
            return await _context.Pills.FirstOrDefaultAsync(p => p.PillId == pillId);
        }

        public async Task<List<Pills>> GetPillsByNameAsync(string name)
        {
            return await _context.Pills
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Pills> UpdatePillAsync(Pills pill)
        {
            Update(pill);
            await _context.SaveChangesAsync();
            return await GetPillByIdAsync(pill.PillId) ?? pill;
        }
    }
}

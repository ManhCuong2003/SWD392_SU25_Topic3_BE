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
    public class InseminationResultRepository : GenericRepository<InseminationResult>, IInseminationResultRepository
    {
        private readonly FertilityClinicDbContext _context;
        public InseminationResultRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateInseminationResultAsync(InseminationResult inseminationResult)
        {
            await AddAsync(inseminationResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInseminationResultAsync(int id)
        {
            var inseminationResult = await GetByIdAsync(id);
            if (inseminationResult == null)
            {
                return false;
            }
            Remove(inseminationResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<InseminationResult>> GetAllInseminationResultsAsync()
        {
            return await _context.InseminationResults
                .Include(ir => ir.InseminationSchedule)
                .Include(ir => ir.Doctor)
                .ToListAsync();
        }

        public async Task<InseminationResult?> GetInseminationResultByIdAsync(int id)
        {
            return await _context.InseminationResults
                .Include(ir => ir.InseminationSchedule)
                .Include(ir => ir.Doctor)
                .FirstOrDefaultAsync(ir => ir.InseminationResultId == id);
        }

        public Task<InseminationResult> UpdateInseminationResultAsync(InseminationResult inseminationResult)
        {
            throw new NotImplementedException();
        }
    }
}

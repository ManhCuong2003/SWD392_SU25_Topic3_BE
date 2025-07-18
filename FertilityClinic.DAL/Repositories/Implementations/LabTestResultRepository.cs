using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class LabTestResultRepository : GenericRepository<LabTestResult> ,ILabTestResultRepository
    {
        private readonly FertilityClinicDbContext _context;
        public LabTestResultRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateLabTestResultAsync(LabTestResult labTestResult)
        {
            await _context.LabTestResults.AddAsync(labTestResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLabTestResultAsync(int labTestResultId)
        {
            var labTestResult = await _context.LabTestResults.FindAsync(labTestResultId);
            if (labTestResult == null)
            {
                return false;
            }
            Remove(labTestResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<LabTestResult>> GetAllLabTestResultsAsync()
        {
            return await _context.LabTestResults
                //.Include(ltr => ltr.LabTestSchedule)
                .Include(ltr => ltr.User)
                .ToListAsync();
        }

        public async Task<LabTestResult> GetLabTestResultByIdAsync(int labTestResultId)
        {
            return await _context.LabTestResults
                //.Include(ltr => ltr.LabTestSchedule)
                .Include(ltr => ltr.User)
                .FirstOrDefaultAsync(ltr => ltr.LabTestResultId == labTestResultId);
        }

        public async Task<IEnumerable<LabTestResult>> GetLabTestResultsByUserIdAsync(int userId)
        {
            return await _context.LabTestResults
                .Where(r => r.UserId == userId)
                //.Include(r => r.LabTestSchedule) // Nếu cần
                .Include(r => r.User)
                .ToListAsync();
        }
        public async Task<LabTestResult?> GetByUserIdAsync(int userId)
        {
            return await _context.LabTestResults
                                 .FirstOrDefaultAsync(l => l.UserId == userId);
        }

    }
}

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
    public class InseminationScheduleRepository : GenericRepository<InseminationSchedule>, IInseminationScheduleRepository
    {
        private readonly FertilityClinicDbContext _context;
        public InseminationScheduleRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateInseminationScheduleAsync(InseminationSchedule inseminationSchedule)
        {
            await _context.InseminationSchedules.AddAsync(inseminationSchedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInseminationScheduleAsync(int inseminationScheduleId)
        {
           var inseminationSchedule = await _context.InseminationSchedules.FindAsync(inseminationScheduleId);
            if (inseminationSchedule == null)
            {
                return false;
            }
            Remove(inseminationSchedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<InseminationSchedule>> GetAllInseminationSchedulesAsync()
        {
            return await _context.InseminationSchedules
                .Include(s => s.TreatmentProcess)
                .Include(s => s.Doctor)
                .ToListAsync();
        }

        public async Task<InseminationSchedule> GetInseminationScheduleByIdAsync(int scheduleId)
        {
            return await _context.InseminationSchedules
                .Include(s => s.TreatmentProcess)
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.InseminationScheduleId == scheduleId);
        }

        public async Task<InseminationSchedule> UpdateInseminationScheduleAsync(InseminationSchedule inseminationSchedule)
        {
            Update(inseminationSchedule);
            await _context.SaveChangesAsync();
            return await GetInseminationScheduleByIdAsync(inseminationSchedule.InseminationScheduleId);
        }
    }
}

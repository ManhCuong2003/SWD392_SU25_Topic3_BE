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
    public class LabTestScheduleRepository : GenericRepository<LabTestSchedule>, ILabTestScheduleRepository
    {
        private readonly FertilityClinicDbContext _context;
        public LabTestScheduleRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateLabTestScheduleAsync(LabTestSchedule labTestSchedule)
        {
            await _context.LabTestSchedules.AddAsync(labTestSchedule);  
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLabTestScheduleAsync(int labTestScheduleId)
        {
            var labTestSchedule = _context.LabTestSchedules.Find(labTestScheduleId);
            if (labTestSchedule == null)
            {
                return false;
            }
            Remove(labTestSchedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<LabTestSchedule>> GetAllLabTestSchedulesAsync()
        {
            return await _context.LabTestSchedules
                .Include(l => l.TreatmentProcess)
                .Include(l => l.Doctor)
                .ToListAsync();
        }

        public async Task<LabTestSchedule> GetLabTestScheduleByIdAsync(int labTestScheduleId)
        {
            return await _context.LabTestSchedules
                .Include(l => l.TreatmentProcess)
                .Include(l => l.Doctor)
                .FirstOrDefaultAsync(l => l.LabTestScheduleId == labTestScheduleId);
        }

        public async Task<LabTestSchedule> UpdateLabTestScheduleAsync(LabTestSchedule labTestSchedule)
        {
            Update(labTestSchedule);
            await _context.SaveChangesAsync();
            return await GetLabTestScheduleByIdAsync(labTestSchedule.LabTestScheduleId) ?? labTestSchedule;
        }
    }
}

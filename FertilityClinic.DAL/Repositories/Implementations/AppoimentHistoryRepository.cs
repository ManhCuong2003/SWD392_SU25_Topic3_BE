using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class AppoimentHistoryRepository : GenericRepository<AppointmentHistory>, IAppoimentHistoryRepository
    {
        public readonly FertilityClinicDbContext _context;
        public AppoimentHistoryRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateAppointmentHistoryAsync(AppointmentHistory appointmentHistory)
        {
            var task = await _context.AddAsync(appointmentHistory);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<AppointmentHistory>> GetAllAppointmentHistoriesAsync()
        {
            return await _context.AppointmentHistories
                .ToListAsync();
        }

        public async Task<List<AppointmentHistory>> GetAppointmentHistoriesByUserIdAsync(int userId)
        {
            return await _context.AppointmentHistories
                .Where(ah => ah.UserId == userId)
                .OrderByDescending(ah => ah.CreatedAt) // Sắp xếp theo thời gian tạo (mới nhất trước)
                .ToListAsync();
        }

        public async Task<AppointmentHistory> GetAppointmentHistoryByIdAsync(int appointmentHistoryId)
        {
            return await _context.AppointmentHistories
                .FirstOrDefaultAsync(a => a.AppointmentHistoryId == appointmentHistoryId);
        }
    }
}

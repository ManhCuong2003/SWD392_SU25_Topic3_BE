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
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly FertilityClinicDbContext _context;

        public NotificationRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateNotificationAsync(Notification notification)
        {
            await AddAsync(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await GetByIdAsync(id);
            if (notification == null)
            {
                return false;
            }
            Remove(notification);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<Notification>> GetAllNotificationAsync()
        {
            return await _context.Notifications
                .Include(n => n.User)
                .ToListAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync();
        }

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            Update(notification);
            await _context.SaveChangesAsync();
            return await GetNotificationByIdAsync(notification.NotificationId) ?? notification;
        }
    }
}

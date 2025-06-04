using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class AppoimentRepository : GenericRepository<Appointment>, IAppoimentRepository
    {
        private readonly FertilityClinicDbContext _context;
        public AppoimentRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateAppointmentAsync(Appointment appointment)
        {
            await _context.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await GetByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }
            Remove(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Include(a => a.Partner)
                .Include(a => a.TreatmentMethod)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Include(a => a.Partner)
                .Include(a => a.TreatmentMethod)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<bool> IsAppointmentTimeConflictAsync(int doctorId, DateOnly appointmentDate, TimeOnly appointmentTime, int? excludeAppointmentId = null)
        {
            var query = _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                       a.AppointmentDate == appointmentDate &&
                       a.AppointmentTime == appointmentTime &&
                       a.Status != "Cancelled");
            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.AppointmentId != excludeAppointmentId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> IsPatientHasAppointmentOnDateAsync(int userId, DateOnly appointmentDate, int? excludeAppointmentId = null)
        {
            var query = _context.Appointments
            .Where(a => a.UserId == userId &&
                       a.AppointmentDate == appointmentDate &&
                       a.Status != "Cancelled"); // Không tính appointment đã hủy

            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.AppointmentId != excludeAppointmentId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            Update(appointment);
            await _context.SaveChangesAsync();
            return await GetAppointmentByIdAsync(appointment.AppointmentId) ?? appointment;
        }

        // Method để check time slot conflicts (với buffer time)
        public async Task<bool> IsTimeSlotConflictAsync(int doctorId, DateOnly appointmentDate, TimeOnly appointmentTime, int bufferMinutes = 30, int? excludeAppointmentId = null)
        {
            var startTime = appointmentTime.AddMinutes(-bufferMinutes);
            var endTime = appointmentTime.AddMinutes(bufferMinutes);

            var query = _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                           a.AppointmentDate == appointmentDate &&
                           a.AppointmentTime >= startTime &&
                           a.AppointmentTime <= endTime &&
                           a.Status != "Cancelled");

            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.AppointmentId != excludeAppointmentId.Value);
            }

            return await query.AnyAsync();
        }
    }

}

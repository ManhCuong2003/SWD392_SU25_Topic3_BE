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
                .Include(a => a.TreatmentMethod)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Include(a => a.TreatmentMethod)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            Update(appointment);
            await _context.SaveChangesAsync();
            return await GetAppointmentByIdAsync(appointment.AppointmentId) ?? appointment;
        }
    }

}

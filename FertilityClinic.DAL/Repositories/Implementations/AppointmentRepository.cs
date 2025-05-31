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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppoimentRepository
    {
        private readonly FertilityClinicDbContext _context;
        public AppointmentRepository(FertilityClinicDbContext context) : base(context)
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
            var appointment = _context.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }
            await _context.Remove(appointment);
            return true;
        }

        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }

}

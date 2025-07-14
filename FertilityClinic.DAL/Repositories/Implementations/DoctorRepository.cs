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
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly FertilityClinicDbContext _context;
        public DoctorRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateDoctorAsync(Doctor doctor)
        {
            await AddAsync(doctor);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await GetByIdAsync(id);
            if (doctor == null)
            {
                return false;
            }
           Remove(doctor);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
            .Include(d => d.User)
            .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task<Doctor?> GetDoctorByNameAsync(string name)
        {
            return await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.User.FullName.ToLower().Contains(name.ToLower()));
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            Update(doctor);
            await _context.SaveChangesAsync();
            return await GetDoctorByIdAsync(doctor.DoctorId) ?? doctor;
        }
        public async Task<Doctor?> GetDoctorByUserIdAsync(int userId)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

    }
}

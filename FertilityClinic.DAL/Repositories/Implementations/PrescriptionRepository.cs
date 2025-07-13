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
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        private readonly FertilityClinicDbContext _context;
        public PrescriptionRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatePrescriptionAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return false;
            }
            Remove(prescription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.TreatmentMethod)
                .Include(p => p.User)
                .Include(p => p.Doctor)
                .Include(p => p.Pill)
                .Include(p => p.Appointment)
                .ToListAsync();
        }

        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.TreatmentMethod)
                .Include(p => p.User)
                .Include(p => p.Doctor)
                .Include(p => p.Pill)
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription)
        {
            Update(prescription);
            await _context.SaveChangesAsync();
            return await GetPrescriptionByIdAsync(prescription.PrescriptionId) ?? prescription;
        }
    }
}

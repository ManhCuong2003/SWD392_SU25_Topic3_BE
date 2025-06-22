using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FertilityClinicDbContext _context;

        public PaymentRepository(FertilityClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<Payment> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
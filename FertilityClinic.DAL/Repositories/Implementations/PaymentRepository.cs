using FertilityClinic.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FertilityClinicDbContext _context;

        public PaymentRepository(FertilityClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<Payment> GetByOrderCodeAsync(long orderCode)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.OrderCode == orderCode);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await Task.CompletedTask;
        }
    }
}
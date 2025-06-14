using FertilityClinic.DAL.Models;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(int id);
        Task<Payment> GetByOrderCodeAsync(long orderCode);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
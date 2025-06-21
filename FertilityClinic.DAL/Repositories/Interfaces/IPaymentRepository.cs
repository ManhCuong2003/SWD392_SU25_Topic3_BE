using FertilityClinic.DAL.Models;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(int id);
        Task<Payment> GetByAppointmentIdAsync(int appointmentId);
        Task<Payment> CreateAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(int id);
    }
}
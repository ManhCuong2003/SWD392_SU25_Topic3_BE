using FertilityClinic.DAL.Models;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentForAppointment(int appointmentId, int amount, string description);
        Task<bool> VerifyPayment(long orderCode);
    }
}
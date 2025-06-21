using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Responses;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> GetPaymentByIdAsync(int id);
        Task<PaymentResponse> GetPaymentByAppointmentIdAsync(int appointmentId);
        Task<PaymentResponse> CreatePaymentAsync(int appointmentId, decimal amount, string paymentMethod);
        Task UpdatePaymentStatusAsync(int paymentId, string status);
        Task<string> VerifyPaymentAsync(int paymentId);
    }
}
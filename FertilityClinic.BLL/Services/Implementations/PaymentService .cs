using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Responses;
using System;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(int appointmentId, decimal amount, string paymentMethod)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new ArgumentException("Appointment not found");
            }

            var payment = new Payment
            {
                AppointmentId = appointmentId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                PaymentDate = DateTime.UtcNow,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Payments.CreateAsync(payment);
            await _unitOfWork.SaveAsync();

            return MapToPaymentResponse(payment);
        }

        public async Task<PaymentResponse> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
            {
                throw new ArgumentException("Payment not found");
            }

            return MapToPaymentResponse(payment);
        }

        public async Task<PaymentResponse> GetPaymentByAppointmentIdAsync(int appointmentId)
        {
            var payment = await _unitOfWork.Payments.GetByAppointmentIdAsync(appointmentId);
            if (payment == null)
            {
                throw new ArgumentException("Payment not found for this appointment");
            }

            return MapToPaymentResponse(payment);
        }

        public async Task UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new ArgumentException("Payment not found");
            }

            payment.Status = status;
            payment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Payments.UpdateAsync(payment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> VerifyPaymentAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new ArgumentException("Payment not found");
            }

            return payment.Status;
        }

        private PaymentResponse MapToPaymentResponse(Payment payment)
        {
            return new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                AppointmentId = payment.AppointmentId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt
            };
        }
    }
}
using Net.payOS;
using Net.payOS.Types;
using FertilityClinic.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PayOSService
    {
        private readonly PayOS _payOS;
        private readonly IUnitOfWork _unitOfWork;

        public PayOSService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _payOS = new PayOS(
                configuration["PayOS:ClientId"],
                configuration["PayOS:ApiKey"],
                configuration["PayOS:ChecksumKey"]
            );
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatePaymentResult> CreateAppointmentPayment(int appointmentId, string returnUrl, string cancelUrl)
        {
            // Lấy thông tin appointment
            var appointment = await _unitOfWork.Appointments.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null)
                throw new ArgumentException("Appointment not found");

            // Lấy thông tin treatment method để biết giá
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(appointment.TreatmentMethodId);
            if (treatmentMethod == null)
                throw new ArgumentException("Treatment method not found");

            // Tạo orderCode unique (có thể dùng appointmentId + timestamp)
            long orderCode = long.Parse($"{appointmentId}{DateTimeOffset.Now.ToUnixTimeSeconds()}");

            var paymentData = new PaymentData(
                orderCode: orderCode,
                amount: (int)treatmentMethod.Price, // Giả sử TreatmentMethod có Price
                description: $"Thanh toán lịch hẹn - {treatmentMethod.MethodName}",
                items: new List<ItemData>
                {
                    new ItemData(
                        name: treatmentMethod.MethodName,
                        quantity: 1,
                        price: (int)treatmentMethod.Price
                    )
                },
                cancelUrl: cancelUrl,
                returnUrl: returnUrl
            );

            var result = await _payOS.createPaymentLink(paymentData);

            // Lưu thông tin payment vào database
            await SavePaymentInfo(appointmentId, orderCode, (int)treatmentMethod.Price);

            return result;
        }

        public async Task<PaymentLinkInformation> GetPaymentInfo(long orderCode)
        {
            return await _payOS.getPaymentLinkInformation(orderCode);
        }

        public async Task<PaymentLinkInformation> CancelPayment(long orderCode, string reason = null)
        {
            return await _payOS.cancelPaymentLink(orderCode, reason);
        }

        public WebhookData VerifyPaymentWebhookData(WebhookType webhookBody)
        {
            return _payOS.verifyPaymentWebhookData(webhookBody);
        }

        private async Task SavePaymentInfo(int appointmentId, long orderCode, int amount)
        {
            // Tạo bảng Payment nếu chưa có
            var payment = new Payment
            {
                AppointmentId = appointmentId,
                OrderCode = orderCode,
                Amount = amount,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePaymentStatus(long orderCode, string status, string transactionId = null)
        {
            var payment = await _unitOfWork.Payments.GetByOrderCodeAsync(orderCode);
            if (payment != null)
            {
                payment.Status = status;
                payment.TransactionId = transactionId;
                payment.UpdatedAt = DateTime.Now;

                // Nếu thanh toán thành công, cập nhật trạng thái appointment
                if (status == "PAID")
                {
                    var appointment = await _unitOfWork.Appointments.GetByIdAsync(payment.AppointmentId);
                    if (appointment != null)
                    {
                        appointment.Status = "Confirmed"; // Xác nhận lịch hẹn khi thanh toán thành công
                        await _unitOfWork.Appointments.UpdateAppointmentAsync(appointment);
                    }
                }

                await _unitOfWork.SaveAsync();
            }
        }
    }
}
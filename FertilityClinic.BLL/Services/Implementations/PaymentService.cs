using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PaymentService: IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, HttpClient httpClient, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Payment> CreatePaymentForAppointment(int appointmentId, int amount, string description)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }

            // Tạo order code ngẫu nhiên
            var random = new Random();
            long orderCode = random.Next(100000, 999999);

            // Tạo payment request
            var paymentData = new
            {
                orderCode = orderCode,
                amount = amount,
                description = description,
                cancelUrl = "https://yourdomain.com/payment/cancel",
                returnUrl = "https://yourdomain.com/payment/success"
            };

            // Lấy cấu hình từ appsettings.json
            var payOSConfig = _configuration.GetSection("PayOS");
            var clientId = payOSConfig["ClientId"];
            var apiKey = payOSConfig["ApiKey"];

            // Gọi API PayOS
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api-merchant.payos.vn/v2/payment-requests");
            request.Headers.Add("x-client-id", clientId);
            request.Headers.Add("x-api-key", apiKey);

            request.Content = new StringContent(
                JsonSerializer.Serialize(paymentData),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create payment link");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var paymentResponse = JsonSerializer.Deserialize<PayOSResponse>(responseContent);

            // Lưu payment vào database
            var payment = new Payment
            {
                AppointmentId = appointmentId,
                OrderCode = orderCode,
                Amount = amount,
                Status = "PENDING",
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAsync();

            return payment;
        }

        public async Task<bool> VerifyPayment(long orderCode)
        {
            var payOSConfig = _configuration.GetSection("PayOS");
            var clientId = payOSConfig["ClientId"];
            var apiKey = payOSConfig["ApiKey"];

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-merchant.payos.vn/v2/payment-requests/{orderCode}");
            request.Headers.Add("x-client-id", clientId);
            request.Headers.Add("x-api-key", apiKey);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var paymentInfo = JsonSerializer.Deserialize<PayOSResponse>(responseContent);

            if (paymentInfo.data.status == "PAID")
            {
                var payment = await _unitOfWork.Payments.GetByOrderCodeAsync(orderCode);
                if (payment != null)
                {
                    payment.Status = "PAID";
                    await _unitOfWork.Payments.UpdateAsync(payment);
                    await _unitOfWork.SaveAsync();

                    // Cập nhật trạng thái appointment
                    var appointment = await _unitOfWork.Appointments.GetByIdAsync(payment.AppointmentId);
                    if (appointment != null)
                    {
                        appointment.Status = "Confirmed";
                        await _unitOfWork.Appointments.UpdateAppointmentAsync(appointment);
                        await _unitOfWork.SaveAsync();
                    }

                    return true;
                }
            }

            return false;
        }
    }

    public class PayOSResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public PaymentData data { get; set; }
    }

    public class PaymentData
    {
        public string status { get; set; }
        // Thêm các trường khác nếu cần
    }
}
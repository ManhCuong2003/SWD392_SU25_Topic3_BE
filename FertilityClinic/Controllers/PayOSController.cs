using System;
using System.Threading.Tasks;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;

namespace FertilityClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOSController : ControllerBase
    {
        private readonly IAppoimentService _appointmentService;
        private readonly ITreatmentMethodService _treatmentMethodService;
        private readonly PayOS _payOS;

        public PayOSController(
            IAppoimentService appointmentService,
            ITreatmentMethodService treatmentMethodService,
            PayOS payOS)
        {
            _appointmentService = appointmentService;
            _treatmentMethodService = treatmentMethodService;
            _payOS = payOS;
        }

        [HttpPost("create-payment/{appointmentId}")]
        public async Task<IActionResult> CreatePayment(int appointmentId)
        {
            try
            {
                // Get appointment info
                var appointmentResponse = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
                var treatmentMethod = await _treatmentMethodService.GetTreatmentMethodByIdAsync(appointmentResponse.TreatmentMethodId);

                // Generate random order code
                var random = new Random();
                long orderCode = random.Next(100000, 999999);

                // Create payment data using constructor
                var paymentData = new PaymentData(
                    orderCode: orderCode,
                    amount: (int)treatmentMethod.Price,
                    description: $"Thanh toán cho lịch hẹn #{appointmentId} - Phương pháp {treatmentMethod.MethodName}",
                    items: new List<ItemData>
                    {
                new ItemData(
                    name: treatmentMethod.MethodName,
                    quantity: 1,
                    price: (int)treatmentMethod.Price
                )
                    },
                    cancelUrl: "https://your-clinic.com/payment/cancel",
                    returnUrl: "https://your-clinic.com/payment/success",
                    buyerName: appointmentResponse.PatientName,
                    buyerEmail: "patient@example.com",
                    buyerPhone: appointmentResponse.PhoneNumber
                );

                // Create payment link with PayOS
                var createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new
                {
                    Success = true,
                    Message = "Tạo link thanh toán thành công",
                    PaymentUrl = createPayment.checkoutUrl,
                    AppointmentId = appointmentId,
                    Amount = treatmentMethod.Price
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = $"Lỗi khi tạo link thanh toán: {ex.Message}"
                });
            }
        }

        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] int orderCode)
        {
            try
            {
                // Xác minh payment với PayOS
                var paymentInfo = await _payOS.getPaymentLinkInformation(orderCode);

                if (paymentInfo.status == "PAID")
                {
                    // TODO: Cập nhật trạng thái appointment thành "Đã thanh toán"
                    // Có thể lưu thêm thông tin thanh toán vào database nếu cần

                    return Redirect("https://your-clinic.com/payment/success");
                }

                return Redirect("https://your-clinic.com/payment/failed");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = $"Lỗi khi xử lý callback: {ex.Message}"
                });
            }
        }

        [HttpGet("check-payment/{appointmentId}")]
        public async Task<IActionResult> CheckPaymentStatus(int appointmentId)
        {
            try
            {
                // TODO: Kiểm tra trạng thái thanh toán từ database hoặc PayOS
                // Trả về trạng thái thanh toán hiện tại của appointment

                return Ok(new
                {
                    Success = true,
                    AppointmentId = appointmentId,
                    PaymentStatus = "PAID" // hoặc "UNPAID" tùy thuộc vào trạng thái thực tế
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = $"Lỗi khi kiểm tra trạng thái thanh toán: {ex.Message}"
                });
            }
        }
    }
}
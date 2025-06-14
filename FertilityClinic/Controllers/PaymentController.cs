using Microsoft.AspNetCore.Mvc;
using FertilityClinic.BLL.Services.Implementations;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create/{appointmentId}")]
        public async Task<IActionResult> CreatePayment(int appointmentId, [FromBody] int amount)
        {
            try
            {
                var payment = await _paymentService.CreatePaymentForAppointment(
                    appointmentId,
                    amount,
                    $"Thanh toán cho lịch hẹn #{appointmentId}");

                return Ok(new
                {
                    PaymentId = payment.PaymentId,
                    Amount = payment.Amount,
                    Status = payment.Status,
                    PaymentUrl = $"https://pay.payos.vn/web/{payment.OrderCode}" // URL thanh toán thực tế
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify/{orderCode}")]
        public async Task<IActionResult> VerifyPayment(long orderCode)
        {
            try
            {
                var isVerified = await _paymentService.VerifyPayment(orderCode);
                return Ok(new { IsPaid = isVerified });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
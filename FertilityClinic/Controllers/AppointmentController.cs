using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class AppointmentController : Controller
    {
        private readonly IAppoimentService _appointmentService;
        private readonly IPayOSService _payOSService;

        public AppointmentController(IAppoimentService appointmentService, IPayOSService payOSService)
        {
            _appointmentService = appointmentService;
            _payOSService = payOSService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            try
            {
                var paymentResponse = await _payOSService.CreatePaymentLinkAsync(request);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("payment-status/{orderId}")]
        public async Task<IActionResult> CheckPaymentStatus(string orderId)
        {
            try
            {
                var status = await _payOSService.CheckPaymentStatusAsync(orderId);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost(APIEndPoints.Appointment.Create)]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest appointmentRequest, int userId, int doctorId/*, int partnerId*/, int treatmentMethodid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(appointmentRequest, userId, doctorId/*, partnerId*/, treatmentMethodid);
                return Ok(appointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid appointment data",
                    Detailed = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new
                {
                    StatusCode = 422,
                    Message = "Failed to process appointment request",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while creating the appointment",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete(APIEndPoints.Appointment.Delete)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }
            try
            {
                var result = await _appointmentService.DeleteAppointmentAsync(id);
                if (result)
                {
                    return Ok($"Appointment with ID {id} deleted successfully.");
                }
                else
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting appointment: {ex.Message}");
            }
        }

        [HttpGet(APIEndPoints.Appointment.GetAll)]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving appointments: {ex.Message}");
            }
        }

        [HttpGet(APIEndPoints.Appointment.GetById)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                else
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving appointment: {ex.Message}");
            }
        }

        [HttpPut(APIEndPoints.Appointment.Update)]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest appointmentRequest)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest("Invalid appointment ID or request data");
            }
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, appointmentRequest);
                return Ok(updatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating appointment: {ex.Message}");
            }
        }
    }
}

using System.Security.Claims;
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

        public AppointmentController(IAppoimentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost(APIEndPoints.Appointment.Create)]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest appointmentRequest, int userId, int doctorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(appointmentRequest, userId, doctorId);
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
        [HttpGet("api/appointments/user")]
        public async Task<IActionResult> GetAppointmentsByCurrentUser()
        {
            try
            {
                // Lấy userId từ token
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);

                // Gọi service để lấy tất cả cuộc hẹn theo userId
                var appointments = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving user's appointments: {ex.Message}");
            }
        }

    }
}

using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    [Route("api/[controller]")]
    public class AppointmentHistoryController : ControllerBase
    {
        private readonly IAppoimentHistoryService _appointmentHistoryService;

        public AppointmentHistoryController(IAppoimentHistoryService appointmentHistoryService)
        {
            _appointmentHistoryService = appointmentHistoryService;
        }

        // Endpoint để lấy toàn bộ appointment history
        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentHistories()
        {
            try
            {
                var appointmentHistories = await _appointmentHistoryService.GetAllAppointmentHistoriesAsync();
                if (appointmentHistories == null || !appointmentHistories.Any())
                {
                    return NotFound("No appointment histories found.");
                }
                return Ok(appointmentHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint để lấy appointment history theo userId (đã có trong file của bạn)
        [HttpGet(APIEndPoints.AppointmentHistory.GetByUserId)]
        public async Task<IActionResult> GetAllAppointmentHistoryByUserId(int userId)
        {
            try
            {
                var appointmentHistories = await _appointmentHistoryService.GetAllAppointmentHistoriesByUserAsync(userId);
                if (appointmentHistories == null || !appointmentHistories.Any())
                {
                    return NotFound("No appointment histories found for the specified user.");
                }
                return Ok(appointmentHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint để lấy appointment history theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentHistoryById(int id)
        {
            try
            {
                var appointmentHistory = await _appointmentHistoryService.GetAppointmentHistoryByIdAsync(id);
                if (appointmentHistory == null)
                {
                    return NotFound($"Appointment history with ID {id} not found.");
                }
                return Ok(appointmentHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
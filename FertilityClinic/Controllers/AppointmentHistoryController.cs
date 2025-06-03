using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    public class AppointmentHistoryController : ControllerBase
    {
        private readonly IAppoimentHistoryService _appointmentHistoryService;
        public AppointmentHistoryController(IAppoimentHistoryService appointmentHistoryService)
        {
            _appointmentHistoryService = appointmentHistoryService;
        }
    
        [HttpGet(APIEndPoints.AppointmentHistory.GetByUserId)]
        public async Task<IActionResult> GetAllAppointmentHistoryByUserId(int userId)
        {
            try
            {
                var appointmentHistories = await _appointmentHistoryService.GetAllAppointmentHistoriesByUserAsync(userId);
                if(appointmentHistories == null) 
                {
                    return BadRequest("No appointment histories found for the specified user.");
                }
                return Ok(appointmentHistories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

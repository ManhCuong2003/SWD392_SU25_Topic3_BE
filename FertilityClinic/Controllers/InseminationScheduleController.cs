using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class InseminationScheduleController : ControllerBase
    {
        private readonly IInseminationScheduleService _inseminationScheduleService;
        public InseminationScheduleController(IInseminationScheduleService inseminationScheduleService)
        {
            _inseminationScheduleService = inseminationScheduleService;
        }
        [HttpPost(APIEndPoints.InseminationSchedule.Create)]
        public async Task<IActionResult> CreateInseminationSchedule([FromBody] InseminationScheduleRequest request, int doctorId, int treatmentProcessId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var schedule = await _inseminationScheduleService.CreateInseminationScheduleAsync(request, doctorId, treatmentProcessId);
                return Ok(schedule);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid insemination schedule data",
                    Detailed = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new
                {
                    StatusCode = 422,
                    Message = "Failed to process insemination schedule request",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while creating the insemination schedule",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpGet(APIEndPoints.InseminationSchedule.GetById)]
        public async Task<IActionResult> GetInseminationSchedule(int id)
        {
            try
            {
                var schedule = await _inseminationScheduleService.GetInseminationScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound(new { Message = "Insemination schedule not found" });
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving the insemination schedule",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        [HttpDelete(APIEndPoints.InseminationSchedule.Delete)]
        public async Task<IActionResult> DeleteInseminationSchedule(int id)
        {
            try
            {
                var result = await _inseminationScheduleService.DeleteInseminationScheduleAsync(id);
                if (!result)
                {
                    return NotFound(new { Message = "Insemination schedule not found" });
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while deleting the insemination schedule",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        [HttpGet(APIEndPoints.InseminationSchedule.GetAll)]
        public async Task<IActionResult> GetAllInseminationSchedules()
        {
            try
            {
                var schedules = await _inseminationScheduleService.GetAllInseminationSchedulesAsync();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving all insemination schedules",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}

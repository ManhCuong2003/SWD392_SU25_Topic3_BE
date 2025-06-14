using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class LabTestScheduleController : ControllerBase
    {
        private readonly ILabTestScheduleService _labTestScheduleService;
        public LabTestScheduleController(ILabTestScheduleService labTestScheduleService)
        {
            _labTestScheduleService = labTestScheduleService;
        }

        [HttpGet("GetAllLabTestSchedules")]
        public async Task<IActionResult> GetAllLabTestSchedules()
        {
            try
            {
                var labTestSchedules = await _labTestScheduleService.GetAllLabTestSchedulesAsync();
                return Ok(labTestSchedules);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving lab test schedules",
                    Detailed = ex.Message
                });
            }
        }

        [HttpGet("GetLabTestScheduleById/{id}")]
        public async Task<IActionResult> GetLabTestScheduleById(int id)
        {
            try
            {
                var labTestSchedule = await _labTestScheduleService.GetLabTestScheduleByIdAsync(id);
                return Ok(labTestSchedule);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Lab test schedule not found",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving the lab test schedule",
                    Detailed = ex.Message
                });
            }
        }


        [HttpPost("CreateLabTestSchedule/{doctorId}/{treatmentProcessId}")]
        public async Task<IActionResult> CreateLabTestSchedule(
     [FromBody] LabTestScheduleRequest labTestSchedule,
     [FromRoute] int doctorId,
     [FromRoute] int treatmentProcessId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdLabTestSchedule = await _labTestScheduleService
                    .CreateLabTestScheduleAsync(labTestSchedule, doctorId, treatmentProcessId);

                return CreatedAtAction(nameof(GetLabTestScheduleById),
                    new { id = createdLabTestSchedule.LabTestScheduleId },
                    createdLabTestSchedule);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid lab test schedule data",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while creating the lab test schedule",
                    Detailed = ex.Message
                });
            }
        }


        [HttpPut("UpdateLabTestSchedule/{id}")]
        public async Task<IActionResult> UpdateLabTestSchedule(int id, [FromBody] UpdateLabTestScheduleRequest labTestSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedLabTestSchedule = await _labTestScheduleService.UpdateLabTestScheduleAsync(id, labTestSchedule);
                if (updatedLabTestSchedule == null)
                {
                    return NotFound(new { Message = "Lab test schedule not found" });
                }
                return Ok(updatedLabTestSchedule);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid lab test schedule data",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while updating the lab test schedule",
                    Detailed = ex.Message
                });
            }
        }

        [HttpDelete("DeleteLabTestSchedule/{id}")]
        public async Task<IActionResult> DeleteLabTestSchedule(int id)
        {
            try
            {
                var result = await _labTestScheduleService.DeleteLabTestScheduleAsync(id);
                if (!result)
                {
                    return NotFound(new { Message = "Lab test schedule not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while deleting the lab test schedule",
                    Detailed = ex.Message
                });
            }
        }
    }
}

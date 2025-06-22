using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class InseminationResultController : ControllerBase
    {
        private readonly IInseminationResultService _inseminationResultService;
        public InseminationResultController(IInseminationResultService inseminationResultService)
        {
            _inseminationResultService = inseminationResultService;
        }

        [HttpGet("GetAllInseminationResults")]
        public async Task<IActionResult> GetAllInseminationResults()
        {
            try
            {
                var results = await _inseminationResultService.GetAllInseminationResultsAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving insemination results",
                    Detailed = ex.Message
                });
            }
        }

        [HttpGet("GetInseminationResultById/{id}")]
        public async Task<IActionResult> GetInseminationResultById(int id)
        {
            try
            {
                var result = await _inseminationResultService.GetInseminationResultByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Insemination result not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving the insemination result",
                    Detailed = ex.Message
                });
            }
        }

        [HttpPost("CreateInseminationResult/{inseminationScheduleId}/{doctorId}")]
        public async Task<IActionResult> CreateInseminationResult(int inseminationScheduleId, int doctorId, [FromBody] InseminationResultRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid request data"
                });
            }
            try
            {
                var result = await _inseminationResultService.CreateInseminationResultAsync(request, inseminationScheduleId, doctorId);
                return CreatedAtAction(nameof(GetInseminationResultById), new { id = result.InseminationResultId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while creating the insemination result",
                    Detailed = ex.Message
                });
            }
        }

        [HttpDelete("DeleteInseminationResult/{id}")]

        public async Task<IActionResult> DeleteInseminationResult(int id)
        {
            try
            {
                var deleted = await _inseminationResultService.DeleteInseminationResultAsync(id);
                if (!deleted)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Insemination result not found"
                    });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while deleting the insemination result",
                    Detailed = ex.Message
                });
            }
        }
    }
}
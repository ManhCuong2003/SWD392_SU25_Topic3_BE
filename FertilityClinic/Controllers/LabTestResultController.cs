using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class LabTestResultController : ControllerBase
    {
        private readonly ILabTestResultService _labTestResultService;
        public LabTestResultController(ILabTestResultService labTestResultService)
        {
            _labTestResultService = labTestResultService;
        }

        [HttpGet("GetAllLabTestResults")]

        public async Task<IActionResult> GetAllLabTestResults()
        {
            try
            {
                var labTestResults = await _labTestResultService.GetAllLabTestResultsAsync();
                return Ok(labTestResults);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving lab test results",
                    Detailed = ex.Message
                });
            }
        }

        [HttpGet("GetLabTestResultById/{id}")]

        public async Task<IActionResult> GetLabTestResultById(int id)
        {
            try
            {
                var labTestResult = await _labTestResultService.GetLabTestResultByIdAsync(id);
                return Ok(labTestResult);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Lab test result not found",
                    Detailed = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving the lab test result",
                    Detailed = ex.Message
                });
            }
        }

        [HttpPost("CreateLabTestResult/{doctorId}")]
        public async Task<IActionResult> CreateLabTestResult(int doctorId, [FromBody] LabTestResultRequest labTestResultRequest)
        {
            try
            {
                if (labTestResultRequest == null)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Invalid lab test result data"
                    });
                }
                var createdLabTestResult = await _labTestResultService.CreateLabTestResultAsync(labTestResultRequest, doctorId);
                return CreatedAtAction(nameof(GetLabTestResultById), new { id = createdLabTestResult.LabTestResultId }, createdLabTestResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while creating the lab test result",
                    Detailed = ex.Message
                });
            }
        }

        [HttpDelete("DeleteLabTestResult/{id}")]
        public async Task<IActionResult> DeleteLabTestResult(int id)
        {
            try
            {
                var result = await _labTestResultService.DeleteLabTestResultAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Lab test result not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while deleting the lab test result",
                    Detailed = ex.Message
                });
            }
        }
    }
}

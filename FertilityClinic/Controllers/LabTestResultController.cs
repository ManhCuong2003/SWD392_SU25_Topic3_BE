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
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("api/LabTestResult/GetAll")]

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
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("api/LabTestResult/GetById/{id}")]

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
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost("api/LabTestResult/Create")]
        public async Task<IActionResult> CreateLabTestResult(int userId, [FromBody] LabTestResultRequest labTestResultRequest)
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
                var createdLabTestResult = await _labTestResultService.CreateLabTestResultAsync(labTestResultRequest, userId);
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
        [Authorize(Roles = "Admin,Doctor")]
        [HttpDelete("api/LabTestResult/Delete/{id}")]
        public async Task<IActionResult> DeleteLabTestResult(int id)
{
    try
    {
        var result = await _labTestResultService.DeleteLabTestResultAsync(id);

        if (result)
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lab test result deleted successfully",
                DeletedId = id
            });
        }

        return StatusCode(500, new
        {
            StatusCode = 500,
            Message = "Failed to delete lab test result"
        });
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new
        {
            StatusCode = 404,
            Message = ex.Message
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            StatusCode = 500,
            Message = "An unexpected error occurred while deleting the lab test result",
            Detailed = ex.Message
        });
    }
}
        [Authorize(Roles = "Admin,Doctor,User")]
        [HttpGet("api/LabTestResult/GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _labTestResultService.GetLabTestResultsByUserIdAsync(userId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Doctor,User")]
        [HttpPut("api/LabTestResult/UpdateByUserId/{userId}")]
        public async Task<IActionResult> UpdateLabTestResult(int Id, [FromBody] LabTestResultUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = await _labTestResultService.UpdateLabTestResultAsync(Id, request);

                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            }
        }
    }
}


using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class TreatmentProcessController : ControllerBase
    {
        private readonly ITreatmentProcessService _treatmentProcessService;
        public TreatmentProcessController(ITreatmentProcessService treatmentProcessService)
        {
            _treatmentProcessService = treatmentProcessService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("api/TreatmentProcesses/Create")]
        public async Task<IActionResult> CreateTreatmentProcess([FromBody] TreatmentProcessRequest request, int userId, int treatmentMethodId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var process = await _treatmentProcessService.CreateTreatmentProcessAsync(request, userId, treatmentMethodId);
                return Ok(process);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating treatment process: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("api/TreatmentProcesses/GetAll")]
        public async Task<IActionResult> GetAllTreatmentProcesses()
        {
            try
            {
                var processes = await _treatmentProcessService.GetAllTreatmentProcessesAsync();
                return Ok(processes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving treatment processes: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("api/TreatmentProcesses/Delete/{id}")]
        public async Task<IActionResult> DeleteTreatmentProcess(int id)
        {
            try
            {
                var result = await _treatmentProcessService.DeleteTreatmentProcessAsync(id);
                if (result)
                    return Ok("Treatment process deleted successfully.");
                else
                    return NotFound("Treatment process not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting treatment process: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("api/TreatmentProcesses/GetById/{id}")]
        public async Task<IActionResult> GetTreatmentProcessById(int id)
        {
            try
            {
                var process = await _treatmentProcessService.GetTreatmentProcessByIdAsync(id);
                if (process == null)
                    return NotFound($"Treatment process with ID {id} not found.");
                return Ok(process);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving treatment process: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("api/TreatmentProcesses/Update/{id}")]
        public async Task<IActionResult> UpdateTreatmentProcess(int id, [FromBody] UpdateTreatmentProcessRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedProcess = await _treatmentProcessService.UpdateTreatmentProcessAsync(id, request);
                return Ok(updatedProcess);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating treatment process: {ex.Message}");
            }
        }
    }
}

using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class TreatmentMethodController : ControllerBase
    {
        private readonly ITreatmentMethodService _treatmentMethodService;
        public TreatmentMethodController(ITreatmentMethodService treatmentMethodService)
        {
            _treatmentMethodService = treatmentMethodService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("api/TreatmentMethods/Create")]
        public async Task<IActionResult> CreateTreatmentMethod([FromBody] TreatmentMethodRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var treatmentMethod = await _treatmentMethodService.CreateTreatmentMethodAsync(request);
                return Ok(treatmentMethod);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating treatment method: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("api/TreatmentMethods/GetAll")]
        public async Task<IActionResult> GetAllTreatmentMethods()
        {
            try
            {
                var treatmentMethods = await _treatmentMethodService.GetAllTreatmentMethodsAsync();
                return Ok(treatmentMethods);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving treatment methods: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("api/TreatmentMethods/GetById/{id}")]
        public async Task<IActionResult> GetTreatmentMethodById(int id)
        {
            try
            {
                var treatmentMethod = await _treatmentMethodService.GetTreatmentMethodByIdAsync(id);
                if (treatmentMethod == null)
                    return NotFound($"Treatment method with ID {id} not found.");
                return Ok(treatmentMethod);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving treatment method: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("api/TreatmentMethods/Delete/{id}")]
        public async Task<IActionResult> DeleteTreatmentMethod(int id)
        {
            try
            {
                var result = await _treatmentMethodService.DeleteTreatmentMethodAsync(id);
                if (!result)
                    return NotFound($"Treatment method with ID {id} not found.");
                return Ok(new { Message = $"Phương pháp điều trị với ID {id} đã được xóa thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting treatment method: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("api/TreatmentMethods/Update/{id}")]
        public async Task<IActionResult> UpdateTreatmentMethod(int id, [FromBody] TreatmentMethodRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedTreatmentMethod = await _treatmentMethodService.UpdateTreatmentMethodAsync(id, request);
                return Ok(updatedTreatmentMethod);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating treatment method: {ex.Message}");
            }
        }
    }
}

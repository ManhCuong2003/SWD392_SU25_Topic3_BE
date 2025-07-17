using FertilityClinic.BLL.Services.Implementations;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Requests.FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost("api/Prescriptions/Create")]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest request, int UserId)
        {
            var result = await _prescriptionService.CreatePrescriptionAsync(request, UserId);
            return Ok(result); // trả về PrescriptionResponse
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("api/Prescriptions/GetAll")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            try
            {
                var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving prescriptions: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("api/Prescriptions/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var result = await _prescriptionService.GetPrescriptionByIdAsync(id);
            if (result == null)
            {
                return NotFound(); // nếu không tìm thấy Prescription
            }
            return Ok(result); // trả về PrescriptionResponse
        }

        /*[Authorize(Roles = "Admin, Doctor")]
        [HttpPut("api/Prescriptions/Update")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] UpdatePrescriptionRequest request)
        {
            try
            {
                var updatedPrescription = await _prescriptionService.UpdatePrescriptionAsync(id, request);
                return Ok(updatedPrescription); // trả về PrescriptionResponse
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating prescription: {ex.Message}");
            }
        }
        */
        [Authorize(Roles = "Admin, Doctor")]
        [HttpDelete("api/Prescriptions/Delete")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            try
            {
                var result = await _prescriptionService.DeletePrescriptionAsync(id);
                if (result)
                {
                    return Ok(new { message = "Prescription deleted successfully." });
                }
                return NotFound(new { message = "Prescription not found." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting prescription: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("api/Prescriptions/user/{userId}")]
        public async Task<IActionResult> GetPrescriptionsByUserId(int userId)
        {
            var result = await _prescriptionService.GetPrescriptionsByUserIdAsync(userId);
            return Ok(result);
        }

    }
}

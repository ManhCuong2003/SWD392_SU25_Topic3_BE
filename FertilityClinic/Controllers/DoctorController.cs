using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(APIEndPoints.Doctor.Create)]
        public async Task<IActionResult> CreateDoctor([FromRoute] int id, [FromBody] DoctorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var doctor = await _doctorService.CreateDoctorAsync(request, id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating doctor: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(APIEndPoints.Doctor.GetAll)]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving doctors: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(APIEndPoints.Doctor.Delete)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var result = await _doctorService.DeleteDoctorAsync(id);
                if (result)
                    return Ok($"Doctor with ID {id} deleted successfully.");
                else
                    return NotFound($"Doctor with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting doctor: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut(APIEndPoints.Doctor.Update)]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedDoctor = await _doctorService.UpdateDoctorAsync(id, request);
                return Ok(updatedDoctor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating doctor: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(APIEndPoints.Doctor.GetById)]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                if (doctor == null)
                    return NotFound($"Doctor with ID {id} not found.");
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving doctor: {ex.Message}");
            }

        }
    }
}

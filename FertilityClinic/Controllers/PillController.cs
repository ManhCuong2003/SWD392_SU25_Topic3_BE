using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class PillController : Controller
    {
        private readonly IPillService _pillService;
        public PillController(IPillService pillService)
        {
            _pillService = pillService;
        }
        public async Task<IActionResult> CreatePill([FromBody] PillRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(pill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating pill: {ex.Message}");
            }
        }
        {
            try
            {
            }
            catch (Exception ex)
            {
            }
        }
        {
            try
            {
            }
            catch (Exception ex)
            {
            }
        }
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
            }
            catch (Exception ex)
            {
            }
        }
        {
            try
            {
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting pill: {ex.Message}");
            }
        }
        [HttpPut("{pillId}")]
        public async Task<IActionResult> UpdatePill(int pillId, [FromBody] UpdatePillRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedPill = await _pillService.UpdatePillAsync(request, pillId);
                if (updatedPill == null)
                    return NotFound($"Pill with ID {pillId} not found.");
                return Ok(updatedPill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating pill: {ex.Message}");
            }
        }
    }
}

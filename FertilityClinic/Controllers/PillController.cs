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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePill([FromBody] PillRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var pill = await _pillService.AddPillAsync(request);
                return Ok(pill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating pill: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{pillId}")]
        public async Task<IActionResult> GetPillById(int pillId)
        {
            try
            {
                var pill = await _pillService.GetPillByIdAsync(pillId);
                if (pill == null)
                    return NotFound($"Pill with ID {pillId} not found.");
                return Ok(pill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving pill: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchPills([FromQuery] string name)
        {
            try
            {
                var pills = await _pillService.GetPillsByNameAsync(name);
                return Ok(pills);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching pills: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllPills()
        {
            try
            {
                var pills = await _pillService.GetAllPillsAsync();
                return Ok(pills);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving doctors: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{pillId}")]
        public async Task<IActionResult> DeletePill(int pillId)
        {
            try
            {
                var result = await _pillService.DeletePillAsync(pillId);
                if (result)
                    return Ok($"Pill with ID {pillId} deleted successfully.");
                else
                    return NotFound($"Pill with ID {pillId} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting pill: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
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
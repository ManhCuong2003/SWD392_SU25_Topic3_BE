using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class PillController : ControllerBase
    {
        private readonly IPillService _pillService;
        public PillController(IPillService pillService)
        {
            _pillService = pillService;
        }
        [HttpPost("api/Pills/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePill([FromBody] PillRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var pill = await _pillService.CreatePillAsync(request);
                return Ok(pill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating pill: {ex.Message}");
            }
        }

        [HttpGet("api/Pills/GetAll")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllPills()
        {
            try
            {
                var pills = await _pillService.GetAllPillsAsync();
                return Ok(pills);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving pills: {ex.Message}");
            }
        }
        [HttpGet("api/Pills/GetById/{pillid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetPillById(int pillid)
        {
            try
            {
                var pill = await _pillService.GetPillByIdAsync(pillid);
                if (pill == null)
                    return NotFound($"Pill with ID {pillid} not found.");
                return Ok(pill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving pill: {ex.Message}");
            }
        }
        [HttpPut("api/Pills/Update/{pillid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdatePill(int pillid, [FromBody] PillRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedPill = await _pillService.UpdatePillAsync(pillid, request);
                return Ok(updatedPill);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating pill: {ex.Message}");
            }
        }
        [HttpDelete("api/Pills/Delete/{pillid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeletePill(int pillid)
        {
            try
            {
                var result = await _pillService.DeletePillAsync(pillid);
                if (!result)
                    return NotFound($"Pill with ID {pillid} not found.");
                return Ok($"Pill with ID {pillid} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting pill: {ex.Message}");
            }
        }
    }
}

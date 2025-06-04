using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FertilityClinic.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        
        [Authorize(Roles = "Admin,User")]
        [HttpPost("api/Partners/Create")]
        public async Task<IActionResult> CreatePartner(int userId, [FromBody] PartnerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // Lấy ID của user hiện tại từ token - sử dụng ClaimTypes.NameIdentifier
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Kiểm tra quyền
                if (userRole == "User" && currentUserId != userId)
                {
                    return Forbid("User can only create partner for themselves");
                }
                var partner = await _partnerService.CreatePartnerAsync(request, userId);
                return Ok(partner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating partner: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/Partners/GetAll")]
        public async Task<IActionResult> GetAllPartners()
        {
            try
            {
                var partners = await _partnerService.GetAllPartnersAsync();
                return Ok(partners);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving partners: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete("api/Partners/Delete/{id}")]
        public async Task<IActionResult> DeletePartner(int id)
        {
            try
            {
                var result = await _partnerService.DeletePartnerAsync(id);
                if (result)
                    return Ok($"Partner with ID {id} deleted successfully.");
                else
                    return NotFound($"Partner with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting partner: {ex.Message}");
            }
        }
        [HttpGet("api/Partners/GetById/{id}")]
        public async Task<IActionResult> GetPartnerById(int id)
        {
            try
            {
                var partner = await _partnerService.GetPartnerByIdAsync(id);
                if (partner == null)
                    return NotFound($"Partner with ID {id} not found.");
                return Ok(partner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving partner: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut("api/Partners/Update/{id}")]
        public async Task<IActionResult> UpdatePartner(int id, [FromBody] UpdatePartnerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedPartner = await _partnerService.UpdatePartnerAsync(id, request);
                return Ok(updatedPartner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating partner: {ex.Message}");
            }
        }
    }
}

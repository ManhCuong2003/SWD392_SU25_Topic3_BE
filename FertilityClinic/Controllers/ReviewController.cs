using FertilityClinic.BLL.Services.Implementations;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPost("api/Reviews/Create")]

        public async Task<IActionResult> CreateReview (int userId,int doctorId ,[FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var review = await _reviewService.CreateReviewAsync(request, userId, doctorId);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating Review: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/Reviews/GetAll")]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving reviews: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/Reviews/GetById")]

        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                if (review == null)
                    return NotFound($"Review with ID {id} not found.");
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving review: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("api/Review/Delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewService.DeleteReviewAsync(id);
                if (review)
                    return Ok($"Review with ID {id} deleted successfully.");
                else
                    return NotFound($"Review with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting review: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut("api/Review/Update/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedReview = await _reviewService.UpdateReviewAsync(id, request);
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating review: {ex.Message}");
            }
        }
    }
}

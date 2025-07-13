using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost("api/Blogs/Create")]
        public async Task<IActionResult> CreateBlog(int userId, [FromBody] BlogRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var blog = await _blogService.CreateBlogAsync(request, userId);
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating blog: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("api/Blogs/GetAll")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogsAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving blogs: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("api/Blogs/GetById")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            try
            {
                var blog = await _blogService.GetBlogByIdAsync(id);
                if (blog == null)
                    return NotFound($"Blog with ID {id} not found.");
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving blog: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpDelete("api/Blogs/Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var result = await _blogService.DeleteBlogAsync(id);
                if (!result)
                    return NotFound($"Blog with ID {id} not found.");
                return Ok($"Blog with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting blog: {ex.Message}");
            }

        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("api/Blogs/Update")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var blog = await _blogService.UpdateBlogAsync(id, request);
                if (blog == null)
                    return NotFound($"Blog with ID {id} not found.");
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating blog: {ex.Message}");
            }
        }
    }
}
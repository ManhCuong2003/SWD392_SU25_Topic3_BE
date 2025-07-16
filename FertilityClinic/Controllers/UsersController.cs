using System.Security.Claims;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FertilityClinic.Controllers
{
    
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Update a user's information
        /// </summary>
        ///
        [Authorize]
        [HttpPut]
        [Route(APIEndPoints.Users.Update)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _userService.UpdateUserAsync(dto);
                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Deletes a user 
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route(APIEndPoints.Users.Delete)]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {

            try
            {
                if (id <= 0)
                    return BadRequest("Invalid user ID");

                var success = await _userService.SoftDeleteUserAsync(id);
                return success ? Ok("User deleted successfully") : NotFound("User not found or already deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Hard deletes a user from database
        /// </summary>
        /// <param name="userId">The ID of the user to delete</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route(APIEndPoints.Users.HardDelete)]
        public async Task<IActionResult> HardDeleteUser(int userId)
        {
            try
            {
                if (userId <= 0)
                    return BadRequest("Invalid user ID");
                var success = await _userService.HardDeleteUserAsync(userId);
                return success ? Ok("User deleted successfully") : NotFound("User not found or already deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all active users
        /// </summary>
        /// <returns>List of users</returns>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(APIEndPoints.Users.GetAll)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID 
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// 
        [Authorize]
        [HttpGet]
        [Route(APIEndPoints.Users.GetById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route(APIEndPoints.Users.GetByEmail)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        //[Authorize(Roles = "Admin, User")]
        //[HttpGet]
        //[Route(APIEndPoints.Users.GetAllPatients)]
        //public async Task<IActionResult> GetAllPatients()
        //{
        //    var users = await _userService.GetAllPatientAsync();
        //    return Ok(users);
        //}
        [Authorize(Roles = "Doctor")]
        [HttpGet("api/users/get-all-users-by-appointments")]
        public async Task<IActionResult> GetUsersByCurrentDoctor()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var users = await _userService.GetUsersByCurrentDoctorAsync(userId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}

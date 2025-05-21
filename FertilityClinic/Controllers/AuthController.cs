using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FertilityClinic.Controllers
{

    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        /// <summary>
        /// Login a user 
        /// </summary>
        /// 
        [HttpPost]

        [Route(APIEndPoints.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Login failed.");
                return Unauthorized(new { message = "Invalid email or password" });
            }
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost]
        [Route(APIEndPoints.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException != null)
                    errorMsg += " Inner exception: " + ex.InnerException.Message;

                return BadRequest(new { message = errorMsg });
            }

        }

    }
}
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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/Notifications/GetAll")]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                var notification = await _notificationService.GetAllNotificationsAsync();
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving notifications: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/Notification/GetById")]

        public async Task<IActionResult> GetNotificationById(int id)
        {
            try
            {
                var notification = await _notificationService.GetNotificationByIdAsync(id);
                if (notification == null)
                    return NotFound($"Notification with ID {id} not found.");
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving notification: {ex.Message}");
            }
        }
            [Authorize(Roles = "Admin,User")]
            [HttpDelete("api/Notification/Delete/{id}")]
            public async Task<IActionResult> DeleteNotification(int id)
            {
                try
                {
                    var notification = await _notificationService.DeleteNotificationAsync(id);
                    if (notification)
                        return Ok($"Notification with ID {id} deleted successfully.");
                    else
                        return NotFound($"Notification with ID {id} not found.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error deleting notification: {ex.Message}");
                }
            }
            [Authorize(Roles = "Admin,User")]
            [HttpPut("api/Notification/Update/{id}")]
            public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    var updatedNotification = await _notificationService.UpdateNotificationAsync(id, request);
                    return Ok(updatedNotification);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error updating notification: {ex.Message}");
                }
            }
            [Authorize(Roles = "Admin,User")]
            [HttpPost("api/Notification/Create")]

            public async Task<IActionResult> CreateNotification(int userId, [FromBody] NotificationRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    var notification = await _notificationService.CreateNotificationAsync(request, userId);
                    return Ok(notification);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error creating notification: {ex.Message}");
                }
            }
    }
}


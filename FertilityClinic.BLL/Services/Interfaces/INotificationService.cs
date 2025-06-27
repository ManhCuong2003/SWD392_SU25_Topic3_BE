using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();
        Task<NotificationResponse?> GetNotificationByIdAsync(int id);
        Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request, int userId);
        Task<NotificationResponse> UpdateNotificationAsync(int id, NotificationRequest request);
        Task<bool> DeleteNotificationAsync(int id);
    }
}

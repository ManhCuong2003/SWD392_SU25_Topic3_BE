using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;




namespace FertilityClinic.BLL.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request, int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var notification = new Notification
            {
                UserId = userId,
                Title = request.Title,
                Content = request.Content,
                IsRead = request.IsRead,
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveAsync();
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                UserId = userId,
                Title = request.Title,
                Content = request.Content,
                IsRead = request.IsRead,
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notification == null)
                throw new Exception("Notification not found");
            _unitOfWork.Notifications.Remove(notification);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<NotificationResponse>> GetAllNotificationsAsync()
        {
            var notifications = await _unitOfWork.Notifications.GetAllAsync();
            if (notifications == null || !notifications.Any())
                return new List<NotificationResponse>();
            return notifications.Select(n => new NotificationResponse
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Title = n.Title,
                Content = n.Content,
                IsRead = n.IsRead,
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt =  DateTime.Now
            }).ToList();
        }

        public async Task<NotificationResponse?> GetNotificationByIdAsync(int id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notification == null)
                throw new Exception("Notification not found");

            var user = await _unitOfWork.Notifications.GetByIdAsync(notification.UserId);
            
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                UserId = notification.UserId,
                Title = notification.Title,
                Content = notification.Content,
                IsRead = notification.IsRead,
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public async Task<NotificationResponse> UpdateNotificationAsync(int id, NotificationRequest request)
        {

            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            //var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (notification == null)
                throw new Exception("Notification not found");
            notification.Title = request.Title ?? notification.Title;
            notification.Content = request.Content ?? notification.Content;
            notification.IsRead = request.IsRead; // ← chỗ này là cốt lõi
            notification.UpdatedAt = DateTime.Now;

            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveAsync();
            var updateNotification = await _unitOfWork.Notifications.GetByIdAsync(id);
            return new NotificationResponse
            {
                NotificationId = updateNotification.NotificationId,
                UserId = updateNotification.UserId,
                Title = updateNotification.Title,
                Content = updateNotification.Content,
                IsRead = notification.IsRead,
                SentAt = updateNotification.SentAt,
                CreatedAt = updateNotification.CreatedAt,
                UpdatedAt = updateNotification.UpdatedAt
            };
        }
    }
}

using Azure.Core;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);

            if (user == null)
                throw new Exception("User not found");

            // Chỉ kiểm tra email nếu email được cập nhật
            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                if (await _unitOfWork.Users.IsEmailExistsAsync(dto.Email, dto.UserId))
                    throw new Exception("Email already exists");
                user.Email = dto.Email;
            }

            // Cập nhật FullName nếu có giá trị mới
            if (!string.IsNullOrEmpty(dto.FullName))
            {
                user.FullName = dto.FullName;
            }

            // Cập nhật Password nếu có giá trị mới
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            return await _unitOfWork.Users.UpdateUserAsync(user);
        }

        public async Task<bool> SoftDeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
                return false;


            user.Email = $"deleted_{Guid.NewGuid()}@deleted.com";
            user.FullName = "Deleted User";

            user.Password = $"{Guid.NewGuid()}";

            await _unitOfWork.Users.UpdateUserAsync(user);
            return true;
        }

        public async Task<List<GetAllUsersResponse>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllActiveUsersAsync();

            return users.Select(u => new GetAllUsersResponse
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,

            }).ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<bool> HardDeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                var userId = user.UserId;



                var result = await _unitOfWork.Users.HardDeleteUserAsync(userId);
                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Failed to delete user");
                }
            }
            else
            {
                throw new Exception("User not found");
            }

        }
    }
}

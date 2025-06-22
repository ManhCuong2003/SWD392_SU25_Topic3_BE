using Azure.Core;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserRepository _userRepository;



        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
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

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.Phone = dto.PhoneNumber;

            if (!string.IsNullOrEmpty(dto.Gender))
                user.Gender = dto.Gender;

            if (!string.IsNullOrEmpty(dto.Address))
                user.Address = dto.Address;

            if (!string.IsNullOrEmpty(dto.Role))
                user.Role = dto.Role;

            if (dto.DateOfBirth != null)
                user.DateOfBirth = dto.DateOfBirth.Value;
            if (dto.IsMarried != null)
                user.IsMarried = dto.IsMarried;
            user.IsMarried = dto.IsMarried;

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
                DateDateOfBirth = u.DateOfBirth,
                Gender = u.Gender,

                DoctorName = u.Doctor?.User?.FullName ?? "Chưa chỉ định",

            }).ToList();
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            return new UserResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                Role = user.Role,
                Gender = user.Gender,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt

            };
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

        public async Task<UserResponse> GetByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);

            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found");

            var response = new UserResponse
            {
                UserId = user.UserId,
                FullName = user.FullName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.Phone ?? "",
                Role = user.Role ?? "",
                Gender = user.Gender ?? "",
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsMarried = user.IsMarried ?? false
            };

            if (user.IsMarried == true && user.Partner != null)
            {
                response.Partner = new PartnerResponse
                {
                    FullName = user.Partner.FullName ?? "",
                    DateOfBirth = user.Partner.DateOfBirth,
                    Gender = user.Partner.Gender ?? "",
                    Phone = user.Partner.Phone ?? "",
                    NationalId = user.Partner.NationalId ?? "",
                    HealthInsuranceId = user.Partner.HealthInsuranceId ?? ""
                };
            }

            return response;
        }
    }
}

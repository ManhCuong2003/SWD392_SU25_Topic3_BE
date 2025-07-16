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
            
            if (!string.IsNullOrEmpty(dto.HealthInsuranceId))
                user.HealthInsuranceId = dto.HealthInsuranceId;
            
            if (!string.IsNullOrEmpty(dto.NationalId))
                user.NationalId = dto.NationalId;
            
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
            
            //user.IsMarried = dto.IsMarried;
            var result = await _unitOfWork.Users.UpdateUserAsync(user);
            if (result)
                await _unitOfWork.SaveAsync();
            return result;

            //return await _unitOfWork.Users.UpdateUserAsync(user);

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
                DateOfBirth = user.DateOfBirth,
                HealthInsuranceId = user.HealthInsuranceId ?? "",
                NationalId = user.NationalId ?? "",
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsMarried = user.IsMarried ?? false,
                Address = user.Address ?? ""
            };

            if (user.IsMarried == true && user.Partner != null)
            {
                response.Partner = new PartnerResponse
                {
                    PartnerId = user.PartnerId,
                    FullName = user.Partner.FullName ?? "",
                    DateOfBirth = user.Partner.DateOfBirth,
                    Gender = user.Partner.Gender ?? "",
                    Phone = user.Partner.Phone ?? "",
                    
                    NationalId = user.Partner.NationalId ?? "",
                    HealthInsuranceId = user.Partner.HealthInsuranceId ?? "",

                };
            }

            return response;
        }
        public async Task<List<UserResponse>> GetUsersByCurrentDoctorAsync(int userId)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByUserIdAsync(userId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            var appointments = await _unitOfWork.Appointments.GetAllAppointmentsAsync();

            var result = appointments
                .Where(a => a.DoctorId == doctor.DoctorId && a.User != null)
                .Select(a => new UserResponse
                {
                    UserId = a.User.UserId,
                    FullName = a.User.FullName ?? "",
                    Email = a.User.Email ?? "",
                    PhoneNumber = a.User.Phone ?? "",
                    DateOfBirth = a.User.DateOfBirth,
                    HealthInsuranceId = a.User.HealthInsuranceId ?? "",
                    NationalId = a.User.NationalId ?? "",
                    Address = a.User.Address ?? "",
                    Gender = a.User.Gender ?? "",
                    IsMarried = a.User.IsMarried ?? false,
                    Partner = a.Partner != null ? new PartnerResponse
                    {
                        PartnerId = a.Partner.PartnerId,
                        FullName = a.Partner.FullName ?? "",
                        DateOfBirth = a.Partner.DateOfBirth,
                        Gender = a.Partner.Gender ?? "",
                        Phone = a.Partner.Phone ?? "",
                        NationalId = a.Partner.NationalId ?? "",
                        HealthInsuranceId = a.Partner.HealthInsuranceId ?? ""
                    } : null,
                    // Thêm thông tin cuộc hẹn vào response
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime
                })
                .ToList();

            if (result.Count == 0)
                throw new Exception("Bác sĩ hiện chưa có cuộc hẹn nào");

            return result;
        }
    }
}

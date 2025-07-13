using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PartnerService : IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PartnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PartnerResponse> CreatePartnerAsync(PartnerRequest request, int userId)
        {
            // Kiểm tra user tồn tại
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            // Kiểm tra xem user đã có partner chưa
            var existingPartner = await _unitOfWork.Partners.GetPartnerByUserIdAsync(userId);
            if (existingPartner != null)
            {
                throw new Exception("User already has a partner");
            }
            // Kiểm tra role của user
            if (user.Role != "Admin" && user.Role != "User")
            {
                throw new Exception("Only Admin or User can create partner");
            }
            var partner = new Partner
            {
                UserId = user.UserId,
                
                FullName = request.FullName,
                
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth ?? null,
                Gender = request.Gender,
                NationalId = request.NationalId,
                HealthInsuranceId = request.HealthInsuranceId,


            };

            await _unitOfWork.Partners.AddAsync(partner);
            await _unitOfWork.SaveAsync();

            //user.PartnerId = partner.PartnerId; // Cập nhật PartnerId cho User
            //await _unitOfWork.Users.UpdateUserAsync(user);
            //await _unitOfWork.Partners.AddAsync(partner);
            //await _unitOfWork.SaveAsync();
            // 2. Cập nhật PartnerId trong User
            try
            {
                user.PartnerId = partner.PartnerId;

                var updateResult = await _unitOfWork.Users.UpdateUserAsync(user);
                if (!updateResult)
                {
                    throw new Exception("Failed to update user's partner information");
                }
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                throw new Exception($"Error updating user's partner information: {ex.Message}");
            }
            return new PartnerResponse
            {
                PartnerId = partner.PartnerId,
                FullName = partner.FullName,
                DateOfBirth = partner.DateOfBirth,
                Gender = partner.Gender,
                //Email = partner.Email,
                Phone = partner.Phone,
                NationalId = partner.NationalId,
                HealthInsuranceId = partner.HealthInsuranceId
            };
        }

        public async Task<bool> DeletePartnerAsync(int id)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(id);
            if (partner == null)
            {
                throw new Exception($"Partner with ID {id} not found");

            }
            _unitOfWork.Partners.Remove(partner);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<PartnerResponse>> GetAllPartnersAsync()
        {
            var partners = await _unitOfWork.Partners.GetAllPartnersAsync();
            if (partners == null || !partners.Any())
                throw new Exception("No partners found");

            return partners.Select(p => new PartnerResponse
            {
                PartnerId = p.PartnerId,
                FullName = p.FullName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Phone = p.Phone,
                NationalId = p.NationalId,
                HealthInsuranceId = p.HealthInsuranceId
            }).ToList();
        }
        public async Task<PartnerResponse?> GetPartnerByIdAsync(int id)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(id);
            if (partner == null)
            {
                throw new Exception($"Partner with ID {id} not found");
            }
            return new PartnerResponse
            {
                PartnerId = partner.PartnerId,
                FullName = partner.FullName,
                DateOfBirth = partner.DateOfBirth,
                Gender = partner.Gender,
                Phone = partner.Phone,
                NationalId = partner.NationalId,
                HealthInsuranceId = partner.HealthInsuranceId
            };
        }

        public async Task<PartnerResponse> UpdatePartnerAsync(int id, UpdatePartnerRequest request)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(id);
            if (partner == null)
                throw new Exception($"Partner with ID {id} not found");
            
            if(!string.IsNullOrEmpty(request.FullName))
                partner.FullName = request.FullName;
            
            if (!string.IsNullOrEmpty(request.Phone))
                partner.Phone = request.Phone;
            
            if (!string.IsNullOrEmpty(request.NationalId))
                partner.NationalId = request.NationalId;
            
            if (!string.IsNullOrEmpty(request.HealthInsuranceId))
                partner.HealthInsuranceId = request.HealthInsuranceId;
            
            if(request.DateOfBirth.HasValue)
                partner.DateOfBirth = request.DateOfBirth.Value;
            
            if (!string.IsNullOrEmpty(request.Gender))
                partner.Gender = request.Gender;
            
            
            _unitOfWork.Partners.Update(partner);
            await _unitOfWork.SaveAsync();
            
            var updatedPartner = await _unitOfWork.Partners.GetByIdAsync(id);

            return new PartnerResponse
            {
                PartnerId = updatedPartner.PartnerId,
                FullName = updatedPartner.FullName,
                DateOfBirth = updatedPartner.DateOfBirth,
                Gender = updatedPartner.Gender,
                Phone = updatedPartner.Phone,
                NationalId = updatedPartner.NationalId,
                HealthInsuranceId = updatedPartner.HealthInsuranceId
            };
        }
    }
}
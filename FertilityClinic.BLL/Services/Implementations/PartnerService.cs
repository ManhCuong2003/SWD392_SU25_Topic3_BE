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

            var partner = new Partner
            {
                UserId = userId,
                FullName = "Chưa cập nhập",
                Email = "Chưa cập nhật",
                Phone = "Chưa cập nhật",
                DateOfBirth = request.DateOfBirth ?? null,
                Gender = "Chưa cập nhập",
                NationalId = "Chưa cập nhập",
                HealthInsuranceId = "Chưa cập nhập",


            };

            user.Role = "Partner";
            await _unitOfWork.Users.UpdateUserAsync(user);
            await _unitOfWork.Partners.AddAsync(partner);
            await _unitOfWork.SaveAsync();
            return new PartnerResponse
            {

                FullName = partner.FullName,
                DateOfBirth = partner.DateOfBirth,
                Gender = partner.Gender,
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
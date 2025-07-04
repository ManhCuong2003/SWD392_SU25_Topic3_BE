﻿using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class TreatmentMethodServicecs : ITreatmentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TreatmentMethodServicecs(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TreatmentMethodResponse> CreateTreatmentMethodAsync(TreatmentMethodRequest request)
        {
            var treatmentMethod = new TreatmentMethod
            {
                MethodName = request.MethodName,
                MethodCode = request.MethodCode,
                Description = request.Description,
                IsActive = true,
                TechnicalRequirements = request.TechnicalRequirements,
                AverageDuration = 0,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.TreatmentMethods.AddAsync(treatmentMethod);
            await _unitOfWork.SaveAsync();
            return new TreatmentMethodResponse
            {
                TreatmentMethodId = treatmentMethod.TreatmentMethodId,
                MethodName = treatmentMethod.MethodName,
                MethodCode = treatmentMethod.MethodCode,
                Description = treatmentMethod.Description,
                IsActive = treatmentMethod.IsActive ?? false, // hoặc true nếu phù hợp
                Price = treatmentMethod.Price,
                TechnicalRequirements = treatmentMethod.TechnicalRequirements,
                AverageDuration = treatmentMethod.AverageDuration
            };
        }

        public async Task<bool> DeleteTreatmentMethodAsync(int id)
        {
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(id);
            if (treatmentMethod == null)
            {
                throw new Exception("Treatment method not found");
            }
            _unitOfWork.TreatmentMethods.Remove(treatmentMethod);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<TreatmentMethodResponse>> GetAllTreatmentMethodsAsync()
        {
            var treatmentMethods = await _unitOfWork.TreatmentMethods.GetAllAsync();
            {
                if (treatmentMethods == null || !treatmentMethods.Any())
                {
                    throw new Exception("No treatment methods found");
                }
                return treatmentMethods.Select(tm => new TreatmentMethodResponse
                {
                    TreatmentMethodId = tm.TreatmentMethodId,
                    MethodName = tm.MethodName,
                    MethodCode = tm.MethodCode,
                    Description = tm.Description,
                    IsActive = tm.IsActive ?? false,
                    TechnicalRequirements = tm.TechnicalRequirements,
                    Price = tm.Price,
                    AverageDuration = tm.AverageDuration
                }).ToList();
            }
            ;
        }

        public async Task<TreatmentMethodResponse?> GetTreatmentMethodByIdAsync(int id)
        {
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(id);
            if(treatmentMethod == null)
            {
                throw new Exception("Treatment method not found");
            }
            return new TreatmentMethodResponse
            {
                TreatmentMethodId = treatmentMethod.TreatmentMethodId,
                MethodName = treatmentMethod.MethodName,
                MethodCode = treatmentMethod.MethodCode,
                Description = treatmentMethod.Description,
                IsActive = treatmentMethod.IsActive ?? false, // hoặc true nếu phù hợp
                Price = treatmentMethod.Price,
                TechnicalRequirements = treatmentMethod.TechnicalRequirements,
                AverageDuration = treatmentMethod.AverageDuration
            };
        }

        public async Task<TreatmentMethodResponse> UpdateTreatmentMethodAsync(int id, TreatmentMethodRequest request)
        {
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(id);
            if (treatmentMethod == null)
                throw new Exception("Treatment method not found");
            
            if(!string.IsNullOrEmpty(request.MethodName))
                treatmentMethod.MethodName = request.MethodName;
            
            if(!string.IsNullOrEmpty(request.MethodCode))
                treatmentMethod.MethodCode = request.MethodCode;
            
            if(!string.IsNullOrEmpty(request.Description))
                treatmentMethod.Description = request.Description;
        
            if(request.IsActive.HasValue)
                treatmentMethod.IsActive = request.IsActive.Value;
            
            if(!string.IsNullOrEmpty(request.TechnicalRequirements))
                treatmentMethod.TechnicalRequirements = request.TechnicalRequirements;
            
            if(request.AverageDuration.HasValue)
                treatmentMethod.AverageDuration = request.AverageDuration.Value;

            if(request.Price.HasValue)
                treatmentMethod.Price = request.Price.Value;

            _unitOfWork.TreatmentMethods.Update(treatmentMethod);
            await _unitOfWork.SaveAsync();
            var updatedTreatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(id);
            return new TreatmentMethodResponse
            {
                TreatmentMethodId = treatmentMethod.TreatmentMethodId,
                MethodName = treatmentMethod.MethodName,
                MethodCode = treatmentMethod.MethodCode,
                Description = treatmentMethod.Description,
                IsActive = treatmentMethod.IsActive ?? false,
                Price = treatmentMethod.Price,
                TechnicalRequirements = treatmentMethod.TechnicalRequirements,
                AverageDuration = treatmentMethod.AverageDuration
            };
        }
    }
}

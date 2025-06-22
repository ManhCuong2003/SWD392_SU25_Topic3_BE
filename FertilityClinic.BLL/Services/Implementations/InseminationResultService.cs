using FertilityClinic.BLL.Services.Interfaces;
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
    public class InseminationResultService : IInseminationResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InseminationResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InseminationResultResponse> CreateInseminationResultAsync(InseminationResultRequest request, int inseminationScheduleId, int doctorId)
        {
            var newInseminationResult = new InseminationResult
            {
                InseminationScheduleId = inseminationScheduleId,
                DoctorId = doctorId,
                ResultDate = request.ResultDate,
                ResultDetails = request.ResultDetails,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InseminationResults.AddAsync(newInseminationResult);
            await _unitOfWork.SaveAsync();
            return new InseminationResultResponse
            {
                InseminationResultId = newInseminationResult.InseminationResultId,
                InseminationScheduleId = newInseminationResult.InseminationScheduleId,
                DoctorId = newInseminationResult.DoctorId,
                ResultDate = newInseminationResult.ResultDate,
                ResultDetails = newInseminationResult.ResultDetails,
                CreatedAt = newInseminationResult.CreatedAt,
                UpdatedAt = newInseminationResult.UpdatedAt
            };
        }

        public async Task<bool> DeleteInseminationResultAsync(int id)
        {
            var inseminationResult = await _unitOfWork.InseminationResults.GetByIdAsync(id);
            if (inseminationResult == null)
            {
                throw new Exception("Insemination Result not found");
            }
            _unitOfWork.InseminationResults.Remove(inseminationResult);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<InseminationResultResponse>> GetAllInseminationResultsAsync()
        {
            var inseminationResults = await _unitOfWork.InseminationResults.GetAllAsync();
            {
                if (inseminationResults == null || !inseminationResults.Any())
                {
                    return new List<InseminationResultResponse>();
                }
                return inseminationResults.Select(ir => new InseminationResultResponse
                {
                    InseminationResultId = ir.InseminationResultId,
                    InseminationScheduleId = ir.InseminationScheduleId,
                    DoctorId = ir.DoctorId,
                    ResultDate = ir.ResultDate,
                    ResultDetails = ir.ResultDetails,
                    CreatedAt = ir.CreatedAt,
                    UpdatedAt = ir.UpdatedAt
                }).ToList();
            }
        }
        public async Task<InseminationResultResponse?> GetInseminationResultByIdAsync(int id)
        {
            var inseminationResult = await _unitOfWork.InseminationResults.GetByIdAsync(id);
            if (inseminationResult == null)
            {
                throw new Exception("Insemination Result not found");
            }
            var inseminationSchedule = await _unitOfWork.InseminationSchedules.GetByIdAsync(inseminationResult.InseminationScheduleId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(inseminationResult.DoctorId);
            return new InseminationResultResponse
            {
                InseminationResultId = inseminationResult.InseminationResultId,
                InseminationScheduleId = inseminationResult.InseminationScheduleId,
                DoctorId = inseminationResult.DoctorId,
                ResultDate = inseminationResult.ResultDate,
                ResultDetails = inseminationResult.ResultDetails,
                CreatedAt = inseminationResult.CreatedAt,
                UpdatedAt = inseminationResult.UpdatedAt
            };
        }
    }
}
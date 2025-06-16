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
    public class LabTestResultService : ILabTestResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LabTestResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LabTestResultResponse> CreateLabTestResultAsync(LabTestResultRequest labTestResult, int labTestScheduleId, int docterId)
        {
            var newLabTestResult = new LabTestResult
            {
                LabTestScheduleId = labTestScheduleId,
                DoctorId = docterId,
                ResultDetails = labTestResult.ResultDetails,
                Notes = labTestResult.Notes,
                ResultDate = labTestResult.ResultDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.LabTestResults.AddAsync(newLabTestResult);
            await _unitOfWork.SaveAsync();
            return new LabTestResultResponse
            {
                LabTestResultId = newLabTestResult.LabTestResultId,
                LabTestScheduleId = newLabTestResult.LabTestScheduleId,
                DoctorId = newLabTestResult.DoctorId,
                ResultDetails = newLabTestResult.ResultDetails,
                Notes = newLabTestResult.Notes,
                ResultDate = newLabTestResult.ResultDate,
                CreatedAt = newLabTestResult.CreatedAt,
                UpdatedAt = newLabTestResult.UpdatedAt
            };
        }

        public Task<bool> DeleteLabTestResultAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LabTestResultResponse>> GetAllLabTestResultsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LabTestResultResponse> GetLabTestResultByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

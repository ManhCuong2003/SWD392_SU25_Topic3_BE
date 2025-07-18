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

        public async Task<LabTestResultResponse> CreateLabTestResultAsync(LabTestResultRequest labTestResult, int userId)
        {
            

            var newLabTestResult = new LabTestResult
            {
                //LabTestScheduleId = labTestScheduleId,
                UserId = userId,
                Name = labTestResult.Name,
                Result = labTestResult.Result,
                Normal = labTestResult.Normal,
                Unit = labTestResult.Unit,
                Bold = labTestResult.Bold,
                Date = labTestResult.Date,

            };
            await _unitOfWork.LabTestResults.AddAsync(newLabTestResult);
            await _unitOfWork.SaveAsync();
            return new LabTestResultResponse
            {
                LabTestResultId = newLabTestResult.LabTestResultId,
                //LabTestScheduleId = newLabTestResult.LabTestScheduleId,
                UserId = newLabTestResult.UserId,
                Name = newLabTestResult.Name,
                Result = newLabTestResult.Result,
                Normal = newLabTestResult.Normal,
                Unit = newLabTestResult.Unit,
                Bold = newLabTestResult.Bold,
                Date = newLabTestResult.Date
            };
        }

        public async Task<bool> DeleteLabTestResultAsync(int id)
        {
            var labTestResult = await _unitOfWork.LabTestResults.GetByIdAsync(id);
            if (labTestResult == null)
            {
                throw new KeyNotFoundException($"Lab test result with ID {id} not found.");
            }

            _unitOfWork.LabTestResults.Remove(labTestResult);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }




        public async Task<List<LabTestResultResponse>> GetAllLabTestResultsAsync()
        {
            var labTestResults = await _unitOfWork.LabTestResults.GetAllAsync();
            if (labTestResults == null || !labTestResults.Any())
            {
                return new List<LabTestResultResponse>();
            }
            return labTestResults.Select(result => new LabTestResultResponse
            {
                LabTestResultId = result.LabTestResultId,
                //LabTestScheduleId = result.LabTestScheduleId,
                UserId = result.UserId,
                Name = result.Name,
                Result = result.Result,
                Normal = result.Normal,
                Unit = result.Unit,
                Bold = result.Bold,
                Date = result.Date

            }).ToList();
        }

        public async Task<LabTestResultResponse> GetLabTestResultByIdAsync(int id)
        {
            var labTestResult = await _unitOfWork.LabTestResults.GetByIdAsync(id);
            if (labTestResult == null)
            {
                throw new Exception("Lab Test Result not found");
            }
            //var labTestSchedule = await _unitOfWork.LabTestSchedules.GetByIdAsync(labTestResult.LabTestScheduleId);
            var doctor = await _unitOfWork.Users.GetByIdAsync(labTestResult.UserId);
            return new LabTestResultResponse
            {
                LabTestResultId = labTestResult.LabTestResultId,
                //LabTestScheduleId = labTestResult.LabTestScheduleId,
                UserId = labTestResult.UserId,
                Name = labTestResult.Name,
                Result = labTestResult.Result,
                Normal = labTestResult.Normal,
                Unit = labTestResult.Unit,
                Bold = labTestResult.Bold,
                Date = labTestResult.Date
            };
        }

        public async Task<List<LabTestResultResponse>> GetLabTestResultsByUserIdAsync(int userId)
        {
            var results = await _unitOfWork.LabTestResults.GetLabTestResultsByUserIdAsync(userId);

            return results.Select(r => new LabTestResultResponse
            {
                LabTestResultId = r.LabTestResultId,
                UserId = r.UserId,
                Name = r.Name,
                Result = r.Result,
                Normal = r.Normal,
                Unit = r.Unit,
                Bold = r.Bold,
                Date = r.Date
            }).ToList();
        }
        public async Task<LabTestResultResponse> UpdateLabTestResultAsync(int id, LabTestResultUpdateRequest request)
        {
            var labTestResult = await _unitOfWork.LabTestResults.GetByIdAsync(id);
            if (labTestResult == null)
                throw new KeyNotFoundException("Lab Test Result not found");

            if (!string.IsNullOrWhiteSpace(request.Name))
                labTestResult.Name = request.Name.Trim();

            if (!string.IsNullOrWhiteSpace(request.Result))
                labTestResult.Result = request.Result.Trim();

            if (!string.IsNullOrWhiteSpace(request.Normal))
                labTestResult.Normal = request.Normal.Trim();

            if (!string.IsNullOrWhiteSpace(request.Unit))
                labTestResult.Unit = request.Unit.Trim();

            if (request.Bold.HasValue)
                labTestResult.Bold = request.Bold.Value;

            if (request.Date.HasValue && request.Date.Value != default)
                labTestResult.Date = request.Date.Value;

            _unitOfWork.LabTestResults.Update(labTestResult);
            await _unitOfWork.SaveAsync();

            return new LabTestResultResponse
            {
                LabTestResultId = labTestResult.LabTestResultId,
                UserId = labTestResult.UserId,
                Name = labTestResult.Name,
                Result = labTestResult.Result,
                Normal = labTestResult.Normal,
                Unit = labTestResult.Unit,
                Bold = labTestResult.Bold,
                Date = labTestResult.Date
            };
        }




    }
}

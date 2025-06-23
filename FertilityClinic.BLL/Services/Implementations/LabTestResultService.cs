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

        public async Task<LabTestResultResponse> CreateLabTestResultAsync(LabTestResultRequest labTestResult, int docterId)
        {
            var newLabTestResult = new LabTestResult
            {
                //LabTestScheduleId = labTestScheduleId,
                DoctorId = docterId,
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
                LabTestScheduleId = newLabTestResult.LabTestScheduleId,
                DoctorId = newLabTestResult.DoctorId,
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
                throw new Exception("Lab Test Result not found");
            }
            _unitOfWork.LabTestResults.Remove(labTestResult);
            await _unitOfWork.SaveAsync();
            return true;
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
                LabTestScheduleId = result.LabTestScheduleId,
                DoctorId = result.DoctorId,
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
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(labTestResult.DoctorId);
            return new LabTestResultResponse
            {
                LabTestResultId = labTestResult.LabTestResultId,
                LabTestScheduleId = labTestResult.LabTestScheduleId,
                DoctorId = labTestResult.DoctorId,
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

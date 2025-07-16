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
    public class TreatmentProcessService : ITreatmentProcessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentProcessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TreatmentProcessResponse> CreateTreatmentProcessAsync(TreatmentProcessRequest request, int userId, int treatmentMethodId, int doctorId)
        {
            //1. Validate input parameters

            // Trong CreateTreatmentProcessAsync
            var appointments = await _unitOfWork.Appointments.GetAllAppointmentsAsync();
            var hasAppointments = appointments.Any(a => a.DoctorId == doctorId);
            if (hasAppointments)
            {
                throw new Exception($"Bác sĩ có ID {doctorId} đã có lịch hẹn, không thể tạo quy trình điều trị.");
            }
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(treatmentMethodId);
            var treatmentProcess = new TreatmentProcess
            {
                UserId = userId,
                TreatmentMethodId = treatmentMethodId,
                DoctorId = doctorId,
                ProcessName = request.ProcessName ?? "Chưa cập nhập",
                Notes = request.Notes ?? "Chưa cập nhập",
                TrackingMode = request.TrackingMode ?? "Chưa cập nhập",
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.TreatmentProcesses.AddAsync(treatmentProcess);
            await _unitOfWork.SaveAsync();
            return new TreatmentProcessResponse
            {
                TreatmentProcessId = treatmentProcess.TreatmentProcessId,
                UserId = userId,
                DoctorId = doctorId,
                UserName = user.FullName,
                TreatmentMethodId = treatmentMethodId,
                TreatmentMethodName = treatmentMethod.MethodName,
                ProcessName = request.ProcessName,
                Notes = request.Notes,
                TrackingMode = request.TrackingMode,
                CreatedAt = treatmentProcess.CreatedAt
            };
            
        }

        public async Task<bool> DeleteTreatmentProcessAsync(int id)
        {
            var treatmentProcess = await _unitOfWork.TreatmentProcesses.GetTreatmentProcessByIdAsync(id);
            if (treatmentProcess == null)
            {
                throw new Exception($"Treatment process with ID {id} not found.");
                return false;
            }
            _unitOfWork.TreatmentProcesses.Remove(treatmentProcess);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<TreatmentProcessResponse>> GetAllTreatmentProcessesAsync()
        {
            var treatmentProcesses = await _unitOfWork.TreatmentProcesses.GetAllTreatmentProcessesAsync();
            if(treatmentProcesses == null || !treatmentProcesses.Any())
                throw new Exception("No treatment processes found.");
            return treatmentProcesses.Select(tp => new TreatmentProcessResponse
            {
                TreatmentMethodId = tp.TreatmentMethodId,
                UserId = tp.UserId,
                DoctorId = tp.DoctorId,
                UserName = tp.User?.FullName,
                TreatmentMethodName = tp.TreatmentMethod?.MethodName,
                TreatmentProcessId = tp.TreatmentProcessId,
                ProcessName = tp.ProcessName,
                Notes = tp.Notes,
                TrackingMode = tp.TrackingMode,
                CreatedAt = tp.CreatedAt
            }).ToList();
        }

        public async Task<TreatmentProcessResponse?> GetTreatmentProcessByIdAsync(int id)
        {
            var treatmentProcess = await _unitOfWork.TreatmentProcesses.GetTreatmentProcessByIdAsync(id);
            if (treatmentProcess == null)
                throw new Exception($"Treatment process with ID {id} not found.");
            return new TreatmentProcessResponse
            {
                TreatmentMethodId = treatmentProcess.TreatmentMethodId,
                UserId = treatmentProcess.UserId,
                DoctorId = treatmentProcess.DoctorId,
                UserName = treatmentProcess.User?.FullName ?? "Unknown",
                TreatmentMethodName = treatmentProcess.TreatmentMethod?.MethodName ?? "Unknown",
                TreatmentProcessId = treatmentProcess.TreatmentProcessId,
                ProcessName = treatmentProcess.ProcessName,
                Notes = treatmentProcess.Notes,
                TrackingMode = treatmentProcess.TrackingMode ?? "Unknown",
                CreatedAt = treatmentProcess.CreatedAt,
            };
        }

        public async Task<TreatmentProcessResponse> UpdateTreatmentProcessAsync(int id, UpdateTreatmentProcessRequest request)
        {
            var treatmentProcess = await _unitOfWork.TreatmentProcesses.GetTreatmentProcessByIdAsync(id);
            if (treatmentProcess == null)
                throw new Exception($"Treatment process with ID {id} not found.");
            
            if(!string.IsNullOrEmpty(request.ProcessName))
                treatmentProcess.ProcessName = request.ProcessName;
            if(!string.IsNullOrEmpty(request.Notes))
                treatmentProcess.Notes = request.Notes;
            if(!string.IsNullOrEmpty(request.TrackingMode))
                treatmentProcess.TrackingMode = request.TrackingMode;
            _unitOfWork.TreatmentProcesses.Update(treatmentProcess);
            await _unitOfWork.SaveAsync();

            var updated = await _unitOfWork.TreatmentProcesses.GetTreatmentProcessByIdAsync(id);
            return new TreatmentProcessResponse
            {
                TreatmentProcessId = treatmentProcess.TreatmentProcessId,
                UserId = treatmentProcess.UserId,
                DoctorId = treatmentProcess.DoctorId,
                UserName = updated.User?.FullName ?? "Unknown",
                TreatmentMethodName = updated.TreatmentMethod?.MethodName ?? "Unknown",
                TreatmentMethodId = treatmentProcess.TreatmentMethodId,
                ProcessName = request.ProcessName ?? treatmentProcess.ProcessName,
                Notes = request.Notes ?? treatmentProcess.Notes,
                TrackingMode = request.TrackingMode ?? treatmentProcess.TrackingMode,
                CreatedAt = treatmentProcess.CreatedAt
            };
        }
    }
}

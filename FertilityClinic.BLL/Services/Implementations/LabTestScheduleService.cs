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
    public class LabTestScheduleService : ILabTestScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LabTestScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LabTestScheduleResponse> CreateLabTestScheduleAsync(LabTestScheduleRequest labTestSchedule, int doctorId, int treatmentProcessId)
        {
            var newLabTestSchedule = new LabTestSchedule
            {
                TreatmentProcessId = treatmentProcessId,
                DoctorId = doctorId,
                TestDate = labTestSchedule.TestDate,
                TestType = labTestSchedule.TestType,
                Status = labTestSchedule.Status,
                Notes = labTestSchedule.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.LabTestSchedules.AddAsync(newLabTestSchedule);
            await _unitOfWork.SaveAsync();
            return new LabTestScheduleResponse
            {
                LabTestScheduleId = newLabTestSchedule.LabTestScheduleId,
                TreatmentProcessId = newLabTestSchedule.TreatmentProcessId,
                DoctorId = newLabTestSchedule.DoctorId,
                TestDate = newLabTestSchedule.TestDate,
                TestType = newLabTestSchedule.TestType,
                Status = newLabTestSchedule.Status,
                Notes = newLabTestSchedule.Notes,
                CreatedAt = newLabTestSchedule.CreatedAt,
                UpdatedAt = newLabTestSchedule.UpdatedAt
            };
        }

        public async Task<bool> DeleteLabTestScheduleAsync(int id)
        {
            var labTestSchedule = await _unitOfWork.LabTestSchedules.GetByIdAsync(id);
            if (labTestSchedule == null)
            {
                throw new Exception("Lab Test Schedule not found");
            }
            _unitOfWork.LabTestSchedules.Remove(labTestSchedule);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<LabTestScheduleResponse>> GetAllLabTestSchedulesAsync()
        {
            var labTestSchedules = await _unitOfWork.LabTestSchedules.GetAllAsync();
            if( labTestSchedules == null || !labTestSchedules.Any())
            {
                return new List<LabTestScheduleResponse>();
            }
            return labTestSchedules.Select(l => new LabTestScheduleResponse
            {
                LabTestScheduleId = l.LabTestScheduleId,
                TreatmentProcessId = l.TreatmentProcessId,
                DoctorId = l.DoctorId,
                TestDate = l.TestDate,
                TestType = l.TestType,
                Status = l.Status,
                Notes = l.Notes,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList();
        }

        public async Task<LabTestScheduleResponse> GetLabTestScheduleByIdAsync(int labTestScheduleId)
        {
            var labTestSchedule = await _unitOfWork.LabTestSchedules.GetByIdAsync(labTestScheduleId);
            if (labTestSchedule == null)
            {
                throw new Exception("Lab Test Schedule not found");
            }
            var treatmentProcess = await _unitOfWork.TreatmentProcesses.GetByIdAsync(labTestSchedule.TreatmentProcessId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(labTestSchedule.DoctorId);
            
            return new LabTestScheduleResponse
            {
                LabTestScheduleId = labTestSchedule.LabTestScheduleId,
                TreatmentProcessId = treatmentProcess.TreatmentProcessId,
                DoctorId = doctor.DoctorId,
                TestDate = labTestSchedule.TestDate,
                TestType = labTestSchedule.TestType,
                Status = labTestSchedule.Status,
                Notes = labTestSchedule.Notes,
                CreatedAt = labTestSchedule.CreatedAt,
                UpdatedAt = labTestSchedule.UpdatedAt,
                
            };
        }

        public async Task<LabTestScheduleResponse> UpdateLabTestScheduleAsync(int id, UpdateLabTestScheduleRequest labTestSchedule)
        {
            var labTestScheduleToUpdate = await _unitOfWork.LabTestSchedules.GetByIdAsync(id);
            if (labTestScheduleToUpdate == null)
            {
                throw new Exception("Lab Test Schedule not found");
            }
            if(labTestSchedule.TestDate.HasValue)
            {
                labTestScheduleToUpdate.TestDate = labTestSchedule.TestDate.Value;
            }
            if (!string.IsNullOrEmpty(labTestSchedule.TestType))
            {
                labTestScheduleToUpdate.TestType = labTestSchedule.TestType;
            }
            if (!string.IsNullOrEmpty(labTestSchedule.Status))
            {
                labTestScheduleToUpdate.Status = labTestSchedule.Status;
            }
            if (!string.IsNullOrEmpty(labTestSchedule.Notes))
            {
                labTestScheduleToUpdate.Notes = labTestSchedule.Notes;
            }
            _unitOfWork.LabTestSchedules.Update(labTestScheduleToUpdate);
            await _unitOfWork.SaveAsync();
            var UpdatedLabTestSchedule = await _unitOfWork.LabTestSchedules.GetByIdAsync(id);
            return new LabTestScheduleResponse
            {
                LabTestScheduleId = UpdatedLabTestSchedule.LabTestScheduleId,
                TreatmentProcessId = UpdatedLabTestSchedule.TreatmentProcessId,
                DoctorId = UpdatedLabTestSchedule.DoctorId,
                TestDate = UpdatedLabTestSchedule.TestDate,
                TestType = UpdatedLabTestSchedule.TestType,
                Status = UpdatedLabTestSchedule.Status,
                Notes = UpdatedLabTestSchedule.Notes,
                CreatedAt = UpdatedLabTestSchedule.CreatedAt,
                UpdatedAt = UpdatedLabTestSchedule.UpdatedAt
            };
        }
    }
}

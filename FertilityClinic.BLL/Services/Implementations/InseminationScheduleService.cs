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
    public class InseminationScheduleService : IInseminationScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InseminationScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InseminationScheduleResponse> CreateInseminationScheduleAsync(InseminationScheduleRequest request, int doctorId, int treatmentProcessId)
        {
            
            var newSchedule = new InseminationSchedule
            {
                // Assuming the request contains properties like Date, Time, etc.
                TreatmentProcessId = treatmentProcessId,
                DoctorId = doctorId,
                InseminationDate = request.InseminationDate ?? DateTime.Now,
                Method = request.Method,
                Status = request.Status,
                Notes = request.Notes,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _unitOfWork.InseminationSchedules.AddAsync(newSchedule);
            await _unitOfWork.SaveAsync();
            return new InseminationScheduleResponse
            {
                TreatmentProcessId = newSchedule.TreatmentProcessId,
                DoctorId = doctorId,
                InseminationDate = newSchedule.InseminationDate,
                Method = newSchedule.Method,
                Status = newSchedule.Status,
                Notes = newSchedule.Notes,
                CreatedAt = newSchedule.CreatedAt,
                UpdatedAt = newSchedule.UpdatedAt
            };


        }
        
        public async Task<bool> DeleteInseminationScheduleAsync(int id)
        {
            var schedule = await _unitOfWork.InseminationSchedules.GetByIdAsync(id);
            if (schedule == null)
            {
                throw new ArgumentException("Insemination schedule not found.");
            }
            _unitOfWork.InseminationSchedules.Remove(schedule);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<InseminationScheduleResponse>> GetAllInseminationSchedulesAsync()
        {
            var inseminationShedules = await _unitOfWork.InseminationSchedules.GetAllAsync();
            if (inseminationShedules == null || !inseminationShedules.Any())
            {
                throw new Exception("No insemination schedules found.");
            }
            return inseminationShedules.Select(schedule => new InseminationScheduleResponse
            {
                InseminationScheduleId = schedule.InseminationScheduleId,
                TreatmentProcessId = schedule.TreatmentProcessId,
                DoctorId = schedule.DoctorId,
                InseminationDate = schedule.InseminationDate,
                Method = schedule.Method,
                Status = schedule.Status,
                Notes = schedule.Notes,
                CreatedAt = schedule.CreatedAt,
                UpdatedAt = schedule.UpdatedAt
            }).ToList();
        }

        public async Task<InseminationScheduleResponse> GetInseminationScheduleByIdAsync(int inseminationScheduleId)
        {
            var schedule = await _unitOfWork.InseminationSchedules.GetByIdAsync(inseminationScheduleId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(schedule.DoctorId);
            if (schedule == null || doctor == null)
            {
                throw new Exception("Insemination schedule or doctor not found.");
            }
            return new InseminationScheduleResponse
            {
                InseminationScheduleId = schedule.InseminationScheduleId,
                TreatmentProcessId = schedule.TreatmentProcessId,
                DoctorId = schedule.DoctorId,
                InseminationDate = schedule.InseminationDate,
                Method = schedule.Method,
                Status = schedule.Status,
                Notes = schedule.Notes,
                CreatedAt = schedule.CreatedAt,
                UpdatedAt = schedule.UpdatedAt
            };
        }

        public Task<InseminationScheduleResponse> UpdateInseminationScheduleAsync(int id, InseminationScheduleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

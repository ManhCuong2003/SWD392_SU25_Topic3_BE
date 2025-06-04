using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class AppoimentHistoryService : IAppoimentHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppoimentHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AppointmentHistoryResponse> CreateAppointmentAsync(AppointmentHistoryRequest appointment)
        {
            var newAppointment = new AppointmentHistory
            {
                PatientName = appointment.PatientName,
                PatientDOB = appointment.PatientDOB,
                PhoneNumber = appointment.PhoneNumber,
                MethodName = appointment.MethodName,
                PartnerName = appointment.PartnerName,
                PartnerDOB = appointment.PartnerDOB,
                DoctorName = appointment.DoctorName,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = appointment.Status,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.AppointmentHistories.AddAsync(newAppointment);
            await _unitOfWork.SaveAsync();
            return new AppointmentHistoryResponse
            {
                PatientName = newAppointment.PatientName,
                PatientDOB = newAppointment.PatientDOB,
                PhoneNumber = newAppointment.PhoneNumber,
                MethodName = newAppointment.MethodName,
                PartnerName = newAppointment.PartnerName,
                PartnerDOB = newAppointment.PartnerDOB,
                DoctorName = newAppointment.DoctorName,
                AppointmentDate = newAppointment.AppointmentDate,
                AppointmentTime = newAppointment.AppointmentTime,
                Status = newAppointment.Status,
                CreatedAt = newAppointment.CreatedAt
            };
        }

        public async Task<List<AppointmentHistoryResponse>> GetAllAppointmentHistoriesAsync()
        {
            var appointmentHistories = await _unitOfWork.AppointmentHistories.GetAllAppointmentHistoriesAsync();
            if(appointmentHistories == null || !appointmentHistories.Any())
            {
                return new List<AppointmentHistoryResponse>();
            }
            return appointmentHistories.Select(ah => new AppointmentHistoryResponse
            {
                PatientName = ah.PatientName,
                PatientDOB = ah.PatientDOB,
                PhoneNumber = ah.PhoneNumber,
                MethodName = ah.MethodName,
                PartnerName = ah.PartnerName,
                PartnerDOB = ah.PartnerDOB,
                DoctorName = ah.DoctorName,
                AppointmentDate = ah.AppointmentDate,
                AppointmentTime = ah.AppointmentTime,
                Status = ah.Status,
                CreatedAt = ah.CreatedAt
            }).ToList();
        }
        public async Task<List<AppointmentHistoryResponse>> GetAllAppointmentHistoriesByUserAsync(int userId)
        {
            var appointmentHistories = await _unitOfWork.AppointmentHistories.GetAppointmentHistoriesByUserIdAsync(userId);

            if (appointmentHistories == null || !appointmentHistories.Any())
            {
                return new List<AppointmentHistoryResponse>();
            }

            return appointmentHistories.Select(ah => new AppointmentHistoryResponse
            {
                PatientName = ah.PatientName,
                PatientDOB = ah.PatientDOB,
                PhoneNumber = ah.PhoneNumber,
                MethodName = ah.MethodName,
                PartnerName = ah.PartnerName,
                PartnerDOB = ah.PartnerDOB,
                DoctorName = ah.DoctorName,
                AppointmentDate = ah.AppointmentDate,
                AppointmentTime = ah.AppointmentTime,
                Status = ah.Status,
                CreatedAt = ah.CreatedAt
            }).ToList();
        }

        public async Task<AppointmentHistoryResponse> GetAppointmentHistoryByIdAsync(int appointmentHistoryId)
        {
            var appointmentHistory = await _unitOfWork.AppointmentHistories.GetByIdAsync(appointmentHistoryId);
            if (appointmentHistory == null)
            {
                throw new Exception("Appointment history not found");
            }
            return new AppointmentHistoryResponse
            {
                PatientName = appointmentHistory.PatientName,
                PatientDOB = appointmentHistory.PatientDOB,
                PhoneNumber = appointmentHistory.PhoneNumber,
                MethodName = appointmentHistory.MethodName,
                PartnerName = appointmentHistory.PartnerName,
                PartnerDOB = appointmentHistory.PartnerDOB,
                DoctorName = appointmentHistory.DoctorName,
                AppointmentDate = appointmentHistory.AppointmentDate,
                AppointmentTime = appointmentHistory.AppointmentTime,
                Status = appointmentHistory.Status,
                CreatedAt = appointmentHistory.CreatedAt
            };
        }
    }
}

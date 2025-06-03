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
    public class AppointmentHistoryService : IAppoimentHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentHistoryService(IUnitOfWork unitOfWork)
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

        public Task<List<AppointmentHistoryResponse>> GetAllAppointmentHistoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentHistoryResponse> GetAppointmentHistoryByIdAsync(int appointmentHistoryId)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class AppoimentService : IAppoimentService
    {
        public readonly IUnitOfWork _unitOfWork;
        public AppoimentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest appointment, int userId, int doctorId, int t)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(userId);
            //var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(userId);
            var newAppointment = new Appointment
            {
                UserId = appointment.UserId,
                TreatmentMethodId = appointment.TreatmentMethodId,
                PartnerName = appointment.PartnerName,
                PartnerDOB = DateOnly.MinValue,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.SaveAsync();
            return new AppointmentResponse
            {
                PatientName = newAppointment.User.FullName,
                PatientDOB = newAppointment.User.DateOfBirth,
                PhoneNumber = newAppointment.User.Phone,
                MethodName = newAppointment.TreatmentMethodId,
                PartnerName = newAppointment.PartnerName,
                PartnerDOB = newAppointment.PartnerDOB,
                DoctorId = newAppointment.DoctorId,
                AppointmentDate = newAppointment.AppointmentDate,
                AppointmentTime = newAppointment.AppointmentTime,
                Status = newAppointment.Status,
                CreatedAt = newAppointment.CreatedAt
            };
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
                return false;
            }
            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<AppointmentResponse>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAppointmentsAsync();
            if(appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found");
            }
            return appointments.Select(a => new AppointmentResponse
            {
                PatientName = a.User.FullName,
                PatientDOB = a.User.DateOfBirth,
                PhoneNumber = a.User.Phone,
                MethodName = a.TreatmentMethodId,
                PartnerName = a.PartnerName,
                PartnerDOB = a.PartnerDOB,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

        public async Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            return new AppointmentResponse
            {
                PatientName = appointment.User.FullName,
                PatientDOB = appointment.User.DateOfBirth,
                PhoneNumber = appointment.User.Phone,
                MethodName = appointment.TreatmentMethodId,
                PartnerName = appointment.PartnerName,
                PartnerDOB = appointment.PartnerDOB,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = appointment.Status,
                CreatedAt = appointment.CreatedAt
            };
        }

        public async Task<AppointmentResponse> UpdateAppointmentAsync(int id, UpdateAppointmentRequest appointment)
        {
            var existingAppointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                throw new Exception("Appointment not found");
            }

            // Update only the properties that are provided in the request
            if (appointment.UserId.HasValue)
                existingAppointment.UserId = appointment.UserId.Value;

            if (appointment.TreatmentMethodId.HasValue)
                existingAppointment.TreatmentMethodId = appointment.TreatmentMethodId.Value;

            if (!string.IsNullOrEmpty(appointment.PartnerName))
                existingAppointment.PartnerName = appointment.PartnerName;

            if (appointment.PartnerDOB.HasValue)
                existingAppointment.PartnerDOB = appointment.PartnerDOB.Value;

            if (appointment.DoctorId.HasValue)
                existingAppointment.DoctorId = appointment.DoctorId.Value;

            //if (appointment.AppointmentDate.HasValue)
            //    existingAppointment.AppointmentDate = appointment.AppointmentDate.Value;

            //if (appointment.AppointmentTime.HasValue)
            //    existingAppointment.AppointmentTime = appointment.AppointmentTime.Value;

            if (!string.IsNullOrEmpty(appointment.Status))
                existingAppointment.Status = appointment.Status;

            // Update the appointment using the repository
            await _unitOfWork.Appointments.UpdateAppointmentAsync(existingAppointment);
            await _unitOfWork.SaveAsync();

            // Refresh the appointment data to get related entities
            var updatedAppointment = await _unitOfWork.Appointments.GetAppointmentByIdAsync(id);

            // Map to response
            return new AppointmentResponse
            {
                PatientName = updatedAppointment.User.FullName,
                PatientDOB = updatedAppointment.User.DateOfBirth,
                PhoneNumber = updatedAppointment.User.Phone,
                MethodName = updatedAppointment.TreatmentMethodId,
                PartnerName = updatedAppointment.PartnerName,
                PartnerDOB = updatedAppointment.PartnerDOB,
                DoctorId = updatedAppointment.DoctorId,
                AppointmentDate = updatedAppointment.AppointmentDate,
                AppointmentTime = updatedAppointment.AppointmentTime,
                Status = updatedAppointment.Status,
                CreatedAt = updatedAppointment.CreatedAt
            };
        }

    }
}

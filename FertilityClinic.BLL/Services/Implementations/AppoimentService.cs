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
        public async Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest appointment, int userId, int doctorId, int partnerId, int treatmentMethodID)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var partner = await _unitOfWork.Users.GetByIdAsync(partnerId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(userId);
            //var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(userId);
            var newAppointment = new Appointment
            {
                UserId = user.UserId,
                PartnerId = user.PartnerId,
                TreatmentMethodId = treatmentMethodID,
                DoctorId = doctor.DoctorId,
                AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate), // Fix for CS0029
                AppointmentTime = appointment.AppointmentTime,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.SaveAsync();
            return new AppointmentResponse
            {
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
                MethodName = "xxx",
                PartnerName = partner.FullName,
                PartnerDOB = partner.DateOfBirth,
                DoctorName = doctor.User.FullName,
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
                MethodName = "methodname",
                PartnerName = a.Partner.FullName,
                PartnerDOB = a.Partner.DateOfBirth,
                DoctorName = a.User.FullName,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

        public async Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            var user = await _unitOfWork.Users.GetByIdAsync(appointment.UserId);
            var partner = await _unitOfWork.Users.GetByIdAsync(appointment.PartnerId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(appointment.DoctorId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            return new AppointmentResponse
            {
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
                MethodName = "methodname",
                PartnerName = partner.FullName,
                PartnerDOB = partner.DateOfBirth,
                DoctorName = doctor.User.FullName,
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
                existingAppointment.Partner.FullName = appointment.PartnerName;

            if (appointment.PartnerDOB.HasValue)
                existingAppointment.Partner.DateOfBirth = appointment.PartnerDOB.Value;

            if (appointment.DoctorId.HasValue)
                existingAppointment.DoctorId = appointment.DoctorId.Value;

            // Fix for CS8629: Ensure nullable AppointmentDate is not null before accessing its Value
            if (appointment.AppointmentDate.HasValue)
                existingAppointment.AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate.Value);

            // Fix for CS0029 and CS8629: Convert TimeSpan to TimeOnly and ensure nullable AppointmentTime is not null
            if (appointment.AppointmentTime.HasValue)
                existingAppointment.AppointmentTime = appointment.AppointmentTime.Value;

            if (!string.IsNullOrEmpty(appointment.Status))
                existingAppointment.Status = appointment.Status;

            // Update the appointment using the repository
            await _unitOfWork.Appointments.UpdateAppointmentAsync(existingAppointment);
            await _unitOfWork.SaveAsync();

            // Refresh the appointment data to get related entities
            var updatedAppointment = await _unitOfWork.Appointments.GetAppointmentByIdAsync(id);
            var user = await _unitOfWork.Users.GetByIdAsync(updatedAppointment.UserId);
            var partner = await _unitOfWork.Users.GetByIdAsync(updatedAppointment.PartnerId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(updatedAppointment.DoctorId);

            // Create appointment history
            var appointmentHistory = new AppointmentHistory
            {
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
                MethodName = updatedAppointment.TreatmentMethodId.ToString(), // You might want to get the actual method name
                PartnerName = partner.FullName,
                PartnerDOB = partner.DateOfBirth,
                DoctorName = doctor.User.FullName,
                AppointmentDate = updatedAppointment.AppointmentDate,
                AppointmentTime = updatedAppointment.AppointmentTime,
                Status = updatedAppointment.Status,
                CreatedAt = DateTime.Now
            };

            // Save appointment history
            await _unitOfWork.AppointmentHistories.AddAsync(appointmentHistory);

            // Map to response
            return new AppointmentResponse
            {
                PatientName = updatedAppointment.User.FullName,
                PatientDOB = updatedAppointment.User.DateOfBirth,
                PhoneNumber = updatedAppointment.User.Phone,
                MethodName = "methodname",
                PartnerName = updatedAppointment.Partner.FullName,
                PartnerDOB = updatedAppointment.Partner.DateOfBirth,
                DoctorName = updatedAppointment.User.FullName,
                AppointmentDate = updatedAppointment.AppointmentDate,
                AppointmentTime = updatedAppointment.AppointmentTime,
                Status = updatedAppointment.Status,
                CreatedAt = updatedAppointment.CreatedAt
            };
        }

    }
}

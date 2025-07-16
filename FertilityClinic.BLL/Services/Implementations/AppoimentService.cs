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
using static FertilityClinic.DTO.Constants.APIEndPoints;
using Appointment = FertilityClinic.DAL.Models.Appointment;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class AppoimentService : IAppoimentService
    {
        public readonly IUnitOfWork _unitOfWork;
        public AppoimentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest appointment, int userId, int doctorId)
        {
            // Validate inputs
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            if (user.PartnerId <= 0)
                throw new ArgumentException("User does not have a partner assigned");

            if (!user.PartnerId.HasValue || user.PartnerId <= 0)
                throw new ArgumentException("User does not have a partner assigned");
            var partner = await _unitOfWork.Partners.GetByIdAsync(user.PartnerId.Value);

            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
                throw new ArgumentException("Doctor not found");

            var appointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate);

            // Check if doctor has any appointment at the exact same time
            var isDoctorBusy = await _unitOfWork.Appointments.IsAppointmentTimeConflictAsync(
                doctorId, appointmentDate, appointment.AppointmentTime);

            if (isDoctorBusy)
            {
                throw new InvalidOperationException("Doctor already has an appointment at this time");
            }

            var patientHasAppointment = await _unitOfWork.Appointments.IsPatientHasAppointmentOnDateAsync(
                userId, appointmentDate);

            if (patientHasAppointment)
            {
                throw new InvalidOperationException("Patient already has an appointment on this date");
            }

            // Optional: Check for time slot conflicts with buffer time
            var hasTimeSlotConflict = await _unitOfWork.Appointments.IsTimeSlotConflictAsync(
                doctorId, appointmentDate, appointment.AppointmentTime, 30);

            if (hasTimeSlotConflict)
            {
                throw new InvalidOperationException("This time slot conflicts with another appointment. Please choose a different time");
            }

            // Create new appointment with status "Successfully" since all checks passed
            var newAppointment = new Appointment
            {
                UserId = userId,
                PartnerId = user.PartnerId.Value,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = "Successfully", // Changed from "Pending" to "Successfully"
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.SaveAsync();

            // Create appointment history record
            var appointmentHistory = new DAL.Models.AppointmentHistory
            {
                UserId = user.UserId,
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PatientGender = user.Gender,
                PhoneNumber = user.Phone,
                PartnerName = partner?.FullName,
                PartnerDOB = partner?.DateOfBirth,
                PartnerGender = partner?.Gender,
                DoctorName = doctor.User?.FullName,
                AppointmentDate = newAppointment.AppointmentDate,
                AppointmentTime = newAppointment.AppointmentTime,
                Status = newAppointment.Status, // "Successfully"
                CreatedAt = newAppointment.CreatedAt
            };
            await _unitOfWork.AppointmentHistories.AddAsync(appointmentHistory);
            await _unitOfWork.SaveAsync();

            return new AppointmentResponse
            {
                AppointmentId = newAppointment.AppointmentId,
                User = new UserResponse
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    HealthInsuranceId = user.HealthInsuranceId,
                    NationalId = user.NationalId,
                    Address = user.Address,
                    IsMarried = user.IsMarried ?? false,
                    Partner = partner != null ? new PartnerResponse
                    {
                        PartnerId = partner.PartnerId,
                        FullName = partner.FullName,
                        DateOfBirth = partner.DateOfBirth,
                        Gender = partner.Gender,
                        Phone = partner.Phone,
                        NationalId = partner.NationalId,
                        HealthInsuranceId = partner.HealthInsuranceId
                    } : null
                },
                DoctorName = doctor.User?.FullName ?? "Unknown",
                AppointmentDate = newAppointment.AppointmentDate,
                AppointmentTime = newAppointment.AppointmentTime,
                Status = newAppointment.Status, // "Successfully"
                CreatedAt = newAppointment.CreatedAt
            };
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<AppointmentResponse>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAppointmentsAsync();
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found");
            }
            return appointments.Select(a => new AppointmentResponse
            {
                AppointmentId = a.AppointmentId,
                User = new UserResponse
                {
                    UserId = a.User.UserId,
                    FullName = a.User.FullName,
                    DateOfBirth = a.User.DateOfBirth,
                    Gender = a.User.Gender,
                    PhoneNumber = a.User.Phone,
                    Email = a.User.Email,
                    HealthInsuranceId = a.User.HealthInsuranceId,
                    NationalId = a.User.NationalId,
                    Address = a.User.Address,
                    IsMarried = a.User.IsMarried ?? false,
                    Partner = a.Partner != null ? new PartnerResponse
                    {
                        PartnerId = a.Partner.PartnerId,
                        FullName = a.Partner.FullName,
                        DateOfBirth = a.Partner.DateOfBirth,
                        Gender = a.Partner.Gender,
                        Phone = a.Partner.Phone,
                        NationalId = a.Partner.NationalId,
                        HealthInsuranceId = a.Partner.HealthInsuranceId
                    } : null
                },
                DoctorName = a.Doctor?.User?.FullName ?? "Unknown",
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

            var user = await _unitOfWork.Users.GetByIdAsync(appointment.UserId);
            if (user == null)
            {
                throw new Exception($"User with ID {appointment.UserId} not found");
            }

            var partner = await _unitOfWork.Partners.GetByIdAsync(appointment.PartnerId);
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(appointment.DoctorId);
            if (doctor == null)
            {
                throw new Exception($"Doctor with ID {appointment.DoctorId} not found");
            }

            return new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                User = new UserResponse
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    HealthInsuranceId = user.HealthInsuranceId,
                    NationalId = user.NationalId,
                    Address = user.Address,
                    IsMarried = user.IsMarried ?? false,
                    Partner = partner != null ? new PartnerResponse
                    {
                        PartnerId = partner.PartnerId,
                        FullName = partner.FullName,
                        DateOfBirth = partner.DateOfBirth,
                        Gender = partner.Gender,
                        Phone = partner.Phone,
                        NationalId = partner.NationalId,
                        HealthInsuranceId = partner.HealthInsuranceId
                    } : null
                },
                DoctorName = doctor.User?.FullName ?? "Unknown",
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

            var user = await _unitOfWork.Users.GetByIdAsync(existingAppointment.UserId);
            var partner = await _unitOfWork.Partners.GetByIdAsync(existingAppointment.PartnerId);
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(existingAppointment.DoctorId);

            // Update only the properties that are provided in the request
            if (appointment.UserId.HasValue)
                existingAppointment.UserId = appointment.UserId.Value;

            if (appointment.DoctorId.HasValue)
                existingAppointment.DoctorId = appointment.DoctorId.Value;

            if (appointment.AppointmentDate.HasValue)
                existingAppointment.AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate.Value);

            if (appointment.AppointmentTime.HasValue)
                existingAppointment.AppointmentTime = appointment.AppointmentTime.Value;

            if (!string.IsNullOrEmpty(appointment.Status))
                existingAppointment.Status = appointment.Status;

            await _unitOfWork.Appointments.UpdateAppointmentAsync(existingAppointment);

            // Create appointment history
            var appointmentHistory = new DAL.Models.AppointmentHistory
            {
                UserId = user.UserId,
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PatientGender = user.Gender,
                PhoneNumber = user.Phone,
                PartnerName = partner?.FullName,
                PartnerDOB = partner?.DateOfBirth,
                PartnerGender = partner?.Gender,
                DoctorName = doctor.User?.FullName,
                AppointmentDate = existingAppointment.AppointmentDate,
                AppointmentTime = existingAppointment.AppointmentTime,
                Status = existingAppointment.Status,
                CreatedAt = existingAppointment.CreatedAt
            };

            await _unitOfWork.AppointmentHistories.AddAsync(appointmentHistory);
            await _unitOfWork.SaveAsync();

            return new AppointmentResponse
            {
                AppointmentId = existingAppointment.AppointmentId,
                User = new UserResponse
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    HealthInsuranceId = user.HealthInsuranceId,
                    NationalId = user.NationalId,
                    Address = user.Address,
                    IsMarried = user.IsMarried ?? false,
                    Partner = partner != null ? new PartnerResponse
                    {
                        PartnerId = partner.PartnerId,
                        FullName = partner.FullName,
                        DateOfBirth = partner.DateOfBirth,
                        Gender = partner.Gender,
                        Phone = partner.Phone,
                        NationalId = partner.NationalId,
                        HealthInsuranceId = partner.HealthInsuranceId
                    } : null
                },
                DoctorName = doctor.User?.FullName ?? "Unknown",
                AppointmentDate = existingAppointment.AppointmentDate,
                AppointmentTime = existingAppointment.AppointmentTime,
                Status = existingAppointment.Status,
                CreatedAt = existingAppointment.CreatedAt
            };
        }

        public async Task<List<AppointmentResponse>> GetAppointmentsByUserIdAsync(int userId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByUserIdAsync(userId);

            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found for this user.");
            }

            return appointments.Select(a => new AppointmentResponse
            {
                AppointmentId = a.AppointmentId,
                User = new UserResponse
                {
                    UserId = a.User.UserId,
                    FullName = a.User.FullName,
                    DateOfBirth = a.User.DateOfBirth,
                    Gender = a.User.Gender,
                    PhoneNumber = a.User.Phone,
                    Email = a.User.Email,
                    HealthInsuranceId = a.User.HealthInsuranceId,
                    NationalId =a.User.NationalId,
                    Address = a.User.Address,
                    IsMarried = a.User.IsMarried ?? false,
                    Partner = a.Partner != null ? new PartnerResponse
                    {
                        PartnerId = a.Partner.PartnerId,
                        FullName = a.Partner.FullName,
                        DateOfBirth = a.Partner.DateOfBirth,
                        Gender = a.Partner.Gender,
                        Phone = a.Partner.Phone,
                        NationalId = a.Partner.NationalId,
                        HealthInsuranceId = a.Partner.HealthInsuranceId
                    } : null
                },
                DoctorName = a.Doctor?.User?.FullName ?? "Unknown",
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}
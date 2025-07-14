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


        public async Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest appointment, int userId, int doctorId)
        {
            // Check xem doctor đã có process chưa
            /*var processes = await _unitOfWork.TreatmentProcesses.GetAllAsync(); // Hoặc method tương tự
            var hasProcess = processes.Any(p => p.DoctorId == appointment.DoctorId);

            if (hasProcess)
            {
                throw new Exception($"Bác sĩ có ID {appointment.DoctorId} đã có quy trình điều trị, không thể tạo lịch hẹn.");
            }*/
            // Validate inputs
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            if (user.PartnerId <= 0)
                throw new ArgumentException("User does not have a partner assigned");


            if (!user.PartnerId.HasValue || user.PartnerId <= 0)
                throw new ArgumentException("User does not have a partner assigned");
            var partner = await _unitOfWork.Partners.GetByIdAsync(user.PartnerId.Value);

            //if (partner == null)
            //    throw new ArgumentException("Partner not found");

            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(doctorId); // Fix: use doctorId instead of userId
            if (doctor == null)
                throw new ArgumentException("Doctor not found");

            var appointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate);

            // 1. Check if doctor has any appointment at the exact same time
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

            // Optional: Check for time slot conflicts with buffer time (e.g., 30 minutes)
            var hasTimeSlotConflict = await _unitOfWork.Appointments.IsTimeSlotConflictAsync(
                doctorId, appointmentDate, appointment.AppointmentTime, 30);

            if (hasTimeSlotConflict)
            {
                throw new InvalidOperationException("This time slot conflicts with another appointment. Please choose a different time");
            }

            // Create new appointment
            var newAppointment = new Appointment
            {
                UserId = userId,
                PartnerId = user.PartnerId.Value,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.SaveAsync();
            return new AppointmentResponse
            {
                AppointmentId = newAppointment.AppointmentId,  // Add this
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
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
                AppointmentId = a.AppointmentId,
                PatientName = a.User.FullName,
                PatientDOB = a.User.DateOfBirth,
                PhoneNumber = a.User.Phone,
                //MethodName = "methodname",
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
            // First, get and validate the appointment
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }

            // Get and validate the user
            var user = await _unitOfWork.Users.GetByIdAsync(appointment.UserId);
            if (user == null)
            {
                throw new Exception($"User with ID {appointment.UserId} not found");
            }

            // Get and validate the partner
            var partner = await _unitOfWork.Users.GetByIdAsync(appointment.PartnerId);
            if (partner == null)
            {
                throw new Exception($"Partner with ID {appointment.PartnerId} not found");
            }

            // Get and validate the doctor
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(appointment.DoctorId);
            if (doctor == null)
            {
                throw new Exception($"Doctor with ID {appointment.DoctorId} not found");
            }

            if (doctor.User == null)
            {
                throw new Exception($"User details for Doctor with ID {appointment.DoctorId} not found");
            }

            // Create and return the response
            return new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
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
            var user = await _unitOfWork.Users.GetByIdAsync(existingAppointment.UserId);
            var partner = await _unitOfWork.Users.GetByIdAsync(existingAppointment.PartnerId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(existingAppointment.DoctorId);
            if (existingAppointment == null)
            {
                throw new Exception("Appointment not found");
            }

            // Update only the properties that are provided in the request
            if (appointment.UserId.HasValue)
                existingAppointment.UserId = appointment.UserId.Value;

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
            // Create appointment history
            var appointmentHistory = new AppointmentHistory
            {
                UserId = user.UserId,
                PatientName = user.FullName,
                PatientDOB = user.DateOfBirth,
                PhoneNumber = user.Phone,
                //MethodName = "methodName",
                PartnerName = partner.FullName,
                PartnerDOB = partner.DateOfBirth,
                DoctorName = doctor.User.FullName,
                AppointmentDate = existingAppointment.AppointmentDate,
                AppointmentTime = existingAppointment.AppointmentTime,
                Status = existingAppointment.Status,
                CreatedAt = existingAppointment.CreatedAt
            };

            // Save appointment history
            await _unitOfWork.AppointmentHistories.AddAsync(appointmentHistory);
            await _unitOfWork.SaveAsync();

            // Map to response
            return new AppointmentResponse
            {
                PatientName = existingAppointment.User.FullName,
                PatientDOB = existingAppointment.User.DateOfBirth,
                PhoneNumber = existingAppointment.User.Phone,
                //MethodName = "methodname",
                PartnerName = existingAppointment.Partner.FullName,
                PartnerDOB = existingAppointment.Partner.DateOfBirth,
                DoctorName = existingAppointment.User.FullName,
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
                PatientName = a.User.FullName,
                PatientDOB = a.User.DateOfBirth,
                PhoneNumber = a.User.Phone,
                PartnerName = a.Partner?.FullName,
                PartnerDOB = a.Partner?.DateOfBirth,
                DoctorName = a.Doctor?.User?.FullName ?? "Unknown",
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

    }
}

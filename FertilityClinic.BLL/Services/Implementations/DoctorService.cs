﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Implementations;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Doctor> _doctorRepository;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DoctorResponse> CreateDoctorAsync(DoctorRequest request, int userId)
        {
            
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var doctor = new Doctor
            {
                UserId = userId,
                Avatar = request.Avatar,
                Specialization = request.Specialization,
                Degree = request.Degree,
                Certifications = request.Certifications,
                ExperienceYears = request.ExperienceYear, // Note: property name difference
                Bio = request.Bio,
                Education = request.Education ?? new List<string>()
            };
            
            user.Role = "Doctor";
            await _unitOfWork.Users.UpdateUserAsync(user);

            // Add doctor using Unit of Work
            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.SaveAsync();
            return new DoctorResponse {
                DoctorId = doctor.DoctorId,
                DoctorName = user.FullName,
                Avatar = doctor.Avatar,
                Specialization = doctor.Specialization,
                Degree = doctor.Degree,
                Certifications = doctor.Certifications,
                ExperienceYear = doctor.ExperienceYears, // Note: property name difference
                Bio = doctor.Bio,
                Education = doctor.Education
            };
        }        

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
                return false;
            }
            // Remove doctor
            _unitOfWork.Doctors.Remove(doctor);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<DoctorResponse>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllDoctorsAsync();
            if(doctors == null || !doctors.Any())
                throw new Exception("No doctors found");
            return doctors.Select(d => new DoctorResponse
            {
                DoctorId = d.DoctorId,
                DoctorName = d.User.FullName,
                Avatar = d.Avatar,
                Specialization = d.Specialization,
                Degree = d.Degree,
                Certifications = d.Certifications,
                ExperienceYear = d.ExperienceYears, // Note: property name difference
                Bio = d.Bio,
                Education = d.Education
            }).ToList();
        }

        public async Task<DoctorResponse?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null)
                throw new Exception("Doctor not found");
            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.User.FullName,
                Avatar = doctor.Avatar,
                Specialization = doctor.Specialization,
                Degree = doctor.Degree,
                Certifications = doctor.Certifications,
                ExperienceYear = doctor.ExperienceYears, // Note: property name difference
                Bio = doctor.Bio,
                Education = doctor.Education
            };
            
        }

        public async Task<DoctorResponse> UpdateDoctorAsync(int id, UpdateDoctorRequest request)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null)
                throw new Exception("Doctor not found");
            if (!string.IsNullOrEmpty(request.Avatar))
                doctor.Avatar = request.Avatar;

            if (!string.IsNullOrEmpty(request.Specialization))
                doctor.Specialization = request.Specialization;

            if (!string.IsNullOrEmpty(request.Degree))
                doctor.Degree = request.Degree;

            if (!string.IsNullOrEmpty(request.Certifications))
                doctor.Certifications = request.Certifications;

            if (request.ExperienceYear.HasValue)
                doctor.ExperienceYears = request.ExperienceYear.Value;

            if (!string.IsNullOrEmpty(request.Bio))
                doctor.Bio = request.Bio;

            if (request.Education != null)
                doctor.Education = request.Education;

            // Update doctor
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.SaveAsync();

            // Fetch updated doctor with user data
            var updatedDoctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            return new DoctorResponse
            {
                DoctorId = updatedDoctor.DoctorId,
                DoctorName = updatedDoctor.User.FullName,
                Avatar = updatedDoctor.Avatar,
                Specialization = updatedDoctor.Specialization,
                Degree = updatedDoctor.Degree,
                Certifications = updatedDoctor.Certifications,
                ExperienceYear = updatedDoctor.ExperienceYears, // Note: property name difference
                Bio = updatedDoctor.Bio,
                Education = updatedDoctor.Education
            };
        }
    }
}

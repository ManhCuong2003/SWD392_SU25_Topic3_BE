using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PrescriptionResponse> CreatePrescriptionAsync(CreatePrescriptionRequest request)
        {
            var prescription = new Prescription
            {
                DoctorId = request.DoctorId,
                UserId = request.UserId,
                Status = request.Status,
                PrescribedDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                Items = request.Items.Select(i => new PrescriptionItem
                {
                    PillId = i.PillId,
                    Dosage = i.Dosage,
                    Frequency = i.Frequency,
                    Duration = i.Duration,
                    Quantity = i.Quantity,
                    Instructions = i.Instructions
                }).ToList()
            };

            _unitOfWork.Prescriptions.AddAsync(prescription);
            await _unitOfWork.SaveAsync();
            return new PrescriptionResponse
            {
                PrescriptionId = prescription.PrescriptionId,
                Status = prescription.Status,
                PrescribedDate = prescription.PrescribedDate,
                DoctorId = prescription.DoctorId,
                UserId = prescription.UserId,
                Items = prescription.Items.Select(i => new PrescriptionItemResponse
                {
                    PillId = i.PillId,
                    Dosage = i.Dosage,
                    Frequency = i.Frequency,
                    Duration = i.Duration,
                    Quantity = i.Quantity,
                    Instructions = i.Instructions
                }).ToList()
            };
        }

        

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
            if (prescription == null)
            {
                throw new Exception("Prescription not found");
            }
            _unitOfWork.Prescriptions.Remove(prescription);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<PrescriptionResponse>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _unitOfWork.Prescriptions.GetAllPrescriptionsAsync();
            {
                if (prescriptions == null || !prescriptions.Any())
                {
                    throw new Exception("No Prescription found");
                }
                return prescriptions.Select(p => new PrescriptionResponse
                {
                    PrescriptionId = p.PrescriptionId,
                    Status = p.Status,
                    PrescribedDate = p.PrescribedDate,
                    DoctorId = p.DoctorId,
                    UserId = p.UserId,
                    Items = p.Items.Select(i => new PrescriptionItemResponse
                    {
                        PillId = i.PillId,
                        Dosage = i.Dosage,
                        Frequency = i.Frequency,
                        Duration = i.Duration,
                        Quantity = i.Quantity,
                        Instructions = i.Instructions
                    }).ToList() ?? new List<PrescriptionItemResponse>()
                }).ToList();
            }
        }
        public async Task<PrescriptionResponse?> GetPrescriptionByIdAsync(int id)
        {
            var prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);

            if (prescription == null)
                throw new Exception("Prescription not found");

            return new PrescriptionResponse
            {
                PrescriptionId = prescription.PrescriptionId,
                Status = prescription.Status,
                PrescribedDate = prescription.PrescribedDate,
                DoctorId = prescription.DoctorId,
                UserId = prescription.UserId,
                Items = prescription.Items?.Select(i => new PrescriptionItemResponse
                {
                    PillId = i.PillId,
                    Dosage = i.Dosage,
                    Frequency = i.Frequency,
                    Duration = i.Duration,
                    Quantity = i.Quantity,
                    Instructions = i.Instructions
                }).ToList() ?? new List<PrescriptionItemResponse>()
            };
        }

        public async Task<PrescriptionResponse> UpdatePrescriptionAsync(int id, UpdatePrescriptionRequest request)
        {
            var prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);
            if (prescription == null)
                throw new Exception("Prescription not found");

            // Update fields
            prescription.Status = request.Status ?? prescription.Status;
            prescription.PrescribedDate = request.PrescribedDate;
            prescription.StartDate = request.StartDate ?? prescription.StartDate;
            prescription.EndDate = request.EndDate ?? prescription.EndDate;
            prescription.UpdatedAt = DateTime.UtcNow;

            // Cập nhật danh sách thuốc
            prescription.Items.Clear(); // Xóa hết rồi thêm lại (cách đơn giản)
            foreach (var itemDto in request.Items)
            {
                prescription.Items.Add(new PrescriptionItem
                {
                    PillId = itemDto.PillId,
                    Dosage = itemDto.Dosage,
                    Frequency = itemDto.Frequency,
                    Duration = itemDto.Duration,
                    Quantity = itemDto.Quantity,
                    Instructions = itemDto.Instructions
                });
            }

            _unitOfWork.Prescriptions.Update(prescription);
            await _unitOfWork.SaveAsync();

            return new PrescriptionResponse
            {
                PrescriptionId = prescription.PrescriptionId,
                Status = prescription.Status,
                PrescribedDate = prescription.PrescribedDate,
                DoctorId = prescription.DoctorId,
                UserId = prescription.UserId,
                Items = prescription.Items.Select(i => new PrescriptionItemResponse
                {
                    PillId = i.PillId,
                    Dosage = i.Dosage,
                    Frequency = i.Frequency,
                    Duration = i.Duration,
                    Quantity = i.Quantity,
                    Instructions = i.Instructions
                }).ToList()
            };
        }

    }

}

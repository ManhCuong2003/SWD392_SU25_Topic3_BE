using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Requests.FertilityClinic.DTO.Requests;
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
        private readonly FertilityClinicDbContext _context;
        public PrescriptionService(IUnitOfWork unitOfWork, FertilityClinicDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<PrescriptionResponse> CreatePrescriptionAsync(CreatePrescriptionRequest request, int userId)
        {
            // Validate phương pháp điều trị
            var method = await _unitOfWork.TreatmentMethods.GetByIdAsync(request.TreatmentMethodId);
            if (method == null)
                throw new Exception("Phương pháp điều trị không tồn tại.");

            // Validate danh sách thuốc
            if (request.Medications == null || !request.Medications.Any())
                throw new Exception("Danh sách thuốc không được để trống.");

            var prescriptionItems = new List<PrescriptionItem>();

            foreach (var m in request.Medications)
            {
                if (m.PillId <= 0)
                    throw new Exception("PillId không hợp lệ.");

                if (string.IsNullOrWhiteSpace(m.Dosage))
                    throw new Exception("Thiếu liều dùng cho thuốc.");

                if (m.Quantity == null || m.Quantity <= 0)
                    throw new Exception("Số lượng thuốc phải lớn hơn 0.");

                if (string.IsNullOrWhiteSpace(m.Unit))
                    throw new Exception("Thiếu đơn vị thuốc.");

                // Kiểm tra thuốc có tồn tại trong DB không
                var pill = await _unitOfWork.Pills.GetPillByIdAsync(m.PillId);
                if (pill == null)
                    throw new Exception($"Thuốc với ID {m.PillId} không tồn tại.");

                prescriptionItems.Add(new PrescriptionItem
                {
                    PillId = m.PillId,
                    Dosage = m.Dosage,
                    Quantity = m.Quantity.Value,
                    Unit = m.Unit
                });
            }

            var prescription = new Prescription
            {
                UserId = userId,
                //DoctorId = doctorId,
                TreatmentMethodId = request.TreatmentMethodId,
                Monitoring = request.Monitoring,
                PrescribedDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                Items = prescriptionItems
            };

            await _unitOfWork.Prescriptions.AddAsync(prescription);
            await _unitOfWork.SaveAsync();

            // Load lại Prescription để có thông tin liên kết
            var created = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(prescription.PrescriptionId);
            if (created == null)
                throw new Exception("Không thể lấy lại đơn thuốc sau khi tạo.");

            // Chuẩn bị phản hồi
            var response = new PrescriptionResponse
            {
                PrescriptionId = created.PrescriptionId,
                MethodName = method.MethodName,
                Monitoring = created.Monitoring,
                Medications = created.Items.Select(i => new PrescriptionItemResponse
                {
                    Name = i.Pill?.Name ?? "",
                    Unit = i.Pill?.Unit ?? "",
                    Quantity = i.Quantity,
                    Usage = i.Dosage,
                }).ToList()
            };

            return response;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            // Lấy đơn thuốc bao gồm các thuốc con (Items)
            var prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);
            if (prescription == null)
                return false;

            // Nếu có các thuốc con thì xoá chúng trước
            if (prescription.Items != null && prescription.Items.Any())
            {
                _context.PrescriptionItems.RemoveRange(prescription.Items); // Fix tại đây
            }

            // Xoá đơn thuốc
            _unitOfWork.Prescriptions.Remove(prescription);

            // Lưu thay đổi vào DB
            await _unitOfWork.SaveAsync();

            return true;
        }


        public async Task<List<PrescriptionResponse>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _unitOfWork.Prescriptions.GetAllPrescriptionsAsync();

            var responses = prescriptions.Select(p => new PrescriptionResponse
            {
                PrescriptionId = p.PrescriptionId,
                MethodName = p.TreatmentMethod?.MethodName ?? "",
                Monitoring = p.Monitoring,
                Medications = p.Items.Select(i => new PrescriptionItemResponse
                {
                    Name = i.Pill?.Name ?? "",
                    Unit = i.Pill?.Unit ?? "",
                    Quantity = i.Quantity,
                    Usage = i.Dosage
                }).ToList()
            }).ToList();

            return responses;
        }


        public async Task<PrescriptionResponse?> GetPrescriptionByIdAsync(int id)
        {
            var prescription = await _unitOfWork.Prescriptions.GetPrescriptionByIdAsync(id);

            if (prescription == null)
                return null;

            // Nếu prescription.Items hoặc TreatmentMethod chưa được Include → cần load đầy đủ lại:
            if (prescription.TreatmentMethod == null || prescription.Items == null || !prescription.Items.Any())
            {
                prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);
                if (prescription == null)
                    return null;
            }

            var response = new PrescriptionResponse
            {
                PrescriptionId = prescription.PrescriptionId,
                MethodName = prescription.TreatmentMethod?.MethodName ?? "",
                Monitoring = prescription.Monitoring,
                Medications = prescription.Items.Select(i => new PrescriptionItemResponse
                {
                    Name = i.Pill?.Name ?? "",
                    Unit = i.Pill?.Unit ?? "",
                    Quantity = i.Quantity,
                    Usage = i.Dosage
                }).ToList()
            };

            return response;
        }

        public async Task<PrescriptionResponse> UpdatePrescriptionAsync(int id, UpdatePrescriptionRequest request)
        {
            var prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);
            if (prescription == null)
                throw new Exception("Đơn thuốc không tồn tại.");

            // Kiểm tra TreatmentMethod mới có tồn tại không (nếu có cập nhật)
            if (request.TreatmentMethodId.HasValue)
            {
                var method = await _unitOfWork.TreatmentMethods.GetByIdAsync(request.TreatmentMethodId.Value);
                if (method == null)
                    throw new Exception("Phương pháp điều trị không hợp lệ.");
                prescription.TreatmentMethodId = request.TreatmentMethodId.Value;
            }

            // Cập nhật monitoring nếu có
            if (!string.IsNullOrWhiteSpace(request.Monitoring))
                prescription.Monitoring = request.Monitoring;

            // Nếu cập nhật thuốc mới
            if (request.Medications != null && request.Medications.Any())
            {
                // Xoá tất cả thuốc cũ
                _context.PrescriptionItems.RemoveRange(prescription.Items);

                // Thêm thuốc mới
                var newItems = new List<PrescriptionItem>();
                foreach (var m in request.Medications)
                {
                    if (m.PillId <= 0)
                        throw new Exception("PillId không hợp lệ.");

                    if (string.IsNullOrWhiteSpace(m.Dosage))
                        throw new Exception("Thiếu liều dùng cho thuốc.");

                    if (m.Quantity == null || m.Quantity <= 0)
                        throw new Exception("Số lượng thuốc phải lớn hơn 0.");

                    if (string.IsNullOrWhiteSpace(m.Unit))
                        throw new Exception("Thiếu đơn vị thuốc.");

                    var pill = await _unitOfWork.Pills.GetPillByIdAsync(m.PillId);
                    if (pill == null)
                        throw new Exception($"Thuốc với ID {m.PillId} không tồn tại.");

                    newItems.Add(new PrescriptionItem
                    {
                        PrescriptionId = id,
                        PillId = m.PillId,
                        Dosage = m.Dosage,
                        Quantity = m.Quantity.Value,
                        Unit = m.Unit
                    });
                }

                prescription.Items = newItems;
            }

            await _unitOfWork.SaveAsync();

            // Lấy lại prescription để build response
            var updated = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(prescription.PrescriptionId);
            if (updated == null)
                throw new Exception("Không thể lấy đơn thuốc sau khi cập nhật.");

            return new PrescriptionResponse
            {
                PrescriptionId = updated.PrescriptionId,
                MethodName = updated.TreatmentMethod?.MethodName ?? "",
                Monitoring = updated.Monitoring,
                Medications = updated.Items.Select(i => new PrescriptionItemResponse
                {
                    Name = i.Pill?.Name ?? "",
                    Unit = i.Pill?.Unit ?? "",
                    Quantity = i.Quantity,
                    Usage = i.Dosage
                }).ToList()
            };
        }


        public async Task<List<PrescriptionByUserResponse>> GetPrescriptionsByUserIdAsync(int userId)
        {
            var prescriptions = await _unitOfWork.Prescriptions.GetPrescriptionsByUserIdAsync(userId);

            return prescriptions.Select(p => new PrescriptionByUserResponse
            {
                PrescriptionId = p.PrescriptionId,
                TreatmentMethodId = p.TreatmentMethodId,
                
                Monitoring = p.Monitoring,
                Medications = p.Items.Select(i => new PrescriptionItemResponse
                {
                    MedicationId = i.PillId,
                    Name = i.Pill?.Name ?? "",
                    Unit = i.Pill?.Unit ?? "",
                    Quantity = i.Quantity,
                    Usage = i.Dosage
                }).ToList()
            }).ToList();
        }

    }
}
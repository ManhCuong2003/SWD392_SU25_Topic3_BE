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
    public class PillService : IPillService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PillResponse> CreatePillAsync(PillRequest request)
        {
            var existingPill = await _unitOfWork.Pills
       .FindAsync(p => p.NameAndContent!.ToLower() == request.NameAndContent!.ToLower());

            if (existingPill.Any())
                throw new Exception("Tên thuốc đã tồn tại trong hệ thống.");
            if (string.IsNullOrWhiteSpace(request.NameAndContent))
                throw new Exception("Tên thuốc không được để trống.");
            if (request.NameAndContent.Length > 255)
                throw new Exception("Tên thuốc vượt quá giới hạn 255 ký tự.");
            if (string.IsNullOrWhiteSpace(request.Unit))
                throw new Exception("Đơn vị không được để trống.");
            if (request.Quantity < 0)
                throw new Exception("Số lượng phải lớn hơn hoặc bằng 0.");
            if (request.UnitPrice < 0)
                throw new Exception("Giá tiền phải lớn hơn hoặc bằng 0.");
            var pill = new Pills
            {
                NameAndContent = request.NameAndContent,
                Description = request.Description,
                Unit = request.Unit,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.Now

            };
            await _unitOfWork.Pills.AddAsync(pill);
            await _unitOfWork.SaveAsync();
            return new PillResponse
            {
                PillId = pill.PillId,
                NameAndContent = pill.NameAndContent,
                Unit = pill.Unit,
                Quantity = pill.Quantity,
                Description = pill.Description,
                UnitPrice = pill.UnitPrice,
                CreatedAt = pill.CreatedAt,
                UpdatedAt = pill.UpdatedAt
            };

        }

        public async Task<bool> DeletePillAsync(int pillid)
        {
            var pill = await _unitOfWork.Pills.GetByIdAsync(pillid);
            if (pill == null)
            {
                throw new Exception("Pill not found");
            }
            _unitOfWork.Pills.Remove(pill);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<PillResponse>> GetAllPillsAsync()
        {
            var pills = await _unitOfWork.Pills.GetAllAsync();
            if (pills == null || !pills.Any())
            {
                throw new Exception("No pills found in the system.");
            }
            return pills.Select(p => new PillResponse
            {
                PillId = p.PillId,
                NameAndContent = p.NameAndContent,
                Unit = p.Unit,
                Quantity = p.Quantity,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }

        public async Task<PillResponse?> GetPillByIdAsync(int pillid)
        {
            var pill = await _unitOfWork.Pills.GetByIdAsync(pillid);
            if (pill == null)
            {
                throw new Exception("Pill not found");
            }
            return new PillResponse
            {
                PillId = pill.PillId,
                NameAndContent = pill.NameAndContent,
                Unit = pill.Unit,
                Quantity = pill.Quantity,
                Description = pill.Description,
                UnitPrice = pill.UnitPrice,
                CreatedAt = pill.CreatedAt,
                UpdatedAt = pill.UpdatedAt
            };
        }

        public async Task<PillResponse> UpdatePillAsync(int pillId, PillRequest request)
        {
            var existingPill = await _unitOfWork.Pills.GetByIdAsync(pillId);
            if (existingPill == null)
                throw new Exception("Không tìm thấy thuốc cần cập nhật.");

            // Nếu có thay đổi tên thuốc => kiểm tra trùng
            if (!string.IsNullOrWhiteSpace(request.NameAndContent) &&
                !request.NameAndContent.Equals(existingPill.NameAndContent, StringComparison.OrdinalIgnoreCase))
            {
                var duplicate = await _unitOfWork.Pills
                    .FindAsync(p => p.PillId != pillId &&
                                    p.NameAndContent!.ToLower() == request.NameAndContent.ToLower());

                if (duplicate.Any())
                    throw new Exception("Tên thuốc đã tồn tại trong hệ thống.");

                if (request.NameAndContent.Length > 255)
                    throw new Exception("Tên thuốc vượt quá giới hạn 255 ký tự.");

                existingPill.NameAndContent = request.NameAndContent;
            }

            // Cập nhật các field khác nếu có
            if (!string.IsNullOrWhiteSpace(request.Unit))
            {
                existingPill.Unit = request.Unit;
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                existingPill.Description = request.Description;
            }

            if (request.Quantity.HasValue)
            {
                if (request.Quantity < 0)
                    throw new Exception("Số lượng phải lớn hơn hoặc bằng 0.");
                existingPill.Quantity = request.Quantity.Value;
            }

            if (request.UnitPrice.HasValue)
            {
                if (request.UnitPrice < 0)
                    throw new Exception("Giá tiền phải lớn hơn hoặc bằng 0.");
                existingPill.UnitPrice = request.UnitPrice.Value;
            }

            existingPill.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Pills.Update(existingPill);
            await _unitOfWork.SaveAsync();

            return new PillResponse
            {
                PillId = existingPill.PillId,
                NameAndContent = existingPill.NameAndContent,
                Unit = existingPill.Unit,
                Quantity = existingPill.Quantity,
                Description = existingPill.Description,
                UnitPrice = existingPill.UnitPrice,
                CreatedAt = existingPill.CreatedAt,
                UpdatedAt = existingPill.UpdatedAt
            };
        }


    }
}

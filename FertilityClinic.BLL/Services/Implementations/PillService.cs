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
    public class PillService : IPillService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<PillResponse>> GetAllPillsAsync()
        {
            var pills = await _unitOfWork.Pills.GetAllPillsAsync();
            return pills.Select(p => new PillResponse
            {
                PillId = p.PillId,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Unit = p.Unit,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }
        public async Task<PillResponse> GetPillByIdAsync(int pillId)
        {
            var pill = await _unitOfWork.Pills.GetPillByIdAsync(pillId);
            if (pill == null)
            {
                throw new Exception("Pill not found");
            }
            return new PillResponse
            {
                PillId = pill.PillId,
                Name = pill.Name,
                Description = pill.Description,
                UnitPrice = pill.UnitPrice,
                Unit = pill.Unit,
                CreatedAt = pill.CreatedAt,
                UpdatedAt = pill.UpdatedAt
            };
        }
        public async Task<List<PillResponse>> GetPillsByNameAsync(string name)
        {
            var pill = await _unitOfWork.Pills.GetPillsByNameAsync(name);
            if (pill == null || !pill.Any())
            {
                throw new Exception("No pills found with the specified name");
            }
            return pill.Select(p => new PillResponse
            {
                PillId = p.PillId,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Unit = p.Unit,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }
        public async Task<PillResponse> AddPillAsync(PillRequest pillRequest)
        {
            var pill = new Pills
            {
                Name = pillRequest.PillName,
                Description = pillRequest.Description,
                UnitPrice = pillRequest.UnitPrice,
                Unit = pillRequest.Unit,
                RequiresPrescription = pillRequest.RequiresPrescription,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
            //validate pill name
            var existingPill = await _unitOfWork.Pills.GetPillsByNameAsync(pill.Name);
            if (existingPill != null)
            {
                throw new Exception("A pill with this name already exists.");
            }
            // Add pill using Unit of Work
            var addedPill = await _unitOfWork.Pills.AddPillAsync(pill);
            await _unitOfWork.SaveAsync();
            return new PillResponse
            {
                PillId = addedPill.PillId,
                Name = addedPill.Name,
                Description = addedPill.Description,
                UnitPrice = addedPill.UnitPrice,
                Unit = addedPill.Unit,
                CreatedAt = addedPill.CreatedAt,
                UpdatedAt = addedPill.UpdatedAt
            };
        }
        public async Task<PillResponse> UpdatePillAsync(UpdatePillRequest updatePill, int id)
        {
            var pill = await _unitOfWork.Pills.GetPillByIdAsync(id);
            if (pill == null)
            {
                throw new Exception("Pill not found");
            }
            if (!string.IsNullOrEmpty(updatePill.Name))
            {
                pill.Name = updatePill.Name;
                if (await _unitOfWork.Pills.GetPillsByNameAsync(pill.Name) != null)
                {
                    throw new Exception("A pill with this name already exists.");
                }
               /* var existingPills = await _unitOfWork.Pills.GetPillsByNameAsync(updatePill.Name);
                var duplicate = existingPills.FirstOrDefault(p => p.PillId != pill.PillId);

                if (duplicate != null)
                {
                    throw new Exception("A pill with this name already exists.");
                }*/

                pill.Name = updatePill.Name;
            }
            if (!string.IsNullOrEmpty(updatePill.Description))
            {
                pill.Description = updatePill.Description;
            }
            if (updatePill.UnitPrice.HasValue)
            {
                pill.UnitPrice = updatePill.UnitPrice.Value;
            }
            if (!string.IsNullOrEmpty(updatePill.Unit))
            {
                pill.Unit = updatePill.Unit;
            }
            if (updatePill.RequiresPrescription.HasValue)
            {
                pill.RequiresPrescription = updatePill.RequiresPrescription.Value;
            }
            pill.UpdatedAt = DateTime.UtcNow;
            // Update pill using Unit of Work
            var updatedPill = await _unitOfWork.Pills.UpdatePillAsync(pill);
            if (updatedPill == null)
            {
                throw new Exception("Failed to update pill");
            }
            return new PillResponse
            {
                PillId = updatedPill.PillId,
                Name = updatedPill.Name,
                Description = updatedPill.Description,
                UnitPrice = updatedPill.UnitPrice,
                Unit = updatedPill.Unit,
                CreatedAt = updatedPill.CreatedAt,
                UpdatedAt = updatedPill.UpdatedAt
            };
        }
        public async Task<bool> DeletePillAsync(int pillId)
        {
            var pill = await _unitOfWork.Pills.DeletePillAsync(pillId);
            if (!pill)
            {
                throw new Exception("Failed to delete pill or pill not found");
            }
            return true;
        }
    }
}
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<List<PartnerResponse>> GetAllPartnersAsync();
        Task<PartnerResponse?> GetPartnerByIdAsync(int id);
        Task<PartnerResponse> CreatePartnerAsync(PartnerRequest request, int userId);
        Task<PartnerResponse> UpdatePartnerAsync(int id, UpdatePartnerRequest request);
        Task<bool> DeletePartnerAsync(int id);
    }
}

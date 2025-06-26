using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewResponse>> GetAllReviewsAsync();
        Task<ReviewResponse?> GetReviewByIdAsync(int id);
        Task<ReviewResponse> CreateReviewAsync(ReviewRequest request, int userId, int doctorId);
        Task<ReviewResponse> UpdateReviewAsync(int id, ReviewRequest request);
        Task<bool> DeleteReviewAsync(int id);
    }
}

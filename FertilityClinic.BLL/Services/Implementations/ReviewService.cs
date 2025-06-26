using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static FertilityClinic.DTO.Constants.APIEndPoints;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ReviewResponse> CreateReviewAsync(ReviewRequest request, int userId, int doctorId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var doctor = await _unitOfWork.Users.GetByIdAsync(doctorId);
            var review = new Review
            {
                UserId = userId,
                DoctorId = doctorId,
                Rating = request.Rating,
                Comment = request.Comments,
                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveAsync();
            return new ReviewResponse
            {
                ReviewId = review.ReviewId,
                UserId = userId,
                DoctorId=doctorId,
                Rating=request.Rating,
                Comment = request.Comments,
                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetreviewByIdAsync(id);
            if (review == null)
                throw new Exception("Review not found");
            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<ReviewResponse>> GetAllReviewsAsync()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            if (reviews == null || !reviews.Any()) 
                return new List<ReviewResponse>();
            return reviews.Select(r => new ReviewResponse
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                DoctorId = r.DoctorId,
                Rating = r.Rating,
                Comment = r.Comment,
                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            }).ToList();

        }

        public async Task<ReviewResponse?> GetReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
                throw new Exception("Review not found");

            var user = await _unitOfWork.Reviews.GetByIdAsync(review.UserId);
            var doctor = await _unitOfWork.Reviews.GetByIdAsync(review.DoctorId);
            return new ReviewResponse
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                DoctorId = review.DoctorId,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
        }

        public async Task<ReviewResponse> UpdateReviewAsync(int id, ReviewRequest request)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null) 
                throw new Exception("Review not found");
            if (!string.IsNullOrEmpty(request.Comments))
                review.Comment = request.Comments;
            if (request.Rating > 0)
                review.Rating = request.Rating;
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.SaveAsync();
            var updateReview = await _unitOfWork.Reviews.GetreviewByIdAsync(id);
            return new ReviewResponse
            {
                ReviewId = updateReview.ReviewId,
                UserId = updateReview.User.UserId,
                DoctorId= updateReview.DoctorId,
                Rating= updateReview.Rating,
                Comment = updateReview.Comment
            };

        }
    }
}

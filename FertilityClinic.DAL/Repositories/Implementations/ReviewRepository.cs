using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FertilityClinic.DTO.Constants.APIEndPoints;
using Review = FertilityClinic.DAL.Models.Review;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly FertilityClinicDbContext _context;
        public ReviewRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatereviewAsync(Review review)
        {
            await AddAsync(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletereviewAsync(int id)
        {
            var review = await GetByIdAsync(id);
            if (review == null)
            {
                return false;
            }
            Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Review>> GetAllreviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Doctor)
                .ToListAsync();
        }

        public async Task<Review?> GetreviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync();
        }

        public async Task<Review> UpdatereviewAsync(Review review)
        {
            Update(review);
            await _context.SaveChangesAsync();
            return await GetreviewByIdAsync(review.ReviewId) ?? review;
        }
    }
}

using FertilityClinic.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllreviewsAsync();
        Task<Review?> GetreviewByIdAsync(int id);
       
        Task<bool> CreatereviewAsync(Review review);
        Task<Review> UpdatereviewAsync(Review review);
        Task<bool> DeletereviewAsync(int id);

    }
}

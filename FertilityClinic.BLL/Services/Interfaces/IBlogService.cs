using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogResponse>> GetAllBlogsAsync();
        Task<BlogResponse?> GetBlogByIdAsync(int id);
        Task<BlogResponse> CreateBlogAsync(BlogRequest request, int userId);
        Task<BlogResponse> UpdateBlogAsync(int id, BlogRequest request);
        Task<bool> DeleteBlogAsync(int id);
    }
}

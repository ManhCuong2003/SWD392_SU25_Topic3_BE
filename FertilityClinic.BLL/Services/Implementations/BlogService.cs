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
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogResponse> CreateBlogAsync(BlogRequest request, int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var blog = new Blog
            {
                UserId = userId,
                Title = request.Title,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                IsPublished = request.IsPublished,
                PublishedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _unitOfWork.Blogs.AddAsync(blog);
            await _unitOfWork.SaveAsync();
            return new BlogResponse
            {
                BlogId = blog.BlogId,
                UserId = userId,
                Title = request.Title,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                IsPublished = request.IsPublished,
                PublishedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog not found");
            _unitOfWork.Blogs.Remove(blog);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<BlogResponse>> GetAllBlogsAsync()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync();
            if (blogs == null || !blogs.Any())
                return new List<BlogResponse>();
            return blogs.Select(r => new BlogResponse
            {
                UserId = r.UserId,
                BlogId = r.BlogId,
                Title = r.Title,
                Content = r.Content,
                ThumbnailUrl = r.ThumbnailUrl,
                IsPublished = r.IsPublished,
                PublishedAt = r.PublishedAt,
                UpdatedAt = r.UpdatedAt,
                CreatedAt = DateTime.Now,
            }).ToList();
        }

        public async Task<BlogResponse?> GetBlogByIdAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog not found");

            var user = await _unitOfWork.Reviews.GetByIdAsync(blog.UserId);
            
            return new BlogResponse
            {
                BlogId = blog.BlogId,
                UserId = blog.UserId,
                Title = blog.Title,
                Content = blog.Content,
                ThumbnailUrl = blog.ThumbnailUrl,
                IsPublished = blog.IsPublished,
                PublishedAt = blog.PublishedAt,
                UpdatedAt = blog.UpdatedAt,
                CreatedAt = DateTime.Now
            };
        }

        public async Task<BlogResponse> UpdateBlogAsync(int id, BlogRequest request)
        {
            
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog not found");
            if (!string.IsNullOrEmpty(request.Title))
                blog.Title = request.Title;
            
            if (!string.IsNullOrEmpty(request.Content))
                blog.Content = request.Content;
            
            if (!string.IsNullOrEmpty(request.ThumbnailUrl))
                blog.ThumbnailUrl = request.ThumbnailUrl;
            blog.IsPublished = request.IsPublished;

            _unitOfWork.Blogs.Update(blog);
            await _unitOfWork.SaveAsync();
            var updateBlog = await _unitOfWork.Blogs.GetByIdAsync(id);
            return new BlogResponse
            {
                BlogId = updateBlog.BlogId,
                UserId = updateBlog.UserId,
                Title = updateBlog.Title,
                Content = updateBlog.Content,
                ThumbnailUrl = updateBlog.ThumbnailUrl,
                IsPublished = updateBlog.IsPublished,
                PublishedAt = updateBlog.PublishedAt,
                UpdatedAt = updateBlog.UpdatedAt,
                CreatedAt = DateTime.Now

            };
        }
    }
}

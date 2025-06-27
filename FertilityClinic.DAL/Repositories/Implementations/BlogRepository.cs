using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly FertilityClinicDbContext _context;
        public BlogRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateBlogAsync(Blog blog)
        {
            await AddAsync(blog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await GetByIdAsync(id);
            if (blog == null)
            {
                return false;
            }
            Remove(blog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs
                .Include(n => n.User)
                .ToListAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(n => n.User)
                .FirstOrDefaultAsync();
        }

        public async Task<Blog> UpdateBlogAsync(Blog blog)
        {
            Update(blog);
            await _context.SaveChangesAsync();
            return await GetBlogByIdAsync(blog.BlogId) ?? blog;
        }
    }
}

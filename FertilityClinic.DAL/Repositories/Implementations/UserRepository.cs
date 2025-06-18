using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DTO.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FertilityClinic.DTO.Constants.APIEndPoints;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FertilityClinicDbContext _context;

        public UserRepository(FertilityClinicDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }


        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsEmailExistsAsync(string email, int userId)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.UserId != userId);
        }

        public async Task<bool> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return false;

            user.PartnerId = dto.PartnerId;

            if (!string.IsNullOrWhiteSpace(dto.FullName)) user.FullName = dto.FullName;
            if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;



            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllActiveUsersAsync()
        {
            return await _context.Users
        .Where(u => u.Role == "User")
        .Include(u => u.Doctor)
        .ToListAsync();
        }

        public async Task<bool> HardDeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<List<User>> GetAllPatientsWithDetailsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "User")
                .Include(u => u.Appointments)
                .Include(u => u.GetAppointmentHistories)
                .Include(u => u.Doctor)
                    .ThenInclude(d => d.User)
                .ToListAsync();
        }
    }
}


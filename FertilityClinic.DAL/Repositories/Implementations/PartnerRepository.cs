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
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        private readonly FertilityClinicDbContext _context;
        public PartnerRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> CreatePartnerAsync(Partner partner)
        {
            await AddAsync(partner);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePartnerAsync(int id)
        {
            var partner = await GetPartnerByIdAsync(id);
            if (partner == null)
            {
                return false;
            }
            Remove(partner);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Partner>> GetAllPartnersAsync()
        {
            return await _context.Partners
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<Partner?> GetPartnerByIdAsync(int id)
        {
            return await _context.Partners
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PartnerId == id);
        }

        public Task<Partner?> GetPartnerByUserIdAsync(int id)
        {
            return _context.Partners
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == id);
        }

        public async Task<Partner> UpdatePartnerAsync(Partner partner)
        {
            Update(partner);
            await _context.SaveChangesAsync();
            return await GetPartnerByIdAsync(partner.PartnerId) ?? partner;
        }
    }
}

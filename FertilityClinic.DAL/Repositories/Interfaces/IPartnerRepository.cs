using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        public Task<IEnumerable<Partner>> GetAllPartnersAsync();
        public Task<Partner?> GetPartnerByIdAsync(int id);
        public Task<Partner?> GetPartnerByUserIdAsync(int id);
        public Task<bool> CreatePartnerAsync(Partner partner);
        public Task<Partner> UpdatePartnerAsync(Partner partner);
        public Task<bool> DeletePartnerAsync(int id);

    }
}

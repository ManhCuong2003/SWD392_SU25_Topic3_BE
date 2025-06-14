using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IInseminationScheduleRepository : IGenericRepository<InseminationSchedule>
    {
        Task<bool> CreateInseminationScheduleAsync(InseminationSchedule inseminationSchedule);
        Task<IEnumerable<InseminationSchedule>> GetAllInseminationSchedulesAsync();
        Task<InseminationSchedule> GetInseminationScheduleByIdAsync(int scheduleId);
        Task<InseminationSchedule> UpdateInseminationScheduleAsync(InseminationSchedule inseminationSchedule);
        Task<bool> DeleteInseminationScheduleAsync(int inseminationScheduleId);
    }
}

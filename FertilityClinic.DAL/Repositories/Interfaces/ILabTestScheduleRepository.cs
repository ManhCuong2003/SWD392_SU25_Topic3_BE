using FertilityClinic.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface ILabTestScheduleRepository : IGenericRepository<LabTestSchedule>
    {
        Task <bool> CreateLabTestScheduleAsync(LabTestSchedule labTestSchedule);
        Task<IEnumerable<LabTestSchedule>> GetAllLabTestSchedulesAsync();
        Task<LabTestSchedule> GetLabTestScheduleByIdAsync(int labTestScheduleId);
        Task<LabTestSchedule> UpdateLabTestScheduleAsync(LabTestSchedule labTestSchedule);
        Task<bool> DeleteLabTestScheduleAsync(int labTestScheduleId);
    }
}

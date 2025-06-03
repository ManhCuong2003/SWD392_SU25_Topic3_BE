using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IAppoimentHistoryRepository: IGenericRepository<AppointmentHistory>
    {
        Task<bool> CreateAppointmentHistoryAsync(AppointmentHistory appointmentHistory);
        Task<List<AppointmentHistory>> GetAllAppointmentHistoriesAsync();
        Task<AppointmentHistory> GetAppointmentHistoryByIdAsync(int appointmentHistoryId);
        Task<List<AppointmentHistory>> GetAppointmentHistoriesByUserIdAsync(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IAppoimentHistoryService
    {
        Task<AppointmentHistoryResponse> CreateAppointmentAsync(AppointmentHistoryRequest appointment);
        Task<List<AppointmentHistoryResponse>> GetAllAppointmentHistoriesAsync();
        Task<List<AppointmentHistoryResponse>> GetAllAppointmentHistoriesByUserAsync(int userId);
        Task<AppointmentHistoryResponse> GetAppointmentHistoryByIdAsync(int appointmentHistoryId);
    }
}

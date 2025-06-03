using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IAppoimentService
    {
        Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest appointment, int userId, int doctorId, int partnerId, int treatmentMethodId);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<List<AppointmentResponse>> GetAllAppointmentsAsync();
        Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId);
        Task<AppointmentResponse> UpdateAppointmentAsync(int id, UpdateAppointmentRequest appointment);
    }
}

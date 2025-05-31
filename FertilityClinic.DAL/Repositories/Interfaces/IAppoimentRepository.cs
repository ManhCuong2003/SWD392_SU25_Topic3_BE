using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IAppoimentRepository: IGenericRepository<Appointment>
    {
        Task<bool> CreateAppointmentAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
    }
}

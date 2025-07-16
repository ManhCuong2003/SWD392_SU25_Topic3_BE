using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface IDoctorRepository: IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<Doctor?> GetDoctorByNameAsync(string name);
        Task<bool> CreateDoctorAsync(Doctor doctor);
        Task<Doctor> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);
        Task<Doctor?> GetDoctorByUserIdAsync(int userId);
    }
}

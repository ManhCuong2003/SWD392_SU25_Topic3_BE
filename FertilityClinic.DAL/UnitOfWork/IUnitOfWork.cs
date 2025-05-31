using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Repositories.Interfaces;

namespace FertilityClinic.DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        //IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IUserRepository Users { get; }
        IDoctorRepository Doctors { get; }
        IAppoimentRepository Appointments { get; }
        IAppoimentHistoryRepository AppointmentHistories { get; }
        Task<int> SaveAsync();
    }
}

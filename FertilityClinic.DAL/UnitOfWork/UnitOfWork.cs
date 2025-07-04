﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories;
using FertilityClinic.DAL.Repositories.Implementations;
using FertilityClinic.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FertilityClinic.DAL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly FertilityClinicDbContext _context;
        private Hashtable _repositories;
        private bool _disposed;
        public IUserRepository Users { get; }
        public IDoctorRepository Doctors { get;  }
        public IAppoimentRepository Appointments { get; }
        public IAppoimentHistoryRepository AppointmentHistories { get; }
        public IPartnerRepository Partners { get; }

        public ITreatmentMethodRepository TreatmentMethods { get; }
        public IPillRepository Pills { get; }
        public ITreatmentProcessRepository TreatmentProcesses { get; }

        //public ILabTestScheduleRepository LabTestSchedules { get; }
        public ILabTestResultRepository LabTestResults { get; }
        public IInseminationScheduleRepository InseminationSchedules { get; }
        public IInseminationResultRepository InseminationResults { get; }

        public IReviewRepository Reviews { get; }
        public IPrescriptionRepository Prescriptions { get; }
        public INotificationRepository Notifications { get; }
        public IBlogRepository Blogs { get; }
        public UnitOfWork(FertilityClinicDbContext context, 
            IUserRepository userRepository,
            IDoctorRepository doctorRepository,
            IAppoimentRepository appoimentRepository,
            IAppoimentHistoryRepository appointmentHistories,
            IPartnerRepository partnerRepository,
            ITreatmentMethodRepository treatMentMethods,
            ITreatmentProcessRepository paymentProcessRepository,
            ITreatmentProcessRepository treatmentProcessesRepository,
            IPillRepository pills,
            ILabTestResultRepository labTestResultsRepository,
            IInseminationScheduleRepository inseminationSchedules,
            IInseminationResultRepository inseminationResults,
            IReviewRepository reviews,
            IPrescriptionRepository prescriptions,
            INotificationRepository notifications,
            IBlogRepository blogs)
        {
            _context = context;
            _repositories = new Hashtable();
            Users = userRepository;
            Doctors = doctorRepository;
            Appointments = appoimentRepository;
            AppointmentHistories = appointmentHistories;
            Partners = partnerRepository;
            TreatmentMethods = treatMentMethods;
            TreatmentProcesses = treatmentProcessesRepository;
            //LabTestSchedules = labTestSchedules;
            LabTestResults = labTestResultsRepository;
            InseminationSchedules = inseminationSchedules;
            InseminationResults = inseminationResults;
            Pills = pills;
            Reviews = reviews;
            Prescriptions = prescriptions;
            Notifications = notifications;
            Blogs = blogs;
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type]!;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

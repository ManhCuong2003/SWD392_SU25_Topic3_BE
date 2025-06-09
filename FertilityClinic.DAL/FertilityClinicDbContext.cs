using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FertilityClinic.DAL
{
    public class FertilityClinicDbContext: DbContext
    {
        public readonly IConfiguration _configuration;
    public FertilityClinicDbContext(DbContextOptions<FertilityClinicDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentMethod> TreatmentMethods { get; set; }
        public DbSet<TreatmentProcess> TreatmentProcesses { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<InseminationSchedule> InseminationSchedules { get; set; }
        public DbSet<InseminationResult> InseminationResults { get; set; }
        public DbSet<LabTestSchedule> LabTestSchedules { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection"); // DefaultConnection is defined in appsettings.json
                optionsBuilder.UseSqlServer(connectionString);

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.HasKey(p => p.PartnerId);

                // Configure one-to-one relationship between Partner and User
                entity.HasOne(p => p.User)
                      .WithOne(u => u.Partner)
                      .HasForeignKey<Partner>(p => p.UserId)  // Partner is the dependent entity
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Appointment configurations
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            //AppointmentHistory configurations
            modelBuilder.Entity<AppointmentHistory>(entity =>
            {
                entity.HasOne(a => a.User)
                .WithMany(b => b.GetAppointmentHistories)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });


            // Doctor configurations
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId); // Primary key is DoctorId

                entity.HasOne(d => d.User)
                      .WithOne(u => u.Doctor)
                      .HasForeignKey<Doctor>(d => d.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

            });

            // Review configurations
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TreatmentProcess>(entity =>
            {
                entity.HasOne(tp => tp.TreatmentMethod)
                      .WithMany(tm => tm.TreatmentProcesses) 
                      .HasForeignKey(tp => tp.TreatmentMethodId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(tp => tp.InjectionSchedules)
                      .WithOne(ins => ins.TreatmentProcess)
                      .HasForeignKey(ins => ins.TreatmentProcessId);
                entity.HasMany(tp => tp.InseminationSchedules)
                      .WithOne(insem => insem.TreatmentProcess)
                      .HasForeignKey(insem => insem.TreatmentProcessId);
                entity.HasMany(tp => tp.LabTestSchedules)
                      .WithOne(lab => lab.TreatmentProcess)
                      .HasForeignKey(lab => lab.TreatmentProcessId);
            });

            // InjectionSchedule configurations
            modelBuilder.Entity<InjectionSchedule>(entity =>
            {
                entity.HasOne(i => i.Doctor)
                    .WithMany(d => d.InjectionSchedules)
                    .HasForeignKey(i => i.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(i => i.TreatmentProcess)
                    .WithMany()
                    .HasForeignKey(i => i.TreatmentProcessId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            
            // LabTestSchedule configurations
            modelBuilder.Entity<LabTestSchedule>(entity =>
            {
                entity.HasOne(l => l.Doctor)
                    .WithMany(d => d.LabTestSchedules)
                    .HasForeignKey(l => l.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(l => l.TreatmentProcess)
                    .WithMany()
                    .HasForeignKey(l => l.TreatmentProcessId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // InseminationSchedule configurations
            modelBuilder.Entity<InseminationSchedule>(entity =>
            {
                entity.HasOne(i => i.Doctor)
                    .WithMany(d => d.InseminationSchedules)
                    .HasForeignKey(i => i.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(i => i.TreatmentProcess)
                    .WithMany()
                    .HasForeignKey(i => i.TreatmentProcessId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // LabTestResult configurations
            modelBuilder.Entity<LabTestResult>(entity =>
            {
                entity.HasOne(l => l.Doctor)
                    .WithMany(d => d.LabTestResults)
                    .HasForeignKey(l => l.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(l => l.LabTestSchedule)
                    .WithMany()
                    .HasForeignKey(l => l.LabTestScheduleId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // InseminationResult configurations
            modelBuilder.Entity<InseminationResult>(entity =>
            {
                entity.HasOne(i => i.Doctor)
                    .WithMany(d => d.InseminationResults)
                    .HasForeignKey(i => i.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(i => i.InseminationSchedule)
                    .WithMany()
                    .HasForeignKey(i => i.InseminationScheduleId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}


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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<InseminationResult> InseminationResults { get; set; }
        public DbSet<InseminationSchedule> InseminationSchedules { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<LabTestSchedule> LabTestSchedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TreatmentMethod> TreatmentMethods { get; set; }
        public DbSet<TreatmentProcess> TreatmentProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appointment configurations
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Doctor configurations
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithOne(u => u.Doctor)
                    .HasForeignKey<Doctor>(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Patient configurations
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithOne(u => u.Patient)
                    .HasForeignKey<Patient>(p => p.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Review configurations
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.Patient)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // TreatmentProcess configurations
            modelBuilder.Entity<TreatmentProcess>(entity =>
            {
                entity.HasOne(tp => tp.Patient)
                    .WithMany(p => p.TreatmentProcesses)
                    .HasForeignKey(tp => tp.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);
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


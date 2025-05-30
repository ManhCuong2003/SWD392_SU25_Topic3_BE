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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentMethod> TreatmentMethods { get; set; }
        public DbSet<TreatmentProcess> TreatmentProcesses { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<InseminationSchedule> InseminationSchedules { get; set; }
        public DbSet<InseminationResult> InseminationResults { get; set; }
        public DbSet<LabTestSchedule> LabTestSchedules { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Floor> Floors { get; set; }
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

            // Section configurations
            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(s => s.Id); // Primary key
                entity.HasMany(s => s.Floors)
                      .WithOne(f => f.Section)
                      .HasForeignKey(f => f.SectionId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            });

            // Floor configurations
            modelBuilder.Entity<Floor>(entity =>
            {
                entity.HasKey(f => f.FloorId); // Primary key
                entity.HasOne(f => f.Section)
                      .WithMany(s => s.Floors)
                      .HasForeignKey(f => f.SectionId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete
                entity.HasMany(f => f.Rooms)
                      .WithOne(r => r.Floor)
                      .HasForeignKey(r => r.FloorId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            });

            // Room configurations
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.RoomId); // Primary key is RoomId, not ID

                entity.HasOne(r => r.Floor)
                      .WithMany(f => f.Rooms)
                      .HasForeignKey(r => r.FloorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.Doctors)
                      .WithOne(d => d.Room)
                      .HasForeignKey(d => d.RoomId)
                      .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.Room)
                      .WithMany(r => r.Doctors)
                      .HasForeignKey(d => d.RoomId)
                      .OnDelete(DeleteBehavior.Restrict);
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
                entity.HasOne(tp => tp.Doctor)
                      .WithMany(d => d.TreatmentProcesses)
                      .HasForeignKey(tp => tp.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);
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


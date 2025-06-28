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
    public class FertilityClinicDbContext : DbContext
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
        
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<Pills> Pills { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Pill)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.PillId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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
                /*entity.HasMany(tp => tp.LabTestSchedules)
                      .WithOne(lab => lab.TreatmentProcess)
                      .HasForeignKey(lab => lab.TreatmentProcessId);*/
            });

            // InjectionSchedule configurations
            modelBuilder.Entity<InjectionSchedule>(entity =>
            {
                entity.HasOne(i => i.Doctor)
                    .WithMany(d => d.InjectionSchedules)
                    .HasForeignKey(i => i.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                /* entity.HasOne(i => i.TreatmentProcess)
                     .WithMany()
                     .HasForeignKey(i => i.TreatmentProcessId)
                     .OnDelete(DeleteBehavior.NoAction);*/
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasOne(b => b.User)
                      .WithMany(u => u.Blogs)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // InseminationSchedule configurations
            modelBuilder.Entity<InseminationSchedule>(entity =>
            {
                entity.HasOne(i => i.Doctor)
                    .WithMany(d => d.InseminationSchedules)
                    .HasForeignKey(i => i.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                /* entity.HasOne(i => i.TreatmentProcess)
                     .WithMany()
                     .HasForeignKey(i => i.TreatmentProcessId)
                     .OnDelete(DeleteBehavior.NoAction);*/
            });

            // LabTestResult configurations
            modelBuilder.Entity<LabTestResult>(entity =>
            {
                entity.HasOne(l => l.Doctor)
                    .WithMany(d => d.LabTestResults)
                    .HasForeignKey(l => l.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);

                /*entity.HasOne(l => l.LabTestSchedule)
                    .WithMany()
                    .HasForeignKey(l => l.LabTestScheduleId)
                    .OnDelete(DeleteBehavior.NoAction);*/
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
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<User>().HasData(
        new User
        {
            UserId = 1,
            FullName = "Admin",
            Email = "admin@email.com",
            Phone = "1234567890",
            NationalId = "ADMIN123",
            DateOfBirth = new DateOnly(1980, 1, 1),
            Gender = "Male",
            Address = "123 Admin Street",
            Password = "123",
            Role = "Admin"
        },
        new User
        {
            UserId = 2,
            FullName = "Dr. John Doe",
            Email = "doctor@email.com",
            Phone = "0987654321",
            NationalId = "DOC123456",
            DateOfBirth = new DateOnly(1975, 5, 15),
            Gender = "Male",
            Address = "456 Doctor Avenue",
            Password = "123",
            Role = "Doctor",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = 1,
                    UserId = 2, // Matches the doctor user above
                    Specialization = "Sản khoa",
                    Avatar = "https://www.future-doctor.de/wp-content/uploads/2024/11/shutterstock_2173377961-1000x667.jpg",
                    Degree = "Tiến sĩ, Bác sĩ Chuyên khoa II",
                    Experience = new List<string>
                    {
                        "2010 - Nay: Trưởng khoa Sản, Bệnh viện Medico",
      "2005 - 2010: Bác sĩ Sản khoa, Bệnh viện Phụ sản Trung ương",
                    },
                    ExperienceYears = 15,
                    Bio = "Thạc sĩ, Bác sĩ Chuyên khoa II Nguyễn Thị Thanh có hơn 15 năm kinh nghiệm trong lĩnh vực Sản phụ khoa. Bác sĩ đã từng công tác tại Bệnh viện Phụ sản Trung ương và hiện là trưởng khoa Sản tại Bệnh viện Đa khoa Quốc tế.",
                    Education = new List<string>
                    {
                        "2000 - 2004: Đại học Y Hà Nội",
      "2006 - 2008: Thạc sĩ Y học, Đại học Y Hà Nội",
      "2015 - 2017: Bác sĩ Chuyên khoa II, Đại học Y Dược TP.HCM",
                    }

                }
            );
        }
    }
}

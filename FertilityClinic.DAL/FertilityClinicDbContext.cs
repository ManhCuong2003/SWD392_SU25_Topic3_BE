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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentMethod> TreatmentMethods { get; set; }
        public DbSet<TreatmentProcess> TreatmentProcesses { get; set; }
        public DbSet<MarriageCertificate> MarriageCertificates { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<InseminationSchedule> InseminationSchedules { get; set; }
        public DbSet<InseminationResult> InseminationResults { get; set; }
        public DbSet<LabTestSchedule> LabTestSchedules { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }

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

            modelBuilder.Entity<MarriageCertificate>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.CertificateId);

                // Relationships
                entity.HasOne(e => e.Patient)
                    .WithMany()  // Assuming Patient doesn't have a navigation property back to MarriageCertificate
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Default values and timestamps
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Indexes
                entity.HasIndex(e => e.CertificateNumber)
                    .IsUnique();

                // Common validation rules
                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.DocumentUrl)
                    .HasMaxLength(255);
            });

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

            // Configure relationships and constraints
            modelBuilder.Entity<TreatmentProcess>()
                .HasOne(tp => tp.MarriageCertificate)
                .WithMany(mc => mc.TreatmentProcesses)
                .HasForeignKey(tp => tp.MarriageCertificateId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<TreatmentMethod>().HasData(
    new TreatmentMethod
    {
        TreatmentMethodId = 1,
        MethodName = "Bơm tinh trùng vào buồng tử cung",
        MethodCode = "IUI",
        Description = "Bơm tinh trùng đã qua lọc rửa trực tiếp vào buồng tử cung.",
        Category = "Basic",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Tinh trùng đạt chất lượng, buồng tử cung bình thường",
        SuccessRate = 15.00m,
        AverageDuration = 30,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 2,
        MethodName = "Thụ tinh trong ống nghiệm",
        MethodCode = "IVF",
        Description = "Thụ tinh trứng và tinh trùng trong môi trường ống nghiệm.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Phòng lab IVF đạt chuẩn",
        SuccessRate = 40.00m,
        AverageDuration = 60,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 3,
        MethodName = "Tiêm tinh trùng vào bào tương noãn",
        MethodCode = "ICSI",
        Description = "Tiêm trực tiếp một tinh trùng vào trứng để tăng khả năng thụ tinh.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Máy vi thao tác, chuyên viên có tay nghề cao",
        SuccessRate = 50.00m,
        AverageDuration = 60,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 4,
        MethodName = "Hỗ trợ phôi thoát màng",
        MethodCode = "AH",
        Description = "Tác động lên vỏ phôi để hỗ trợ thoát màng và tăng tỉ lệ làm tổ.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Laser hỗ trợ phôi thoát màng",
        SuccessRate = 30.00m,
        AverageDuration = 3,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 5,
        MethodName = "Kỹ thuật keo dính phôi",
        MethodCode = "EmbryoGlue",
        Description = "Sử dụng dung dịch keo dính hỗ trợ phôi bám vào niêm mạc tử cung.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Dung dịch hỗ trợ gắn phôi",
        SuccessRate = 20.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 6,
        MethodName = "Trưởng thành trứng non",
        MethodCode = "IVM",
        Description = "Nuôi trưởng thành trứng non bên ngoài cơ thể trước khi thụ tinh.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Môi trường nuôi trứng non",
        SuccessRate = 35.00m,
        AverageDuration = 10,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 7,
        MethodName = "Kỹ thuật sinh thiết phôi",
        MethodCode = "Biopsy",
        Description = "Lấy một số tế bào từ phôi để xét nghiệm di truyền.",
        Category = "Advanced",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Thiết bị sinh thiết, chuyên viên tay nghề cao",
        SuccessRate = 99.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 8,
        MethodName = "Kỹ thuật gom trứng",
        MethodCode = "EggRetrieval",
        Description = "Chọc hút và thu thập trứng từ buồng trứng.",
        Category = "Basic",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Thiết bị siêu âm và hút trứng",
        SuccessRate = 100.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 9,
        MethodName = "Trữ đông noãn",
        MethodCode = "EggFreezing",
        Description = "Bảo quản trứng ở nhiệt độ cực thấp để sử dụng sau.",
        Category = "Preservation",
        RequiresMarriageCertificate = false,
        TechnicalRequirements = "Kỹ thuật trữ đông tiên tiến",
        SuccessRate = 90.00m,
        AverageDuration = 30,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 10,
        MethodName = "Trữ đông phôi",
        MethodCode = "EmbryoFreezing",
        Description = "Trữ đông phôi để sử dụng trong chu kỳ điều trị sau.",
        Category = "Preservation",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Kỹ thuật trữ đông phôi",
        SuccessRate = 85.00m,
        AverageDuration = 30,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 11,
        MethodName = "Trữ đông tinh trùng",
        MethodCode = "SpermFreezing",
        Description = "Trữ đông tinh trùng để sử dụng trong tương lai.",
        Category = "Preservation",
        RequiresMarriageCertificate = false,
        TechnicalRequirements = "Nitơ lỏng bảo quản mẫu tinh trùng",
        SuccessRate = 90.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 12,
        MethodName = "Phẫu thuật nội soi buồng tử cung trong vô sinh",
        MethodCode = "HSC",
        Description = "Nội soi buồng tử cung để chẩn đoán và điều trị vô sinh.",
        Category = "Surgical",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Phòng mổ nội soi, thiết bị nội soi buồng tử cung",
        SuccessRate = 60.00m,
        AverageDuration = 7,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 13,
        MethodName = "Lấy tinh trùng từ mào tinh và tinh hoàn",
        MethodCode = "PESA_TESA",
        Description = "Phẫu thuật lấy tinh trùng trực tiếp từ mào tinh hoặc tinh hoàn.",
        Category = "Surgical",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Phẫu thuật viên có chuyên môn, vô trùng tuyệt đối",
        SuccessRate = 70.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new TreatmentMethod
    {
        TreatmentMethodId = 14,
        MethodName = "Lọc rửa tinh trùng",
        MethodCode = "SpermWash",
        Description = "Lọc rửa tinh trùng để tăng chất lượng trước IUI hoặc IVF.",
        Category = "Basic",
        RequiresMarriageCertificate = true,
        TechnicalRequirements = "Thiết bị ly tâm, kỹ thuật viên chuyên môn",
        SuccessRate = 100.00m,
        AverageDuration = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    }
);

        }
    }
}


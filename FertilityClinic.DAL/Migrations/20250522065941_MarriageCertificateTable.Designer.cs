﻿// <auto-generated />
using System;
using FertilityClinic.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    [DbContext(typeof(FertilityClinicDbContext))]
    [Migration("20250522065941_MarriageCertificateTable")]
    partial class MarriageCertificateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FertilityClinic.DAL.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("Certifications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InjectionSchedule", b =>
                {
                    b.Property<int>("InjectionScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InjectionScheduleId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InjectionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreatmentProcessId")
                        .HasColumnType("int");

                    b.Property<int?>("TreatmentProcessId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("InjectionScheduleId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("TreatmentProcessId");

                    b.HasIndex("TreatmentProcessId1");

                    b.ToTable("InjectionSchedules");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InseminationResult", b =>
                {
                    b.Property<int>("InseminationResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InseminationResultId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("InseminationScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ResultDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResultDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("InseminationResultId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("InseminationScheduleId");

                    b.ToTable("InseminationResults");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InseminationSchedule", b =>
                {
                    b.Property<int>("InseminationScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InseminationScheduleId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InseminationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreatmentProcessId")
                        .HasColumnType("int");

                    b.Property<int?>("TreatmentProcessId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("InseminationScheduleId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("TreatmentProcessId");

                    b.HasIndex("TreatmentProcessId1");

                    b.ToTable("InseminationSchedules");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.LabTestResult", b =>
                {
                    b.Property<int>("LabTestResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LabTestResultId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("LabTestScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ResultDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResultDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("LabTestResultId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("LabTestScheduleId");

                    b.ToTable("LabTestResults");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.LabTestSchedule", b =>
                {
                    b.Property<int>("LabTestScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LabTestScheduleId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreatmentProcessId")
                        .HasColumnType("int");

                    b.Property<int?>("TreatmentProcessId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("LabTestScheduleId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("TreatmentProcessId");

                    b.HasIndex("TreatmentProcessId1");

                    b.ToTable("LabTestSchedules");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.MarriageCertificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"));

                    b.Property<string>("CertificateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("DocumentUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly>("IssueDate")
                        .HasColumnType("date");

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("SpouseIdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("VerificationStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CertificateId");

                    b.HasIndex("CertificateNumber")
                        .IsUnique();

                    b.HasIndex("PatientId");

                    b.ToTable("MarriageCertificates");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedicalHistory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicalRecordCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.PriceList", b =>
                {
                    b.Property<int>("PriceListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceListId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceListId");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ReviewId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.TreatmentMethod", b =>
                {
                    b.Property<int>("TreatmentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TreatmentMethodId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MethodCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TreatmentMethodId");

                    b.ToTable("TreatmentMethods");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.TreatmentProcess", b =>
                {
                    b.Property<int>("TreatmentProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TreatmentProcessId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MarriageCertificateCertificateId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreatmentMethodId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TreatmentProcessId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MarriageCertificateCertificateId");

                    b.HasIndex("PatientId");

                    b.HasIndex("TreatmentMethodId");

                    b.ToTable("TreatmentProcesses");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Appointment", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Blog", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Doctor", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("FertilityClinic.DAL.Models.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InjectionSchedule", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("InjectionSchedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", "TreatmentProcess")
                        .WithMany()
                        .HasForeignKey("TreatmentProcessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", null)
                        .WithMany("InjectionSchedules")
                        .HasForeignKey("TreatmentProcessId1");

                    b.Navigation("Doctor");

                    b.Navigation("TreatmentProcess");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InseminationResult", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("InseminationResults")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.InseminationSchedule", "InseminationSchedule")
                        .WithMany()
                        .HasForeignKey("InseminationScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("InseminationSchedule");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.InseminationSchedule", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("InseminationSchedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", "TreatmentProcess")
                        .WithMany()
                        .HasForeignKey("TreatmentProcessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", null)
                        .WithMany("InseminationSchedules")
                        .HasForeignKey("TreatmentProcessId1");

                    b.Navigation("Doctor");

                    b.Navigation("TreatmentProcess");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.LabTestResult", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("LabTestResults")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.LabTestSchedule", "LabTestSchedule")
                        .WithMany()
                        .HasForeignKey("LabTestScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("LabTestSchedule");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.LabTestSchedule", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("LabTestSchedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", "TreatmentProcess")
                        .WithMany()
                        .HasForeignKey("TreatmentProcessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentProcess", null)
                        .WithMany("LabTestSchedules")
                        .HasForeignKey("TreatmentProcessId1");

                    b.Navigation("Doctor");

                    b.Navigation("TreatmentProcess");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.MarriageCertificate", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Notification", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Patient", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("FertilityClinic.DAL.Models.Patient", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Review", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("Reviews")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.Patient", "Patient")
                        .WithMany("Reviews")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.TreatmentProcess", b =>
                {
                    b.HasOne("FertilityClinic.DAL.Models.Doctor", "Doctor")
                        .WithMany("TreatmentProcesses")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.MarriageCertificate", null)
                        .WithMany("TreatmentProcesses")
                        .HasForeignKey("MarriageCertificateCertificateId");

                    b.HasOne("FertilityClinic.DAL.Models.Patient", "Patient")
                        .WithMany("TreatmentProcesses")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FertilityClinic.DAL.Models.TreatmentMethod", "TreatmentMethod")
                        .WithMany("TreatmentProcesses")
                        .HasForeignKey("TreatmentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("TreatmentMethod");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("InjectionSchedules");

                    b.Navigation("InseminationResults");

                    b.Navigation("InseminationSchedules");

                    b.Navigation("LabTestResults");

                    b.Navigation("LabTestSchedules");

                    b.Navigation("Reviews");

                    b.Navigation("TreatmentProcesses");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.MarriageCertificate", b =>
                {
                    b.Navigation("TreatmentProcesses");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Reviews");

                    b.Navigation("TreatmentProcesses");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.TreatmentMethod", b =>
                {
                    b.Navigation("TreatmentProcesses");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.TreatmentProcess", b =>
                {
                    b.Navigation("InjectionSchedules");

                    b.Navigation("InseminationSchedules");

                    b.Navigation("LabTestSchedules");
                });

            modelBuilder.Entity("FertilityClinic.DAL.Models.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Doctor")
                        .IsRequired();

                    b.Navigation("Notifications");

                    b.Navigation("Patient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

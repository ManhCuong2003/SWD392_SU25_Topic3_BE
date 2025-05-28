using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Patients_PatientId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses");

            migrationBuilder.DropTable(
                name: "MarriageCertificates");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TreatmentMethods",
                keyColumn: "TreatmentMethodId",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "MarriageCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "TreatmentMethods");

            migrationBuilder.DropColumn(
                name: "RequiresMarriageCertificate",
                table: "TreatmentMethods");

            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "TreatmentMethods");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TreatmentMethods");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "TreatmentProcesses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_PatientId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_UserId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PatientId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Appointments",
                newName: "PartnerName");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AppointmentTime",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "PartnerDOB",
                table: "Appointments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "AppointmentHistories",
                columns: table => new
                {
                    AppointmentHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerPatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDOBDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentHistories", x => x.AppointmentHistoryId);
                    table.ForeignKey(
                        name: "FK_AppointmentHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    FloorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.FloorId);
                    table.ForeignKey(
                        name: "FK_Floors_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "FloorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentHistories_UserId",
                table: "AppointmentHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_SectionId",
                table: "Floors",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Rooms_RoomId",
                table: "Doctors",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Users_UserId",
                table: "TreatmentProcesses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Rooms_RoomId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Users_UserId",
                table: "TreatmentProcesses");

            migrationBuilder.DropTable(
                name: "AppointmentHistories");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PartnerDOB",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TreatmentProcesses",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_UserId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_PatientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Review",
                newName: "IX_Review_PatientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PartnerName",
                table: "Appointments",
                newName: "Reason");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TreatmentProcesses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarriageCertificateId",
                table: "TreatmentProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TreatmentProcesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TreatmentProcesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "TreatmentMethods",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresMarriageCertificate",
                table: "TreatmentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SuccessRate",
                table: "TreatmentMethods",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TreatmentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalRecordCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MarriageCertificates",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DocumentUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpouseIdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpouseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageCertificates", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TreatmentMethods",
                columns: new[] { "TreatmentMethodId", "AverageDuration", "Category", "CreatedAt", "Description", "IsActive", "MethodCode", "MethodName", "RequiresMarriageCertificate", "SuccessRate", "TechnicalRequirements", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 30, "Basic", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2069), "Bơm tinh trùng đã qua lọc rửa trực tiếp vào buồng tử cung.", true, "IUI", "Bơm tinh trùng vào buồng tử cung", true, 15.00m, "Tinh trùng đạt chất lượng, buồng tử cung bình thường", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2070) },
                    { 2, 60, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2073), "Thụ tinh trứng và tinh trùng trong môi trường ống nghiệm.", true, "IVF", "Thụ tinh trong ống nghiệm", true, 40.00m, "Phòng lab IVF đạt chuẩn", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2074) },
                    { 3, 60, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2076), "Tiêm trực tiếp một tinh trùng vào trứng để tăng khả năng thụ tinh.", true, "ICSI", "Tiêm tinh trùng vào bào tương noãn", true, 50.00m, "Máy vi thao tác, chuyên viên có tay nghề cao", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2076) },
                    { 4, 3, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2078), "Tác động lên vỏ phôi để hỗ trợ thoát màng và tăng tỉ lệ làm tổ.", true, "AH", "Hỗ trợ phôi thoát màng", true, 30.00m, "Laser hỗ trợ phôi thoát màng", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2079) },
                    { 5, 1, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2082), "Sử dụng dung dịch keo dính hỗ trợ phôi bám vào niêm mạc tử cung.", true, "EmbryoGlue", "Kỹ thuật keo dính phôi", true, 20.00m, "Dung dịch hỗ trợ gắn phôi", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2082) },
                    { 6, 10, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2084), "Nuôi trưởng thành trứng non bên ngoài cơ thể trước khi thụ tinh.", true, "IVM", "Trưởng thành trứng non", true, 35.00m, "Môi trường nuôi trứng non", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2085) },
                    { 7, 1, "Advanced", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2087), "Lấy một số tế bào từ phôi để xét nghiệm di truyền.", true, "Biopsy", "Kỹ thuật sinh thiết phôi", true, 99.00m, "Thiết bị sinh thiết, chuyên viên tay nghề cao", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2087) },
                    { 8, 1, "Basic", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2089), "Chọc hút và thu thập trứng từ buồng trứng.", true, "EggRetrieval", "Kỹ thuật gom trứng", true, 100.00m, "Thiết bị siêu âm và hút trứng", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2090) },
                    { 9, 30, "Preservation", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2092), "Bảo quản trứng ở nhiệt độ cực thấp để sử dụng sau.", true, "EggFreezing", "Trữ đông noãn", false, 90.00m, "Kỹ thuật trữ đông tiên tiến", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2092) },
                    { 10, 30, "Preservation", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2094), "Trữ đông phôi để sử dụng trong chu kỳ điều trị sau.", true, "EmbryoFreezing", "Trữ đông phôi", true, 85.00m, "Kỹ thuật trữ đông phôi", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2095) },
                    { 11, 1, "Preservation", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2097), "Trữ đông tinh trùng để sử dụng trong tương lai.", true, "SpermFreezing", "Trữ đông tinh trùng", false, 90.00m, "Nitơ lỏng bảo quản mẫu tinh trùng", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2097) },
                    { 12, 7, "Surgical", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2099), "Nội soi buồng tử cung để chẩn đoán và điều trị vô sinh.", true, "HSC", "Phẫu thuật nội soi buồng tử cung trong vô sinh", true, 60.00m, "Phòng mổ nội soi, thiết bị nội soi buồng tử cung", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2100) },
                    { 13, 1, "Surgical", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2102), "Phẫu thuật lấy tinh trùng trực tiếp từ mào tinh hoặc tinh hoàn.", true, "PESA_TESA", "Lấy tinh trùng từ mào tinh và tinh hoàn", true, 70.00m, "Phẫu thuật viên có chuyên môn, vô trùng tuyệt đối", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2102) },
                    { 14, 1, "Basic", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2106), "Lọc rửa tinh trùng để tăng chất lượng trước IUI hoặc IVF.", true, "SpermWash", "Lọc rửa tinh trùng", true, 100.00m, "Thiết bị ly tâm, kỹ thuật viên chuyên môn", new DateTime(2025, 5, 24, 6, 47, 35, 699, DateTimeKind.Utc).AddTicks(2106) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateId",
                table: "TreatmentProcesses",
                column: "MarriageCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_CertificateNumber",
                table: "MarriageCertificates",
                column: "CertificateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_PatientId",
                table: "MarriageCertificates",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Patients_PatientId",
                table: "Review",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateId",
                table: "TreatmentProcesses",
                column: "MarriageCertificateId",
                principalTable: "MarriageCertificates",
                principalColumn: "CertificateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

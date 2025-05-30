using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Patients_PatientId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameColumn(
                name: "MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                newName: "MarriageCertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_MarriageCertificateId");

            migrationBuilder.RenameColumn(
                name: "DoctorCode",
                table: "Doctors",
                newName: "Education");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PatientId",
                table: "Review",
                newName: "IX_Review_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_DoctorId",
                table: "Review",
                newName: "IX_Review_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notification",
                newName: "IX_Notification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blog",
                newName: "IX_Blog_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TreatmentProcesses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MethodName",
                table: "TreatmentMethods",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MethodCode",
                table: "TreatmentMethods",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AverageDuration",
                table: "TreatmentMethods",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "TechnicalRequirements",
                table: "TreatmentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseName",
                table: "MarriageCertificates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseIdNumber",
                table: "MarriageCertificates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "MarriageCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "MarriageCertificates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "MarriageCertificates",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "MarriageCertificates",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CertificateNumber",
                table: "MarriageCertificates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "BlogId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Users_UserId",
                table: "Blog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Doctors_DoctorId",
                table: "Review",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Patients_PatientId",
                table: "Review",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Users_UserId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Doctors_DoctorId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Patients_PatientId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

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
                name: "AverageDuration",
                table: "TreatmentMethods");

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
                name: "TechnicalRequirements",
                table: "TreatmentMethods");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameColumn(
                name: "MarriageCertificateId",
                table: "TreatmentProcesses",
                newName: "MarriageCertificateCertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_MarriageCertificateCertificateId");

            migrationBuilder.RenameColumn(
                name: "Education",
                table: "Doctors",
                newName: "DoctorCode");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PatientId",
                table: "Reviews",
                newName: "IX_Reviews_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_DoctorId",
                table: "Reviews",
                newName: "IX_Reviews_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UserId",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TreatmentProcesses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MethodName",
                table: "TreatmentMethods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MethodCode",
                table: "TreatmentMethods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SpouseName",
                table: "MarriageCertificates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SpouseIdNumber",
                table: "MarriageCertificates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "MarriageCertificates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "MarriageCertificates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "IssueDate",
                table: "MarriageCertificates",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "MarriageCertificates",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CertificateNumber",
                table: "MarriageCertificates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Doctors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "BlogId");

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    PriceListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.PriceListId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Patients_PatientId",
                table: "Reviews",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                column: "MarriageCertificateCertificateId",
                principalTable: "MarriageCertificates",
                principalColumn: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }
    }
}

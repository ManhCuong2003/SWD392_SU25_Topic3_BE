using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_status_for_doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedAt", "DateOfBirth", "Email", "FullName", "Gender", "HealthInsuranceExpirationDate", "HealthInsuranceId", "IsMarried", "NationalId", "PartnerId", "Password", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Admin Street", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 1, 1), "admin@email.com", "Admin", "Male", null, null, null, "ADMIN123", null, "123", "1234567890", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "456 Doctor Avenue", new DateTime(2025, 6, 28, 19, 10, 11, 118, DateTimeKind.Local).AddTicks(8413), new DateOnly(1975, 5, 15), "doctor@email.com", "Dr. John Doe", "Male", null, null, null, "DOC123456", null, "123", "0987654321", "Doctor", new DateTime(2025, 6, 28, 19, 10, 11, 118, DateTimeKind.Local).AddTicks(8425) }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Avatar", "Bio", "Degree", "Education", "Experience", "ExperienceYears", "Specialization", "Status", "UserId" },
                values: new object[] { 1, "https://www.future-doctor.de/wp-content/uploads/2024/11/shutterstock_2173377961-1000x667.jpg", "Thạc sĩ, Bác sĩ Chuyên khoa II Nguyễn Thị Thanh có hơn 15 năm kinh nghiệm trong lĩnh vực Sản phụ khoa. Bác sĩ đã từng công tác tại Bệnh viện Phụ sản Trung ương và hiện là trưởng khoa Sản tại Bệnh viện Đa khoa Quốc tế.", "Tiến sĩ, Bác sĩ Chuyên khoa II", "[\"2000 - 2004: \\u0110\\u1EA1i h\\u1ECDc Y H\\u00E0 N\\u1ED9i\",\"2006 - 2008: Th\\u1EA1c s\\u0129 Y h\\u1ECDc, \\u0110\\u1EA1i h\\u1ECDc Y H\\u00E0 N\\u1ED9i\",\"2015 - 2017: B\\u00E1c s\\u0129 Chuy\\u00EAn khoa II, \\u0110\\u1EA1i h\\u1ECDc Y D\\u01B0\\u1EE3c TP.HCM\"]", "[\"2010 - Nay: Tr\\u01B0\\u1EDFng khoa S\\u1EA3n, B\\u1EC7nh vi\\u1EC7n Medico\",\"2005 - 2010: B\\u00E1c s\\u0129 S\\u1EA3n khoa, B\\u1EC7nh vi\\u1EC7n Ph\\u1EE5 s\\u1EA3n Trung \\u01B0\\u01A1ng\"]", 15, "Sản khoa", true, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Doctors");
        }
    }
}

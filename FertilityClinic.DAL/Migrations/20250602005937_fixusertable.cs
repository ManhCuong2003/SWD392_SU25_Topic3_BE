using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Doctors_DoctorId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_DoctorId",
                table: "Reviews",
                newName: "IX_Reviews_DoctorId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "HealthInsuranceExpirationDate",
                table: "Users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthInsuranceId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMarried",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthInsuranceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthInsuranceExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                    table.ForeignKey(
                        name: "FK_Partners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partners_UserId",
                table: "Partners",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "HealthInsuranceExpirationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HealthInsuranceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMarried",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_DoctorId",
                table: "Review",
                newName: "IX_Review_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Doctors_DoctorId",
                table: "Review",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}

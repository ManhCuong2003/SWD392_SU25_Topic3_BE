using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateLabTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "ResultDate",
                table: "LabTestResults");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "LabTestResults",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ResultDetails",
                table: "LabTestResults",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "LabTestResults",
                newName: "Result");

            migrationBuilder.AddColumn<bool>(
                name: "Bold",
                table: "LabTestResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LabTestResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Normal",
                table: "LabTestResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bold",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "Normal",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "LabTestResults",
                newName: "ResultDetails");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "LabTestResults",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "LabTestResults",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LabTestResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResultDate",
                table: "LabTestResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixappointmenthistorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "AppointmentHistories");

            migrationBuilder.DropColumn(
                name: "PatientDOBDate",
                table: "AppointmentHistories");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "AppointmentHistories");

            migrationBuilder.RenameColumn(
                name: "Section",
                table: "AppointmentHistories",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PartnerPatientName",
                table: "AppointmentHistories",
                newName: "PartnerName");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PatientDOB",
                table: "AppointmentHistories",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppointmentDate",
                table: "AppointmentHistories",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PartnerDOB",
                table: "AppointmentHistories",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerDOB",
                table: "AppointmentHistories");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AppointmentHistories",
                newName: "Section");

            migrationBuilder.RenameColumn(
                name: "PartnerName",
                table: "AppointmentHistories",
                newName: "PartnerPatientName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PatientDOB",
                table: "AppointmentHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "AppointmentHistories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "AppointmentHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientDOBDate",
                table: "AppointmentHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "AppointmentHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

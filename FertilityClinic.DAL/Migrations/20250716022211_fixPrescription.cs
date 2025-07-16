using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_Doctors_DoctorId",
                table: "LabTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "LabTestResults",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResults_DoctorId",
                table: "LabTestResults",
                newName: "IX_LabTestResults_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_Doctors_UserId",
                table: "LabTestResults",
                column: "UserId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_Doctors_UserId",
                table: "LabTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LabTestResults",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResults_UserId",
                table: "LabTestResults",
                newName: "IX_LabTestResults_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_Doctors_DoctorId",
                table: "LabTestResults",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "LabTestSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "LabTestSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LabTestSchedules_TreatmentProcessId1",
                table: "LabTestSchedules");

            migrationBuilder.DropColumn(
                name: "TreatmentProcessId1",
                table: "LabTestSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "LabTestSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "LabTestSchedules");

            migrationBuilder.AddColumn<int>(
                name: "TreatmentProcessId1",
                table: "LabTestSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTestSchedules_TreatmentProcessId1",
                table: "LabTestSchedules",
                column: "TreatmentProcessId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "LabTestSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "LabTestSchedules",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");
        }
    }
}

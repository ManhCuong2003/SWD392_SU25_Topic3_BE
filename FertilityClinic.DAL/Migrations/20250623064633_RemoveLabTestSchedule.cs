using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLabTestSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_LabTestSchedules_LabTestScheduleId",
                table: "LabTestResults");

            migrationBuilder.DropTable(
                name: "LabTestSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LabTestResults_LabTestScheduleId",
                table: "LabTestResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTestSchedules",
                columns: table => new
                {
                    LabTestScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TreatmentProcessId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestSchedules", x => x.LabTestScheduleId);
                    table.ForeignKey(
                        name: "FK_LabTestSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                        column: x => x.TreatmentProcessId,
                        principalTable: "TreatmentProcesses",
                        principalColumn: "TreatmentProcessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTestResults_LabTestScheduleId",
                table: "LabTestResults",
                column: "LabTestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestSchedules_DoctorId",
                table: "LabTestSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestSchedules_TreatmentProcessId",
                table: "LabTestSchedules",
                column: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_LabTestSchedules_LabTestScheduleId",
                table: "LabTestResults",
                column: "LabTestScheduleId",
                principalTable: "LabTestSchedules",
                principalColumn: "LabTestScheduleId");
        }
    }
}

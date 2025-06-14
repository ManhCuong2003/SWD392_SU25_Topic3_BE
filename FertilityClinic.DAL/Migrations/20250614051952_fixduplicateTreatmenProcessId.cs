using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixduplicateTreatmenProcessId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InseminationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InseminationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_InseminationSchedules_TreatmentProcessId1",
                table: "InseminationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_InjectionSchedules_TreatmentProcessId1",
                table: "InjectionSchedules");

            migrationBuilder.DropColumn(
                name: "TreatmentProcessId1",
                table: "InseminationSchedules");

            migrationBuilder.DropColumn(
                name: "TreatmentProcessId1",
                table: "InjectionSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InjectionSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InseminationSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InseminationSchedules");

            migrationBuilder.AddColumn<int>(
                name: "TreatmentProcessId1",
                table: "InseminationSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentProcessId1",
                table: "InjectionSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InseminationSchedules_TreatmentProcessId1",
                table: "InseminationSchedules",
                column: "TreatmentProcessId1");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionSchedules_TreatmentProcessId1",
                table: "InjectionSchedules",
                column: "TreatmentProcessId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InjectionSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InjectionSchedules",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InseminationSchedules",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InseminationSchedules",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcesses",
                principalColumn: "TreatmentProcessId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addRelationshipLabTestandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_Doctors_UserId",
                table: "LabTestResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_Users_UserId",
                table: "LabTestResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_Users_UserId",
                table: "LabTestResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_Doctors_UserId",
                table: "LabTestResults",
                column: "UserId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");
        }
    }
}

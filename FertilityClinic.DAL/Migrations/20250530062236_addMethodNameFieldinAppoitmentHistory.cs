using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addMethodNameFieldinAppoitmentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TreatmentMethodId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "AppointmentHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentMethodId",
                table: "Appointments",
                column: "TreatmentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TreatmentMethods_TreatmentMethodId",
                table: "Appointments",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TreatmentMethods_TreatmentMethodId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TreatmentMethodId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TreatmentMethodId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "AppointmentHistories");
        }
    }
}

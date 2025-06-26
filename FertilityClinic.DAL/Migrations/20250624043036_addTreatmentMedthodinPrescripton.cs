using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTreatmentMedthodinPrescripton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TreatmentMethodId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_TreatmentMethodId",
                table: "Prescriptions",
                column: "TreatmentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_TreatmentMethods_TreatmentMethodId",
                table: "Prescriptions",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_TreatmentMethods_TreatmentMethodId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_TreatmentMethodId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "TreatmentMethodId",
                table: "Prescriptions");
        }
    }
}

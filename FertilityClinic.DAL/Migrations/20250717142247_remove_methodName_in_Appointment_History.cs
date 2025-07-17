using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class remove_methodName_in_Appointment_History : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "AppointmentHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "AppointmentHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

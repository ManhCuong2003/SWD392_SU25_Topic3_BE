using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class change_certification_to_experience_in_doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Certifications",
                table: "Doctors",
                newName: "Experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Doctors",
                newName: "Certifications");
        }
    }
}

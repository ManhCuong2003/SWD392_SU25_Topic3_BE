using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MarriageCertificateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MarriageCertificates",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpouseIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageCertificates", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                column: "MarriageCertificateCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_CertificateNumber",
                table: "MarriageCertificates",
                column: "CertificateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_PatientId",
                table: "MarriageCertificates",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateCertificateId",
                table: "TreatmentProcesses",
                column: "MarriageCertificateCertificateId",
                principalTable: "MarriageCertificates",
                principalColumn: "CertificateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_MarriageCertificates_MarriageCertificateCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropTable(
                name: "MarriageCertificates");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentProcesses_MarriageCertificateCertificateId",
                table: "TreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "MarriageCertificateCertificateId",
                table: "TreatmentProcesses");
        }
    }
}

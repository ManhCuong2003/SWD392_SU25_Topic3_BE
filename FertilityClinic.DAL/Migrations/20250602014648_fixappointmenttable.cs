using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixappointmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerDOB",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PartnerName",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PartnerId",
                table: "Appointments",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Partners_PartnerId",
                table: "Appointments",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Partners_PartnerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PartnerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Appointments");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PartnerDOB",
                table: "Appointments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "PartnerName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

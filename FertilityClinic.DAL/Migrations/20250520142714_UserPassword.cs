using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityClinic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedule_Doctors_DoctorId",
                table: "InjectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedule_TreatmentProcess_TreatmentProcessId",
                table: "InjectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "InjectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationResult_Doctors_DoctorId",
                table: "InseminationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationResult_InseminationSchedule_InseminationScheduleId",
                table: "InseminationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedule_Doctors_DoctorId",
                table: "InseminationSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedule_TreatmentProcess_TreatmentProcessId",
                table: "InseminationSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "InseminationSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResult_Doctors_DoctorId",
                table: "LabTestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResult_LabTestSchedule_LabTestScheduleId",
                table: "LabTestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedule_Doctors_DoctorId",
                table: "LabTestSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedule_TreatmentProcess_TreatmentProcessId",
                table: "LabTestSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "LabTestSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcess_Doctors_DoctorId",
                table: "TreatmentProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcess_Patients_PatientId",
                table: "TreatmentProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcess_TreatmentMethod_TreatmentMethodId",
                table: "TreatmentProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentProcess",
                table: "TreatmentProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethod",
                table: "TreatmentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestSchedule",
                table: "LabTestSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestResult",
                table: "LabTestResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InseminationSchedule",
                table: "InseminationSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InseminationResult",
                table: "InseminationResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InjectionSchedule",
                table: "InjectionSchedule");

            migrationBuilder.RenameTable(
                name: "TreatmentProcess",
                newName: "TreatmentProcesses");

            migrationBuilder.RenameTable(
                name: "TreatmentMethod",
                newName: "TreatmentMethods");

            migrationBuilder.RenameTable(
                name: "LabTestSchedule",
                newName: "LabTestSchedules");

            migrationBuilder.RenameTable(
                name: "LabTestResult",
                newName: "LabTestResults");

            migrationBuilder.RenameTable(
                name: "InseminationSchedule",
                newName: "InseminationSchedules");

            migrationBuilder.RenameTable(
                name: "InseminationResult",
                newName: "InseminationResults");

            migrationBuilder.RenameTable(
                name: "InjectionSchedule",
                newName: "InjectionSchedules");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcess_TreatmentMethodId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_TreatmentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcess_PatientId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcess_DoctorId",
                table: "TreatmentProcesses",
                newName: "IX_TreatmentProcesses_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedule_TreatmentProcessId1",
                table: "LabTestSchedules",
                newName: "IX_LabTestSchedules_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedule_TreatmentProcessId",
                table: "LabTestSchedules",
                newName: "IX_LabTestSchedules_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedule_DoctorId",
                table: "LabTestSchedules",
                newName: "IX_LabTestSchedules_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResult_LabTestScheduleId",
                table: "LabTestResults",
                newName: "IX_LabTestResults_LabTestScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResult_DoctorId",
                table: "LabTestResults",
                newName: "IX_LabTestResults_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedule_TreatmentProcessId1",
                table: "InseminationSchedules",
                newName: "IX_InseminationSchedules_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedule_TreatmentProcessId",
                table: "InseminationSchedules",
                newName: "IX_InseminationSchedules_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedule_DoctorId",
                table: "InseminationSchedules",
                newName: "IX_InseminationSchedules_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationResult_InseminationScheduleId",
                table: "InseminationResults",
                newName: "IX_InseminationResults_InseminationScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationResult_DoctorId",
                table: "InseminationResults",
                newName: "IX_InseminationResults_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedule_TreatmentProcessId1",
                table: "InjectionSchedules",
                newName: "IX_InjectionSchedules_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedule_TreatmentProcessId",
                table: "InjectionSchedules",
                newName: "IX_InjectionSchedules_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedule_DoctorId",
                table: "InjectionSchedules",
                newName: "IX_InjectionSchedules_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentProcesses",
                table: "TreatmentProcesses",
                column: "TreatmentProcessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethods",
                table: "TreatmentMethods",
                column: "TreatmentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestSchedules",
                table: "LabTestSchedules",
                column: "LabTestScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestResults",
                table: "LabTestResults",
                column: "LabTestResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InseminationSchedules",
                table: "InseminationSchedules",
                column: "InseminationScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InseminationResults",
                table: "InseminationResults",
                column: "InseminationResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InjectionSchedules",
                table: "InjectionSchedules",
                column: "InjectionScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedules_Doctors_DoctorId",
                table: "InjectionSchedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

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
                name: "FK_InseminationResults_Doctors_DoctorId",
                table: "InseminationResults",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationResults_InseminationSchedules_InseminationScheduleId",
                table: "InseminationResults",
                column: "InseminationScheduleId",
                principalTable: "InseminationSchedules",
                principalColumn: "InseminationScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedules_Doctors_DoctorId",
                table: "InseminationSchedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_Doctors_DoctorId",
                table: "LabTestResults",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResults_LabTestSchedules_LabTestScheduleId",
                table: "LabTestResults",
                column: "LabTestScheduleId",
                principalTable: "LabTestSchedules",
                principalColumn: "LabTestScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedules_Doctors_DoctorId",
                table: "LabTestSchedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_Doctors_DoctorId",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InjectionSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InjectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationResults_Doctors_DoctorId",
                table: "InseminationResults");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationResults_InseminationSchedules_InseminationScheduleId",
                table: "InseminationResults");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_Doctors_DoctorId",
                table: "InseminationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "InseminationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_InseminationSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "InseminationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_Doctors_DoctorId",
                table: "LabTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestResults_LabTestSchedules_LabTestScheduleId",
                table: "LabTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_Doctors_DoctorId",
                table: "LabTestSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId",
                table: "LabTestSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTestSchedules_TreatmentProcesses_TreatmentProcessId1",
                table: "LabTestSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Doctors_DoctorId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_Patients_PatientId",
                table: "TreatmentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentProcesses_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentProcesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentProcesses",
                table: "TreatmentProcesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethods",
                table: "TreatmentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestSchedules",
                table: "LabTestSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestResults",
                table: "LabTestResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InseminationSchedules",
                table: "InseminationSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InseminationResults",
                table: "InseminationResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InjectionSchedules",
                table: "InjectionSchedules");

            migrationBuilder.RenameTable(
                name: "TreatmentProcesses",
                newName: "TreatmentProcess");

            migrationBuilder.RenameTable(
                name: "TreatmentMethods",
                newName: "TreatmentMethod");

            migrationBuilder.RenameTable(
                name: "LabTestSchedules",
                newName: "LabTestSchedule");

            migrationBuilder.RenameTable(
                name: "LabTestResults",
                newName: "LabTestResult");

            migrationBuilder.RenameTable(
                name: "InseminationSchedules",
                newName: "InseminationSchedule");

            migrationBuilder.RenameTable(
                name: "InseminationResults",
                newName: "InseminationResult");

            migrationBuilder.RenameTable(
                name: "InjectionSchedules",
                newName: "InjectionSchedule");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_TreatmentMethodId",
                table: "TreatmentProcess",
                newName: "IX_TreatmentProcess_TreatmentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_PatientId",
                table: "TreatmentProcess",
                newName: "IX_TreatmentProcess_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentProcesses_DoctorId",
                table: "TreatmentProcess",
                newName: "IX_TreatmentProcess_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedules_TreatmentProcessId1",
                table: "LabTestSchedule",
                newName: "IX_LabTestSchedule_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedules_TreatmentProcessId",
                table: "LabTestSchedule",
                newName: "IX_LabTestSchedule_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestSchedules_DoctorId",
                table: "LabTestSchedule",
                newName: "IX_LabTestSchedule_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResults_LabTestScheduleId",
                table: "LabTestResult",
                newName: "IX_LabTestResult_LabTestScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_LabTestResults_DoctorId",
                table: "LabTestResult",
                newName: "IX_LabTestResult_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedules_TreatmentProcessId1",
                table: "InseminationSchedule",
                newName: "IX_InseminationSchedule_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedules_TreatmentProcessId",
                table: "InseminationSchedule",
                newName: "IX_InseminationSchedule_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationSchedules_DoctorId",
                table: "InseminationSchedule",
                newName: "IX_InseminationSchedule_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationResults_InseminationScheduleId",
                table: "InseminationResult",
                newName: "IX_InseminationResult_InseminationScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_InseminationResults_DoctorId",
                table: "InseminationResult",
                newName: "IX_InseminationResult_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedules_TreatmentProcessId1",
                table: "InjectionSchedule",
                newName: "IX_InjectionSchedule_TreatmentProcessId1");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedules_TreatmentProcessId",
                table: "InjectionSchedule",
                newName: "IX_InjectionSchedule_TreatmentProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_InjectionSchedules_DoctorId",
                table: "InjectionSchedule",
                newName: "IX_InjectionSchedule_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentProcess",
                table: "TreatmentProcess",
                column: "TreatmentProcessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethod",
                table: "TreatmentMethod",
                column: "TreatmentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestSchedule",
                table: "LabTestSchedule",
                column: "LabTestScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestResult",
                table: "LabTestResult",
                column: "LabTestResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InseminationSchedule",
                table: "InseminationSchedule",
                column: "InseminationScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InseminationResult",
                table: "InseminationResult",
                column: "InseminationResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InjectionSchedule",
                table: "InjectionSchedule",
                column: "InjectionScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedule_Doctors_DoctorId",
                table: "InjectionSchedule",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedule_TreatmentProcess_TreatmentProcessId",
                table: "InjectionSchedule",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InjectionSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "InjectionSchedule",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationResult_Doctors_DoctorId",
                table: "InseminationResult",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationResult_InseminationSchedule_InseminationScheduleId",
                table: "InseminationResult",
                column: "InseminationScheduleId",
                principalTable: "InseminationSchedule",
                principalColumn: "InseminationScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedule_Doctors_DoctorId",
                table: "InseminationSchedule",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedule_TreatmentProcess_TreatmentProcessId",
                table: "InseminationSchedule",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_InseminationSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "InseminationSchedule",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResult_Doctors_DoctorId",
                table: "LabTestResult",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestResult_LabTestSchedule_LabTestScheduleId",
                table: "LabTestResult",
                column: "LabTestScheduleId",
                principalTable: "LabTestSchedule",
                principalColumn: "LabTestScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedule_Doctors_DoctorId",
                table: "LabTestSchedule",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedule_TreatmentProcess_TreatmentProcessId",
                table: "LabTestSchedule",
                column: "TreatmentProcessId",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTestSchedule_TreatmentProcess_TreatmentProcessId1",
                table: "LabTestSchedule",
                column: "TreatmentProcessId1",
                principalTable: "TreatmentProcess",
                principalColumn: "TreatmentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcess_Doctors_DoctorId",
                table: "TreatmentProcess",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcess_Patients_PatientId",
                table: "TreatmentProcess",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentProcess_TreatmentMethod_TreatmentMethodId",
                table: "TreatmentProcess",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethod",
                principalColumn: "TreatmentMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorsAppointment.Migrations
{
    public partial class DoctorPolyclinicSpecializationRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPolyclinic_Doctor_DoctorsId",
                table: "DoctorPolyclinic");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Doctor_DoctorsId",
                table: "DoctorSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Specialization_SpecializationsId",
                table: "DoctorSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialization",
                table: "Specialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "Specialization",
                newName: "Specializations");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPolyclinic_Doctors_DoctorsId",
                table: "DoctorPolyclinic",
                column: "DoctorsId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Doctors_DoctorsId",
                table: "DoctorSpecialization",
                column: "DoctorsId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Specializations_SpecializationsId",
                table: "DoctorSpecialization",
                column: "SpecializationsId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPolyclinic_Doctors_DoctorsId",
                table: "DoctorPolyclinic");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Doctors_DoctorsId",
                table: "DoctorSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Specializations_SpecializationsId",
                table: "DoctorSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Specializations",
                newName: "Specialization");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialization",
                table: "Specialization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPolyclinic_Doctor_DoctorsId",
                table: "DoctorPolyclinic",
                column: "DoctorsId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Doctor_DoctorsId",
                table: "DoctorSpecialization",
                column: "DoctorsId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Specialization_SpecializationsId",
                table: "DoctorSpecialization",
                column: "SpecializationsId",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

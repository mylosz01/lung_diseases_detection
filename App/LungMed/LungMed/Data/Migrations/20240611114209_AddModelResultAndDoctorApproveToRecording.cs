using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LungMed.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModelResultAndDoctorApproveToRecording : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DoctorApprove",
                table: "Recording",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelResult",
                table: "Recording",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Recording",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Patient",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "Patient",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PersonalNumber",
                table: "Patient",
                column: "PersonalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PhoneNumber",
                table: "Patient",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PersonalNumber",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PhoneNumber",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DoctorApprove",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "ModelResult",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Recording");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

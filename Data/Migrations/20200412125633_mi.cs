using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class mi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentProcedureDescription",
                table: "Appointments",
                newName: "ProcedureDescription");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDay",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Appointments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDay",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ProcedureDescription",
                table: "Appointments",
                newName: "AppointmentProcedureDescription");
        }
    }
}

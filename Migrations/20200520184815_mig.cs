using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedureAppointmentCanceled",
                table: "AppointmentProcedures");

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentCanceled",
                table: "Appointments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentCanceled",
                table: "Appointments");

            migrationBuilder.AddColumn<bool>(
                name: "ProcedureAppointmentCanceled",
                table: "AppointmentProcedures",
                nullable: false,
                defaultValue: false);
        }
    }
}

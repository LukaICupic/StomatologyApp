using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class Miga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DentalProcedureCanceled",
                table: "DentalProcedures");

            migrationBuilder.DropColumn(
                name: "AppointmentCanceled",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ProcedureName",
                table: "DentalProcedures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProcedureAppointmentCanceled",
                table: "AppointmentProcedure",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedureAppointmentCanceled",
                table: "AppointmentProcedure");

            migrationBuilder.AlterColumn<string>(
                name: "ProcedureName",
                table: "DentalProcedures",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "DentalProcedureCanceled",
                table: "DentalProcedures",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentCanceled",
                table: "Appointments",
                nullable: false,
                defaultValue: false);
        }
    }
}

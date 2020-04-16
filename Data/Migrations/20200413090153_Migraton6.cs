using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class Migraton6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedure_Appointments_AppointmentId",
                table: "AppointmentProcedure");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedure_DentalProcedures_DentalProcedureId",
                table: "AppointmentProcedure");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProcedure_Customers_CustomerId",
                table: "CustomerProcedure");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProcedure_DentalProcedures_DentalProcedureId",
                table: "CustomerProcedure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProcedure",
                table: "CustomerProcedure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentProcedure",
                table: "AppointmentProcedure");

            migrationBuilder.RenameTable(
                name: "CustomerProcedure",
                newName: "CustomerProcedures");

            migrationBuilder.RenameTable(
                name: "AppointmentProcedure",
                newName: "AppointmentProcedures");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProcedure_DentalProcedureId",
                table: "CustomerProcedures",
                newName: "IX_CustomerProcedures_DentalProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentProcedure_DentalProcedureId",
                table: "AppointmentProcedures",
                newName: "IX_AppointmentProcedures_DentalProcedureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProcedures",
                table: "CustomerProcedures",
                columns: new[] { "CustomerId", "DentalProcedureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentProcedures",
                table: "AppointmentProcedures",
                columns: new[] { "AppointmentId", "DentalProcedureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedures_Appointments_AppointmentId",
                table: "AppointmentProcedures",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedures_DentalProcedures_DentalProcedureId",
                table: "AppointmentProcedures",
                column: "DentalProcedureId",
                principalTable: "DentalProcedures",
                principalColumn: "DentalProcedureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProcedures_Customers_CustomerId",
                table: "CustomerProcedures",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProcedures_DentalProcedures_DentalProcedureId",
                table: "CustomerProcedures",
                column: "DentalProcedureId",
                principalTable: "DentalProcedures",
                principalColumn: "DentalProcedureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedures_Appointments_AppointmentId",
                table: "AppointmentProcedures");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedures_DentalProcedures_DentalProcedureId",
                table: "AppointmentProcedures");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProcedures_Customers_CustomerId",
                table: "CustomerProcedures");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProcedures_DentalProcedures_DentalProcedureId",
                table: "CustomerProcedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProcedures",
                table: "CustomerProcedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentProcedures",
                table: "AppointmentProcedures");

            migrationBuilder.RenameTable(
                name: "CustomerProcedures",
                newName: "CustomerProcedure");

            migrationBuilder.RenameTable(
                name: "AppointmentProcedures",
                newName: "AppointmentProcedure");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProcedures_DentalProcedureId",
                table: "CustomerProcedure",
                newName: "IX_CustomerProcedure_DentalProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentProcedures_DentalProcedureId",
                table: "AppointmentProcedure",
                newName: "IX_AppointmentProcedure_DentalProcedureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProcedure",
                table: "CustomerProcedure",
                columns: new[] { "CustomerId", "DentalProcedureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentProcedure",
                table: "AppointmentProcedure",
                columns: new[] { "AppointmentId", "DentalProcedureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedure_Appointments_AppointmentId",
                table: "AppointmentProcedure",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedure_DentalProcedures_DentalProcedureId",
                table: "AppointmentProcedure",
                column: "DentalProcedureId",
                principalTable: "DentalProcedures",
                principalColumn: "DentalProcedureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProcedure_Customers_CustomerId",
                table: "CustomerProcedure",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProcedure_DentalProcedures_DentalProcedureId",
                table: "CustomerProcedure",
                column: "DentalProcedureId",
                principalTable: "DentalProcedures",
                principalColumn: "DentalProcedureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

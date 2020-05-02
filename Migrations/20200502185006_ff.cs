using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "DentalProcedures",
                columns: table => new
                {
                    DentalProcedureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProcedureName = table.Column<string>(nullable: false),
                    ProcedurePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalProcedures", x => x.DentalProcedureId);
                });

            migrationBuilder.CreateTable(
                name: "WorkDays",
                columns: table => new
                {
                    WorkDaysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkWeekStart = table.Column<DateTime>(nullable: false),
                    WorkWeekEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDays", x => x.WorkDaysId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProcedures",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    DentalProcedureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProcedures", x => new { x.CustomerId, x.DentalProcedureId });
                    table.ForeignKey(
                        name: "FK_CustomerProcedures_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProcedures_DentalProcedures_DentalProcedureId",
                        column: x => x.DentalProcedureId,
                        principalTable: "DentalProcedures",
                        principalColumn: "DentalProcedureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppointmentStart = table.Column<DateTime>(nullable: false),
                    AppointmentEnd = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ProcedureDescription = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    WorkDaysId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_WorkDays_WorkDaysId",
                        column: x => x.WorkDaysId,
                        principalTable: "WorkDays",
                        principalColumn: "WorkDaysId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentProcedures",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false),
                    DentalProcedureId = table.Column<int>(nullable: false),
                    ProcedureAppointmentCanceled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentProcedures", x => new { x.AppointmentId, x.DentalProcedureId });
                    table.ForeignKey(
                        name: "FK_AppointmentProcedures_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentProcedures_DentalProcedures_DentalProcedureId",
                        column: x => x.DentalProcedureId,
                        principalTable: "DentalProcedures",
                        principalColumn: "DentalProcedureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "TelephoneNumber" },
                values: new object[] { 1, "Miramarska 22", "Luka", "0993132245" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "TelephoneNumber" },
                values: new object[] { 2, "Miramarska 23", "Ana", "0993132246" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "TelephoneNumber" },
                values: new object[] { 3, "Miramarska 24", "Vilihrast", "0993131245" });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentProcedures_DentalProcedureId",
                table: "AppointmentProcedures",
                column: "DentalProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_WorkDaysId",
                table: "Appointments",
                column: "WorkDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProcedures_DentalProcedureId",
                table: "CustomerProcedures",
                column: "DentalProcedureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentProcedures");

            migrationBuilder.DropTable(
                name: "CustomerProcedures");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DentalProcedures");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "WorkDays");
        }
    }
}

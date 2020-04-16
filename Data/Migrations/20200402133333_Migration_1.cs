using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class Migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true)
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
                    ProcedureName = table.Column<string>(nullable: true),
                    ProcedurePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DentalProcedureCanceled = table.Column<bool>(nullable: false)
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
                    WorkStart = table.Column<DateTime>(nullable: false),
                    WorkEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDays", x => x.WorkDaysId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProcedure",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    DentalProcedureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProcedure", x => new { x.CustomerId, x.DentalProcedureId });
                    table.ForeignKey(
                        name: "FK_CustomerProcedure_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProcedure_DentalProcedures_DentalProcedureId",
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
                    AppointmentCanceled = table.Column<bool>(nullable: false),
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
                name: "AppointmentProcedure",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false),
                    DentalProcedureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentProcedure", x => new { x.AppointmentId, x.DentalProcedureId });
                    table.ForeignKey(
                        name: "FK_AppointmentProcedure_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentProcedure_DentalProcedures_DentalProcedureId",
                        column: x => x.DentalProcedureId,
                        principalTable: "DentalProcedures",
                        principalColumn: "DentalProcedureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentProcedure_DentalProcedureId",
                table: "AppointmentProcedure",
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
                name: "IX_CustomerProcedure_DentalProcedureId",
                table: "CustomerProcedure",
                column: "DentalProcedureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentProcedure");

            migrationBuilder.DropTable(
                name: "CustomerProcedure");

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

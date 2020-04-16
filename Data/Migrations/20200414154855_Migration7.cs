using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "WorkWeekEnd",
                table: "WorkDays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkWeekStart",
                table: "WorkDays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkWeekEnd",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "WorkWeekStart",
                table: "WorkDays");
        }
    }
}

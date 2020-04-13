using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatologyApp.Migrations
{
    public partial class Pra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "FirstName", "LastName", "TelephoneNumber" },
                values: new object[] { 1, "Miramarska 22", "Luka", "Marinković", "0993132245" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "FirstName", "LastName", "TelephoneNumber" },
                values: new object[] { 2, "Miramarska 23", "Ana", "Anić", "0993132246" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "FirstName", "LastName", "TelephoneNumber" },
                values: new object[] { 3, "Miramarska 24", "Vilihrast", "Kćerčić", "0993131245" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);
        }
    }
}

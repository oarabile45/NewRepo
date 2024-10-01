using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "RoleName",
                value: "Fault Technician");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 3, "Maintenance Technician" },
                    { 4, "Stock Controller" },
                    { 5, "Customer Service" },
                    { 6, "Purchasing Manager" },
                    { 7, "Administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "RoleName",
                value: "Employee");
        }
    }
}

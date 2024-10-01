using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedDummyEmailOnSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "suppliers",
                keyColumn: "SupplierId",
                keyValue: 456,
                column: "Email",
                value: "ayakhac16@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "suppliers",
                keyColumn: "SupplierId",
                keyValue: 456,
                column: "Email",
                value: "info@jusfridge.co.za");
        }
    }
}

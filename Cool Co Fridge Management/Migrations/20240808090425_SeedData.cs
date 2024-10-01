using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 1,
                column: "Availablity",
                value: true);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 2,
                column: "Availablity",
                value: true);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 3,
                column: "Availablity",
                value: true);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 4,
                column: "Availablity",
                value: true);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 5,
                column: "Availablity",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 1,
                column: "Availablity",
                value: false);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 2,
                column: "Availablity",
                value: false);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 3,
                column: "Availablity",
                value: false);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 4,
                column: "Availablity",
                value: false);

            migrationBuilder.UpdateData(
                table: "fridge_type",
                keyColumn: "FridgeTypeID",
                keyValue: 5,
                column: "Availablity",
                value: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_stock_FridgeTypeID",
                table: "stock",
                column: "FridgeTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_stock_fridge_type_FridgeTypeID",
                table: "stock",
                column: "FridgeTypeID",
                principalTable: "fridge_type",
                principalColumn: "FridgeTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stock_fridge_type_FridgeTypeID",
                table: "stock");

            migrationBuilder.DropIndex(
                name: "IX_stock_FridgeTypeID",
                table: "stock");
        }
    }
}

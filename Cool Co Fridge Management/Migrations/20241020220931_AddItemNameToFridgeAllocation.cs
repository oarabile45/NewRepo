using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddItemNameToFridgeAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "ItemName",
               table: "FridgeAllocation",
               nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "FridgeAllocation");
        }
    }
}

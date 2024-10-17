using Microsoft.EntityFrameworkCore.Migrations;

namespace Cool_Co_Fridge_Management.Migrations
{
    public partial class AddAllocationDateAndFridgeTypeToFridgeAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AllocationDate",
                table: "FridgeAllocation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FridgeType",
                table: "FridgeAllocation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocationDate",
                table: "FridgeAllocation");

            migrationBuilder.DropColumn(
                name: "FridgeType",
                table: "FridgeAllocation");
        }
    }
}

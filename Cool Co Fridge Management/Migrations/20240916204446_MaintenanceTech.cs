using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceTech : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTechID",
                table: "MaintenanceBookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaintenanceTech",
                columns: table => new
                {
                    MaintenanceTechID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceTech", x => x.MaintenanceTechID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceBookings_MaintenanceTechID",
                table: "MaintenanceBookings",
                column: "MaintenanceTechID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceBookings_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBookings",
                column: "MaintenanceTechID",
                principalTable: "MaintenanceTech",
                principalColumn: "MaintenanceTechID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceBookings_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBookings");

            migrationBuilder.DropTable(
                name: "MaintenanceTech");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceBookings_MaintenanceTechID",
                table: "MaintenanceBookings");

            migrationBuilder.DropColumn(
                name: "MaintenanceTechID",
                table: "MaintenanceBookings");
        }
    }
}

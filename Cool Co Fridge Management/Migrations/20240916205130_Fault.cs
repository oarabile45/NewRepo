using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class Fault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    FaultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    FaultTechId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.FaultId);
                    table.ForeignKey(
                        name: "FK_Faults_FaultTech_FaultTechId",
                        column: x => x.FaultTechId,
                        principalTable: "FaultTech",
                        principalColumn: "FaultTechId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faults_MaintenanceBookings_ID",
                        column: x => x.ID,
                        principalTable: "MaintenanceBookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faults_FaultTechId",
                table: "Faults",
                column: "FaultTechId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_ID",
                table: "Faults",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faults");
        }
    }
}

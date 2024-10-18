using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "Notifications",
           columns: table => new
           {
               NotificationId = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
               Message = table.Column<string>(nullable: false),
               CreatedAt = table.Column<DateTime>(nullable: false),
               IsRead = table.Column<bool>(nullable: false, defaultValue: false),
               FaultTechId = table.Column<int>(nullable: false),
               MaintenanceTechID = table.Column<int>(nullable: false)
           },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                        table.ForeignKey(
                            name: "FK_Notifications_FaultTech_FaultTechId",
                            column: x => x.FaultTechId,
                            principalTable: "FaultTech",
                            principalColumn: "FaultTechId",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Notifications_MaintenanceTech_MaintenanceTechID",
                            column: x => x.MaintenanceTechID,
                            principalTable: "MaintenanceTech",
                            principalColumn: "MaintenanceTechID",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FaultTechId",
                table: "Notifications",
                column: "FaultTechId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MaintenanceTechID",
                table: "Notifications",
                column: "MaintenanceTechID");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.DropTable(
           name: "Notifications");
        }
    }
}

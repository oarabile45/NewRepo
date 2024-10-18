using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "MaintenanceRequests",
            columns: table => new
            {
                BookingID = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ID = table.Column<int>(nullable: true),
                MaintenanceTechID = table.Column<int>(nullable: true),
                FirstName = table.Column<string>(nullable: false),
                LastName = table.Column<string>(nullable: true),
                Address = table.Column<string>(nullable: true),
                RequestedDate = table.Column<DateTime>(nullable: false),
                ApprovedDate = table.Column<DateTime>(nullable: true),
                IsApprovedByTechnician = table.Column<bool>(nullable: false, defaultValue: false),
                UserConfirmationStatus = table.Column<string>(nullable: false, defaultValue: "Pending"),
                status = table.Column<int>(nullable: false),
                FaultDescription = table.Column<string>(nullable: true)
            },
             constraints: table =>
             {
                 table.PrimaryKey("PK_MaintenanceRequests", x => x.BookingID);
                 table.ForeignKey(
                     name: "FK_MaintenanceRequests_Users_ID",
                     column: x => x.ID,
                     principalTable: "Users",
                     principalColumn: "ID",
                     onDelete: ReferentialAction.Restrict);
                 table.ForeignKey(
                     name: "FK_MaintenanceRequests_MaintenanceTech_MaintenanceTechID",
                     column: x => x.MaintenanceTechID,
                     principalTable: "MaintenanceTech",
                     principalColumn: "MaintenanceTechID",
                     onDelete: ReferentialAction.Restrict);
             });
            migrationBuilder.CreateIndex(
           name: "IX_MaintenanceRequests_ID",
           table: "MaintenanceRequests",
           column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_MaintenanceTechID",
                table: "MaintenanceRequests",
                column: "MaintenanceTechID");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "MaintenanceRequests");

        }
    }
}

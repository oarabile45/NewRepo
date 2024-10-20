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
                   FirstName = table.Column<string>(nullable: false),
                   LastName = table.Column<string>(nullable: false),
                   Address = table.Column<string>(nullable: false),
                   RequestedDate = table.Column<DateTime>(nullable: false),
                   ApprovedDate = table.Column<DateTime>(nullable: true),
                   IsApprovedByTechnician = table.Column<bool>(nullable: false, defaultValue: false),
                   UserConfirmationStatus = table.Column<string>(nullable: true, defaultValue: "Pending"),
                   Status = table.Column<int>(nullable: false),
                   FaultDescription = table.Column<string>(nullable: true),
                   UserId = table.Column<int>(nullable: false) // Foreign key to User
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_MaintenanceRequests", x => x.BookingID);
                   table.ForeignKey(
                       name: "FK_MaintenanceRequests_Users_UserId",
                       column: x => x.UserId,
                       principalTable: "Users", // Adjust this if your users table has a different name
                       principalColumn: "Id", // Adjust this if your user ID column has a different name
                       onDelete: ReferentialAction.Cascade);
               });
            migrationBuilder.CreateIndex(
           name: "IX_MaintenanceRequests_UserId",
           table: "MaintenanceRequests",
           column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "MaintenanceRequests");
        }
    }
}

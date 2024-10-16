using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddAllocationDateToFridgeAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_MaintenanceBooking_ID",
                table: "Faults");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceBooking_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceBooking_Users_UserID",
                table: "MaintenanceBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceBooking",
                table: "MaintenanceBooking");

            migrationBuilder.RenameTable(
                name: "MaintenanceBooking",
                newName: "MaintenanceBookings");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceBooking_UserID",
                table: "MaintenanceBookings",
                newName: "IX_MaintenanceBookings_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceBooking_MaintenanceTechID",
                table: "MaintenanceBookings",
                newName: "IX_MaintenanceBookings_MaintenanceTechID");

            migrationBuilder.AddColumn<string>(
                name: "FaultDescription",
                table: "MaintenanceBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByTechnician",
                table: "MaintenanceBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserConfirmationStatus",
                table: "MaintenanceBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceBookings",
                table: "MaintenanceBookings",
                column: "BookingID");

            migrationBuilder.CreateTable(
                name: "FridgeRequests",
                columns: table => new
                {
                    FridgeRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridgeTypeID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeRequests", x => x.FridgeRequestID);
                    table.ForeignKey(
                        name: "FK_FridgeRequests_fridge_type_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "fridge_type",
                        principalColumn: "FridgeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    FaultTechId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceTechID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FridgeAllocation",
                columns: table => new
                {
                    FridgeAllocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    FridgeID = table.Column<int>(type: "int", nullable: false),
                    FridgeType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridgeRequestID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllocationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeAllocation", x => x.FridgeAllocationID);
                    table.ForeignKey(
                        name: "FK_FridgeAllocation_FridgeRequests_FridgeRequestID",
                        column: x => x.FridgeRequestID,
                        principalTable: "FridgeRequests",
                        principalColumn: "FridgeRequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeAllocation_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeAllocation_stock_FridgeID",
                        column: x => x.FridgeID,
                        principalTable: "stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeAllocation_FridgeID",
                table: "FridgeAllocation",
                column: "FridgeID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeAllocation_FridgeRequestID",
                table: "FridgeAllocation",
                column: "FridgeRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeAllocation_Id",
                table: "FridgeAllocation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeRequests_FridgeTypeID",
                table: "FridgeRequests",
                column: "FridgeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FaultTechId",
                table: "Notifications",
                column: "FaultTechId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MaintenanceTechID",
                table: "Notifications",
                column: "MaintenanceTechID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_MaintenanceBookings_ID",
                table: "Faults",
                column: "ID",
                principalTable: "MaintenanceBookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceBookings_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBookings",
                column: "MaintenanceTechID",
                principalTable: "MaintenanceTech",
                principalColumn: "MaintenanceTechID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceBookings_Users_UserID",
                table: "MaintenanceBookings",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_MaintenanceBookings_ID",
                table: "Faults");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceBookings_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceBookings_Users_UserID",
                table: "MaintenanceBookings");

            migrationBuilder.DropTable(
                name: "FridgeAllocation");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "FridgeRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceBookings",
                table: "MaintenanceBookings");

            migrationBuilder.DropColumn(
                name: "FaultDescription",
                table: "MaintenanceBookings");

            migrationBuilder.DropColumn(
                name: "IsApprovedByTechnician",
                table: "MaintenanceBookings");

            migrationBuilder.DropColumn(
                name: "UserConfirmationStatus",
                table: "MaintenanceBookings");

            migrationBuilder.RenameTable(
                name: "MaintenanceBookings",
                newName: "MaintenanceBooking");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceBookings_UserID",
                table: "MaintenanceBooking",
                newName: "IX_MaintenanceBooking_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceBookings_MaintenanceTechID",
                table: "MaintenanceBooking",
                newName: "IX_MaintenanceBooking_MaintenanceTechID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceBooking",
                table: "MaintenanceBooking",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_MaintenanceBooking_ID",
                table: "Faults",
                column: "ID",
                principalTable: "MaintenanceBooking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceBooking_MaintenanceTech_MaintenanceTechID",
                table: "MaintenanceBooking",
                column: "MaintenanceTechID",
                principalTable: "MaintenanceTech",
                principalColumn: "MaintenanceTechID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceBooking_Users_UserID",
                table: "MaintenanceBooking",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

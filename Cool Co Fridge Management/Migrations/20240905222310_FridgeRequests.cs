using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class FridgeRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FridgeAllocations",
                columns: table => new
                {
                    FridgeAllocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    FridgeID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridgeRequestID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeAllocations", x => x.FridgeAllocationID);
                    table.ForeignKey(
                        name: "FK_FridgeAllocations_Users_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeAllocations_stock_FridgeID",
                        column: x => x.FridgeID,
                        principalTable: "stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FridgeRequests",
                columns: table => new
                {
                    FridgeRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FridgeTypeID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeRequests", x => x.FridgeRequestID);
                    table.ForeignKey(
                        name: "FK_FridgeRequests_Users_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeRequests_fridge_type_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "fridge_type",
                        principalColumn: "FridgeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FridgesAvailable",
                columns: table => new
                {
                    FridgeToAllocatedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridgeTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgesAvailable", x => x.FridgeToAllocatedId);
                    table.ForeignKey(
                        name: "FK_FridgesAvailable_fridge_type_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "fridge_type",
                        principalColumn: "FridgeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceBooking",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceBooking", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeAllocations_CustomerID",
                table: "FridgeAllocations",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeAllocations_FridgeID",
                table: "FridgeAllocations",
                column: "FridgeID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeRequests_FridgeTypeID",
                table: "FridgeRequests",
                column: "FridgeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgesAvailable_FridgeTypeID",
                table: "FridgesAvailable",
                column: "FridgeTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeAllocations");

            migrationBuilder.DropTable(
                name: "FridgeRequests");

            migrationBuilder.DropTable(
                name: "FridgesAvailable");

            migrationBuilder.DropTable(
                name: "MaintenanceBooking");
        }
    }
}

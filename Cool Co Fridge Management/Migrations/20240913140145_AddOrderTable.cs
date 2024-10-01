using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "faultTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    FridgeTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_orders_fridge_type_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "fridge_type",
                        principalColumn: "FridgeTypeID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_orders_suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faultTypes_ID",
                table: "faultTypes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_orders_FridgeTypeID",
                table: "orders",
                column: "FridgeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_orders_SupplierId",
                table: "orders",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_faultTypes_Users_ID",
                table: "faultTypes",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_faultTypes_Users_ID",
                table: "faultTypes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropIndex(
                name: "IX_faultTypes_ID",
                table: "faultTypes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "faultTypes");
        }
    }
}

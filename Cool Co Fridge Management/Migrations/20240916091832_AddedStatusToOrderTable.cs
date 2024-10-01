using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderStatusId",
                table: "orders",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "orderStatus",
                principalColumn: "OrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_OrderStatusId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "orders");
        }
    }
}

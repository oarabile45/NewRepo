using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "orderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusId",
                table: "orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "orderStatus",
                principalColumn: "OrderStatusId");
        }
    }
}

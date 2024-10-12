using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedStatusToPurchaseRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_OrderStatusId",
                table: "PurchaseRequests",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_orderStatus_OrderStatusId",
                table: "PurchaseRequests",
                column: "OrderStatusId",
                principalTable: "orderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_orderStatus_OrderStatusId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_OrderStatusId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "PurchaseRequests");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedPurchaseRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryNoteId",
                table: "orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    PurchaseRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FridgeTypeID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    RequestQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.PurchaseRequestId);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_fridge_type_FridgeTypeID",
                        column: x => x.FridgeTypeID,
                        principalTable: "fridge_type",
                        principalColumn: "FridgeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_FridgeTypeID",
                table: "PurchaseRequests",
                column: "FridgeTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders",
                column: "DeliveryNoteId",
                principalTable: "DeliveryNotes",
                principalColumn: "DeliveryNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryNoteId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders",
                column: "DeliveryNoteId",
                principalTable: "DeliveryNotes",
                principalColumn: "DeliveryNoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

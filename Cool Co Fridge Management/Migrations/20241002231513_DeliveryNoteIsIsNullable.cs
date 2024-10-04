using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryNoteIsIsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryNoteId",
                table: "orders",
                type: "int",
                nullable: true);


            migrationBuilder.AddForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders",
                column: "DeliveryNoteId",
                principalTable: "DeliveryNotes",
                principalColumn: "DeliveryNoteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_DeliveryNotes_DeliveryNoteId",
                table: "orders");

            migrationBuilder.DropColumn(
               name: "DeliveryNoteId",
               table: "orders");
        }
    }
}

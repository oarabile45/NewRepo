using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeliveryNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "DeliveryNotes",
               columns: table => new
               {
                   DeliveryNoteId = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   OrderID = table.Column<int>(type: "int", nullable: false),
                   DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   DeliveryDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_DeliveryNotes", x => x.DeliveryNoteId);
                   table.ForeignKey(
                      name: "FK_DeliveryNotes_orders_OrderID", // Foreign key relationship
                      column: x => x.OrderID,
                      principalTable: "orders",
                      principalColumn: "OrderID",
                      onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNotes_OrderID",
                table: "DeliveryNotes",
                column: "OrderID");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "DeliveryNotes");

        }
    }
}

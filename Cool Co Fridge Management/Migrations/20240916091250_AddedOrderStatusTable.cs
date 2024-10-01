using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "orderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.InsertData(
                table: "orderStatus",
                columns: new[] { "OrderStatusId", "OrderDesc" },
                values: new object[,]
                {
                    { 1, "Complete" },
                    { 2, "Pending" },
                    { 3, "Cancelled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderStatus");

            
        }
    }
}

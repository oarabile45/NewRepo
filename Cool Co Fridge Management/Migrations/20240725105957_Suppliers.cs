using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class Suppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fridge_type_SupplierID",
                table: "fridge_type",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridge_type_suppliers_SupplierID",
                table: "fridge_type",
                column: "SupplierID",
                principalTable: "suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fridge_type_suppliers_SupplierID",
                table: "fridge_type");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropIndex(
                name: "IX_fridge_type_SupplierID",
                table: "fridge_type");
        }
    }
}

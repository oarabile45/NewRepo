using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedQuotationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Quotations",
                columns: table => new
                {
                    QuotationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFQID = table.Column<int>(type: "int", nullable: false),
                    RFQuotationRFQID = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    QuotationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuotationNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotations", x => x.QuotationId);
                    table.ForeignKey(
                        name: "FK_Quotations_RFQuotation_RFQID",
                        column: x => x.RFQID,
                        principalTable: "RFQuotation",
                        principalColumn: "RFQID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotations_suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.NoAction);
                });

            
            migrationBuilder.CreateTable(
                name: "QuotationItems",
                columns: table => new
                {
                    QuotationItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationItems", x => x.QuotationItemId);
                    table.ForeignKey(
                        name: "FK_QuotationItems_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "QuotationId",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_QuotationId",
                table: "QuotationItems",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_RFQID",
                table: "Quotations",
                column: "RFQID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_SupplierId",
                table: "Quotations",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "QuotationItems");

            migrationBuilder.DropTable(
                name: "Quotations");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class NonEmptyNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_RFQuotation_RFQID",
                table: "Quotations");

            //migrationBuilder.DropIndex(
            //    name: "IX_Quotations_RFQuotationRFQID",
            //    table: "Quotations");

            //migrationBuilder.DropColumn(
            //    name: "RFQuotationRFQID",
            //    table: "Quotations");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_RFQID",
                table: "Quotations",
                column: "RFQID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_RFQuotation_RFQID",
                table: "Quotations",
                column: "RFQID",
                principalTable: "RFQuotation",
                principalColumn: "RFQID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_RFQuotation_RFQID",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_RFQID",
                table: "Quotations");

            migrationBuilder.AddColumn<int>(
                name: "RFQuotationRFQID",
                table: "Quotations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_RFQuotationRFQID",
                table: "Quotations",
                column: "RFQuotationRFQID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_RFQuotation_RFQuotationRFQID",
                table: "Quotations",
                column: "RFQuotationRFQID",
                principalTable: "RFQuotation",
                principalColumn: "RFQID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

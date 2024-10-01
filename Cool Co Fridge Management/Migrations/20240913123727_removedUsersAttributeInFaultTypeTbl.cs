using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class removedUsersAttributeInFaultTypeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_faultTypes_Users_ID",
                table: "faultTypes");

            migrationBuilder.DropIndex(
                name: "IX_faultTypes_ID",
                table: "faultTypes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "faultTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "faultTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_faultTypes_ID",
                table: "faultTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_faultTypes_Users_ID",
                table: "faultTypes",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}

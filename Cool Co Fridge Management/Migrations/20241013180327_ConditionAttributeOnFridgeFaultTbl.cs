using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class ConditionAttributeOnFridgeFaultTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionID",
                table: "fridgeFaults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fridgeFaults_ConditionID",
                table: "fridgeFaults",
                column: "ConditionID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_FridgeConditions_ConditionID",
                table: "fridgeFaults",
                column: "ConditionID",
                principalTable: "FridgeConditions",
                principalColumn: "ConditionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fridgeFaults_FridgeConditions_ConditionID",
                table: "fridgeFaults");

            migrationBuilder.DropIndex(
                name: "IX_fridgeFaults_ConditionID",
                table: "fridgeFaults");

            migrationBuilder.DropColumn(
                name: "ConditionID",
                table: "fridgeFaults");
        }
    }
}

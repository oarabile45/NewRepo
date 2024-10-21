using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class addingUsersColumnFridgeFaultTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_fridgeFaults_Users_ID",
            //    table: "fridgeFaults");

            //migrationBuilder.DropIndex(
            //    name: "IX_fridgeFaults_ID",
            //    table: "fridgeFaults");

            //migrationBuilder.DropColumn(
            //    name: "ID",
            //    table: "fridgeFaults");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "fridgeFaults",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_fridgeFaults_Id",
                table: "fridgeFaults",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_Id",
                table: "fridgeFaults",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_fridgeFaults_Users_UserID",
            //    table: "fridgeFaults");

            //migrationBuilder.DropIndex(
            //    name: "IX_fridgeFaults_UserID",
            //    table: "fridgeFaults");

            //migrationBuilder.DropColumn(
            //    name: "UserID",
            //    table: "fridgeFaults");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "fridgeFaults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fridgeFaults_ID",
                table: "fridgeFaults",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_ID",
                table: "fridgeFaults",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}

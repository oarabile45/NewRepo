using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class FridgeFaultAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fridgeFaults_Users_UsersID",
                table: "fridgeFaults");

            migrationBuilder.RenameColumn(
                name: "UsersID",
                table: "fridgeFaults",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_fridgeFaults_UsersID",
                table: "fridgeFaults",
                newName: "IX_fridgeFaults_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_ID",
                table: "fridgeFaults",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fridgeFaults_Users_ID",
                table: "fridgeFaults");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "fridgeFaults",
                newName: "UsersID");

            migrationBuilder.RenameIndex(
                name: "IX_fridgeFaults_ID",
                table: "fridgeFaults",
                newName: "IX_fridgeFaults_UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_UsersID",
                table: "fridgeFaults",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}

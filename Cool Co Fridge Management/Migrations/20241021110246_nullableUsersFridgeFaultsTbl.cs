using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class nullableUsersFridgeFaultsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_fridgeFaults_Users_UserID",
            //    table: "fridgeFaults");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "fridgeFaults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_UserID",
                table: "fridgeFaults",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_fridgeFaults_Users_UserID",
            //    table: "fridgeFaults");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "fridgeFaults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_UserID",
                table: "fridgeFaults",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

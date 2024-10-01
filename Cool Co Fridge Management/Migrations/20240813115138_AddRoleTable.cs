using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

           
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleID);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Customer" },
                    { 2, "Employee" }
                });
            migrationBuilder.CreateIndex( //AI generated
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId"
                );
            migrationBuilder.AddForeignKey(
                name: "FK_Users_roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropTable( //AI generated
                 name: "Roles");
        }
    }
}

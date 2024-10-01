using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class repairDateAndUserIDAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "fridgeFaults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RepairDate",
                table: "fridgeFaults",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersID",
                table: "fridgeFaults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fridgeFaults_UsersID",
                table: "fridgeFaults",
                column: "UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_fridgeFaults_Users_UsersID",
                table: "fridgeFaults",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fridgeFaults_Users_UsersID",
                table: "fridgeFaults");

            migrationBuilder.DropIndex(
                name: "IX_fridgeFaults_UsersID",
                table: "fridgeFaults");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "fridgeFaults");

            migrationBuilder.DropColumn(
                name: "RepairDate",
                table: "fridgeFaults");

            migrationBuilder.DropColumn(
                name: "UsersID",
                table: "fridgeFaults");
        }
    }
}

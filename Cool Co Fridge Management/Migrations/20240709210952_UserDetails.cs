using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class UserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                    // Add other columns as needed
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);

                });

            // Ensure AspNet* tables are not affected
            // migrationBuilder.DropPrimaryKey("PK_users", table: "users");
            // migrationBuilder.RenameTable(name: "users", newName: "Users");
            // migrationBuilder.AddPrimaryKey("PK_Users", table: "Users", column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            // Revert other changes if any
            // migrationBuilder.DropPrimaryKey("PK_Users", table: "Users");
            // migrationBuilder.RenameTable(name: "Users", newName: "users");
            // migrationBuilder.AddPrimaryKey("PK_users", table: "users", column: "ID");

            // Ensure AspNet* tables are recreated if they were dropped
            // Recreate AspNet* tables here if they were accidentally dropped
        }
    }
}

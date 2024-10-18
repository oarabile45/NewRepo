using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    /// <inheritdoc />
    public partial class SyncDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Users_UserID",
                table: "MaintenanceRequests");


            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_MaintenanceTech_MaintenanceTechID",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTechID",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

          

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_UserID",
                table: "MaintenanceRequests",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

         

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_MaintenanceTech_MaintenanceTechID",
                table: "Notifications",
                column: "MaintenanceTechID",
                principalTable: "MaintenanceTech",
                principalColumn: "MaintenanceTechID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Users_UserID",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_MaintenanceTech_MaintenanceTechID",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTechID",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_UserID",
                table: "MaintenanceRequests",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_MaintenanceTech_MaintenanceTechID",
                table: "Notifications",
                column: "MaintenanceTechID",
                principalTable: "MaintenanceTech",
                principalColumn: "MaintenanceTechID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OrderAndNotificationRelationOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Orders_OrderId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_OrderId",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "hasNotification",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OrderId",
                table: "Notifications",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Orders_OrderId",
                table: "Notifications",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Orders_OrderId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_OrderId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "hasNotification",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OrderId",
                table: "Notifications",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Orders_OrderId",
                table: "Notifications",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

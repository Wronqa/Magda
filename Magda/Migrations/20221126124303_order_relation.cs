using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magda.Migrations
{
    public partial class order_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestListListId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Order_GuestListListId",
                table: "Order",
                column: "GuestListListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_GuestList_GuestListListId",
                table: "Order",
                column: "GuestListListId",
                principalTable: "GuestList",
                principalColumn: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_GuestList_GuestListListId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_GuestListListId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "GuestListListId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Order");
        }
    }
}

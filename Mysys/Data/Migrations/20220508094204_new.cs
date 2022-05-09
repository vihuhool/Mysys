using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserID",
                table: "Items",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserID",
                table: "Items",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Items");
        }
    }
}

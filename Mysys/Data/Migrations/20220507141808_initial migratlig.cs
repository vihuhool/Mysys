using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigratlig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fields",
                table: "Add",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fields",
                table: "Add");
        }
    }
}

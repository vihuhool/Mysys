using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigratligd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fields",
                table: "Add",
                newName: "Field");

            migrationBuilder.AddColumn<string>(
                name: "FieldType",
                table: "Add",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "Add");

            migrationBuilder.RenameColumn(
                name: "Field",
                table: "Add",
                newName: "Fields");
        }
    }
}

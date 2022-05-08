using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigratligdsfgd4dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Add");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Add",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Add", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Add_Collections_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Add_CollectionID",
                table: "Add",
                column: "CollectionID");
        }
    }
}

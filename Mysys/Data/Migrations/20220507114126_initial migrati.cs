using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigrati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Collections_CollectionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CollectionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CollectionId",
                table: "Tag",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CollectionId",
                table: "Tags",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Collections_CollectionId",
                table: "Tags",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

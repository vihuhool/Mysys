using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigratli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolFields_Items_ItemId",
                table: "BoolFields");

            migrationBuilder.DropForeignKey(
                name: "FK_DateTimeFields_Items_ItemId",
                table: "DateTimeFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Items_ItemId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TextFields_Items_ItemId",
                table: "TextFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionID",
                table: "Item",
                newName: "IX_Item_CollectionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Add",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_BoolFields_Item_ItemId",
                table: "BoolFields",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimeFields_Item_ItemId",
                table: "DateTimeFields",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collections_CollectionID",
                table: "Item",
                column: "CollectionID",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Item_ItemId",
                table: "Tags",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TextFields_Item_ItemId",
                table: "TextFields",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolFields_Item_ItemId",
                table: "BoolFields");

            migrationBuilder.DropForeignKey(
                name: "FK_DateTimeFields_Item_ItemId",
                table: "DateTimeFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collections_CollectionID",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Item_ItemId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TextFields_Item_ItemId",
                table: "TextFields");

            migrationBuilder.DropTable(
                name: "Add");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionID",
                table: "Items",
                newName: "IX_Items_CollectionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolFields_Items_ItemId",
                table: "BoolFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimeFields_Items_ItemId",
                table: "DateTimeFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionID",
                table: "Items",
                column: "CollectionID",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Items_ItemId",
                table: "Tags",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TextFields_Items_ItemId",
                table: "TextFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

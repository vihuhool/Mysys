using Microsoft.EntityFrameworkCore.Migrations;

namespace Mysys.Data.Migrations
{
    public partial class initialmigratligdsfgd4dkjhddssghffggf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Add_Collections_CollectionID",
                table: "Add");

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
                name: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Add",
                table: "Add");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Add",
                newName: "Adds");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionID",
                table: "Items",
                newName: "IX_Items_CollectionID");

            migrationBuilder.RenameIndex(
                name: "IX_Add_CollectionID",
                table: "Adds",
                newName: "IX_Adds_CollectionID");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "TextFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "DateTimeFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "BoolFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adds",
                table: "Adds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CollectionId",
                table: "Tags",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adds_Collections_CollectionID",
                table: "Adds",
                column: "CollectionID",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoolFields_Items_ItemId",
                table: "BoolFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimeFields_Items_ItemId",
                table: "DateTimeFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionID",
                table: "Items",
                column: "CollectionID",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Collections_CollectionId",
                table: "Tags",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adds_Collections_CollectionID",
                table: "Adds");

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
                name: "FK_Tags_Collections_CollectionId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Items_ItemId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TextFields_Items_ItemId",
                table: "TextFields");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CollectionId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adds",
                table: "Adds");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Adds",
                newName: "Add");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionID",
                table: "Item",
                newName: "IX_Item_CollectionID");

            migrationBuilder.RenameIndex(
                name: "IX_Adds_CollectionID",
                table: "Add",
                newName: "IX_Add_CollectionID");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "TextFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "DateTimeFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "BoolFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Add",
                table: "Add",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Add_Collections_CollectionID",
                table: "Add",
                column: "CollectionID",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}

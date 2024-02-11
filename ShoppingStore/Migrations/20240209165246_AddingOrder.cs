using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingStore.Migrations
{
    public partial class AddingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Items_ItemID",
                table: "OrdersItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_OrderID",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "OrdersItems",
                newName: "OrdersId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "OrdersItems",
                newName: "ItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_ItemID",
                table: "OrdersItems",
                newName: "IX_OrdersItems_OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Items_ItemsId",
                table: "OrdersItems",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_OrdersId",
                table: "OrdersItems",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Items_ItemsId",
                table: "OrdersItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_OrdersId",
                table: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrdersItems",
                newName: "ItemID");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "OrdersItems",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_OrdersId",
                table: "OrdersItems",
                newName: "IX_OrdersItems_ItemID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrdersItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Items_ItemID",
                table: "OrdersItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_OrderID",
                table: "OrdersItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

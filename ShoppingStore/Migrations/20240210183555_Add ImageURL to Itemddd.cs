using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingStore.Migrations
{
    public partial class AddImageURLtoItemddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_OrdersId",
                table: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrdersItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_OrdersId",
                table: "OrdersItems",
                newName: "IX_OrdersItems_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrdersItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_OrderId",
                table: "OrdersItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_OrderId",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrdersItems",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_OrderId",
                table: "OrdersItems",
                newName: "IX_OrdersItems_OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_OrdersId",
                table: "OrdersItems",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

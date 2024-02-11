using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingStore.Migrations
{
    public partial class AddImageURLtoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Items");
        }
    }
}

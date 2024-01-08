using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomweb.Migrations
{
    public partial class AddQuantityField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Products");
        }
    }
}

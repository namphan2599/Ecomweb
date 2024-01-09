using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomweb.Migrations
{
    public partial class RedesignCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCart",
                columns: table => new
                {
                    CartId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false),
                    CartId = table.Column<long>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCart", x => new { x.CartId1, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCart_Cart_CartId1",
                        column: x => x.CartId1,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCart_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCart_ProductId",
                table: "ProductCart",
                column: "ProductId");
        }
    }
}

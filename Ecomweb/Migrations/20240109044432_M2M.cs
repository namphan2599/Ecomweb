using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomweb.Migrations
{
    public partial class M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCart",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false),
                    CartId1 = table.Column<int>(type: "INTEGER", nullable: false),
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCart");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "quantity");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomweb.Migrations
{
    public partial class ChangeLongToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId1",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CartId1",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId1",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItems",
                newName: "CartId1");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                newName: "IX_CartItems_CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId1",
                table: "CartItems",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseSaleApi.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Categories_CategotyId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Packages_PackageId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PackageId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_CategotyId",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CategotyId",
                table: "Gifts");

            migrationBuilder.CreateTable(
                name: "CategoryGift",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    GiftsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGift", x => new { x.CategoriesId, x.GiftsId });
                    table.ForeignKey(
                        name: "FK_CategoryGift_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGift_Gifts_GiftsId",
                        column: x => x.GiftsId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackagePurchase",
                columns: table => new
                {
                    PackagesId = table.Column<int>(type: "int", nullable: false),
                    PurchasesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagePurchase", x => new { x.PackagesId, x.PurchasesId });
                    table.ForeignKey(
                        name: "FK_PackagePurchase_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackagePurchase_Purchases_PurchasesId",
                        column: x => x.PurchasesId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGift_GiftsId",
                table: "CategoryGift",
                column: "GiftsId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePurchase_PurchasesId",
                table: "PackagePurchase",
                column: "PurchasesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGift");

            migrationBuilder.DropTable(
                name: "PackagePurchase");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategotyId",
                table: "Gifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PackageId",
                table: "Purchases",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_CategotyId",
                table: "Gifts",
                column: "CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Categories_CategotyId",
                table: "Gifts",
                column: "CategotyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Packages_PackageId",
                table: "Purchases",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

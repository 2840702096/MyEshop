using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class mig_Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SWAndCommentsRelation",
                table: "Comments_Questions_Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SlideCommentTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductMainCategoryId = table.Column<int>(type: "int", nullable: false),
                    SlideCommentTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideCommentTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideCommentTitles_ProductCategories_ProductMainCategoryId",
                        column: x => x.ProductMainCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrengthsOrWeaknesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPositiveOrNegative = table.Column<bool>(type: "bit", nullable: false),
                    SWAndCommentsRelation = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsRefused = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrengthsOrWeaknesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrengthsOrWeaknesses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrengthsOrWeaknesses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlideComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCTitleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SCValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlideComments_SlideCommentTitles_SCTitleId",
                        column: x => x.SCTitleId,
                        principalTable: "SlideCommentTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlideComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlideComments_ProductId",
                table: "SlideComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideComments_SCTitleId",
                table: "SlideComments",
                column: "SCTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideComments_UserId",
                table: "SlideComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideCommentTitles_ProductMainCategoryId",
                table: "SlideCommentTitles",
                column: "ProductMainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StrengthsOrWeaknesses_ProductId",
                table: "StrengthsOrWeaknesses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StrengthsOrWeaknesses_UserId",
                table: "StrengthsOrWeaknesses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlideComments");

            migrationBuilder.DropTable(
                name: "StrengthsOrWeaknesses");

            migrationBuilder.DropTable(
                name: "SlideCommentTitles");

            migrationBuilder.DropColumn(
                name: "SWAndCommentsRelation",
                table: "Comments_Questions_Answers");
        }
    }
}

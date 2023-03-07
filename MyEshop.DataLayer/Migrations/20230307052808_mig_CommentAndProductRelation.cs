using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class mig_CommentAndProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Comments_Questions_Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Questions_Answers_ProductId",
                table: "Comments_Questions_Answers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Questions_Answers_Products_ProductId",
                table: "Comments_Questions_Answers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Questions_Answers_Products_ProductId",
                table: "Comments_Questions_Answers");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Questions_Answers_ProductId",
                table: "Comments_Questions_Answers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Comments_Questions_Answers");
        }
    }
}

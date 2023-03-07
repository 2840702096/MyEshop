using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class mig_CommentAndSlideCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "SlideComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SlideComments_CommentId",
                table: "SlideComments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideComments_Comments_Questions_Answers_CommentId",
                table: "SlideComments",
                column: "CommentId",
                principalTable: "Comments_Questions_Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideComments_Comments_Questions_Answers_CommentId",
                table: "SlideComments");

            migrationBuilder.DropIndex(
                name: "IX_SlideComments_CommentId",
                table: "SlideComments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "SlideComments");
        }
    }
}

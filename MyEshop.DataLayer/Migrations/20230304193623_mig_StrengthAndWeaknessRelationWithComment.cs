using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class mig_StrengthAndWeaknessRelationWithComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SWAndCommentsRelation",
                table: "Comments_Questions_Answers");

            migrationBuilder.RenameColumn(
                name: "SWAndCommentsRelation",
                table: "StrengthsOrWeaknesses",
                newName: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_StrengthsOrWeaknesses_CommentId",
                table: "StrengthsOrWeaknesses",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StrengthsOrWeaknesses_Comments_Questions_Answers_CommentId",
                table: "StrengthsOrWeaknesses",
                column: "CommentId",
                principalTable: "Comments_Questions_Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StrengthsOrWeaknesses_Comments_Questions_Answers_CommentId",
                table: "StrengthsOrWeaknesses");

            migrationBuilder.DropIndex(
                name: "IX_StrengthsOrWeaknesses_CommentId",
                table: "StrengthsOrWeaknesses");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "StrengthsOrWeaknesses",
                newName: "SWAndCommentsRelation");

            migrationBuilder.AddColumn<int>(
                name: "SWAndCommentsRelation",
                table: "Comments_Questions_Answers",
                type: "int",
                nullable: true);
        }
    }
}
